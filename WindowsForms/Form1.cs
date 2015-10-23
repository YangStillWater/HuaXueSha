using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameCore;
using HuaXueSha;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        GameContext gCtx;

        public List<Player> playerlist;
        PlayerBox currentPlayerBox
        {
            get
            {
                var p = gCtx.currentPlayer;
                if (p != null)
                {
                    int i = gCtx.players.IndexOf(p);
                    return PlayerBoxes[i];
                }
                else
                {
                    return null;
                }
            }
        }
        PlayerBox currentTargetPlayerBox
        {
            get
            {
                var p = gCtx.Rules.currentTarget;
                if (p != null)
                {
                    int i = gCtx.players.IndexOf(p);
                    return PlayerBoxes[i];
                }
                else
                {
                    return null;
                }
            }
        }
        List<PlayerBox> PlayerBoxes;
        //List<PlayerBox> otherPlayerBoxes => PlayerBoxes.Where(b => b.Player != gCtx.currentPlayer).ToList();

        ListBox currentLbCards => currentPlayerBox?.lbCards;
        ListBox currentTargetListBox => currentTargetPlayerBox.lbCards;

        public Form1()
        {
            InitializeComponent();
            playerlist = new List<Player>();
        }

        private void InitGame()
        {
            gCtx = new GameContext();
            PlayerBoxes = new List<PlayerBox>(gCtx.players.Count);
            foreach (var player in gCtx.players)
            {
                var pb = new PlayerBox(player);
                pb.Click += PlayerSelect;
                pb.lbCards.SelectedIndexChanged += CardSelect;
                PlayerBoxes.Add(pb);
            }
            flowLayoutPanel1.Controls.AddRange(PlayerBoxes.ToArray());


            gCtx.OnBeginPrepareCards += InvokeSynchronize;
            gCtx.OnEndPrepareCards += InvokeSynchronize;
            gCtx.OnBeginGetCards += delegate
            {
                InvokeAddMessage($"玩家{gCtx.currentPlayer.name}摸{gCtx.currentPlayer.CardCountToGet}张牌");
            };
            gCtx.OnEndGetCards += InvokeSynchronize;
            gCtx.OnBeginSelectOneCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    panelDealCard.Show();
                    lblSelectCard.Show();
                    currentLbCards.SelectionMode = SelectionMode.One;

                    AddMessage($"轮到玩家{gCtx.currentPlayer.name}出牌");
                }));
            };
            gCtx.OnEndSelectOneCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblSelectCard.Hide();
                    currentLbCards.SelectionMode = SelectionMode.None;
                    AddMessage($"玩家{gCtx.currentPlayer.name}选择了牌{string.Join(",", gCtx.cardDress.ActualCards)}");
                }));
            };
            gCtx.OnCannotSelectThisCard += delegate
            {
                InvokeAddMessage($"玩家{gCtx.currentPlayer.name}出一次酸或碱之后就不能再出了");
            };

            gCtx.OnBeginSetTarget += delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblSelectTarget.Show();
                    AddMessage($"玩家{gCtx.currentPlayer.name}正在选择目标……");
                }));
            };
            gCtx.OnEndSetTarget += delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblSelectTarget.Hide();
                    AddMessage($"玩家{gCtx.currentPlayer.name}选择了目标{gCtx.targets.First().name}");
                }));
            };
            gCtx.OnBeginDealCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    btnDealCard.Show();
                    btnDealCard.Enabled = true;
                }));
            };
            gCtx.OnEndDealCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    AddMessage($"玩家{gCtx.currentPlayer.name}对玩家{string.Join(",", gCtx.targets.Select(t => t.name))}出牌{string.Join(",", gCtx.cardDress.ActualCards)}");
                    panelDealCard.Hide();
                    Synchronize();
                }));
            };

            gCtx.OnDealCardsFinish += delegate
            {
                this.Invoke(new Action(() =>
                {
                    AddMessage($"玩家{gCtx.currentPlayer.name}出牌结束");
                    panelDealCard.Hide();
                }));
            };

            gCtx.OnBeginDropCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    panelDrop.Show();
                    currentLbCards.SelectionMode = SelectionMode.MultiSimple;
                    AddMessage($"玩家{gCtx.currentPlayer.name}请弃掉{gCtx.CardsCountToDrop}张牌");
                }));
            };
            gCtx.OnEndDropCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    AddMessage($"玩家{gCtx.currentPlayer.name}弃牌结束");
                    panelDrop.Hide();
                    currentLbCards.SelectionMode = SelectionMode.None;
                    Synchronize();
                }));
            };
            gCtx.OnCardsDropped += delegate
            {
                this.Invoke(new Action(() =>
                {
                    AddMessage($"玩家{gCtx.currentPlayer.name}弃牌");
                    Synchronize();
                }));
            };


            gCtx.OnGameOver += delegate
            {
                this.Invoke(new Action(() =>
                {
                    AddMessage($"游戏结束");
                    panelDealCard.Hide();
                    Synchronize();
                }));
            };

            gCtx.Rules.OnBeginDefend += delegate
            {
                this.Invoke(new Action(() =>
                {
                    panelRespond.Show();
                    currentTargetListBox.SelectionMode = SelectionMode.One;
                    AddMessage($"玩家{gCtx.Rules.currentTarget.name}准备出牌应对");
                }));
            };
            gCtx.Rules.OnEndDefend += InvokeSynchronize;
            gCtx.Rules.OnEndDefend += delegate
            {
                this.Invoke(new Action(() =>
                {
                    panelRespond.Hide();
                }));
            };
            gCtx.Rules.OnDefendResult += delegate
            {
                InvokeAddMessage($"玩家{gCtx.Rules.currentTarget.name}回应了牌{gCtx.Rules.defenderCard}");
            };
            gCtx.Rules.OnTolerateResult += delegate
            {
                InvokeAddMessage($"玩家{gCtx.Rules.currentTarget.name}没有应对");
            };
            gCtx.Rules.OnDropBlood += delegate
            {
                InvokeAddMessage($"玩家{gCtx.Rules.currentTarget.name}掉血了");
            };
            gCtx.Rules.OnWrongCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("当前不能出这张牌");
                }));
            };
        }

        #region 自定义事件
        void CardSelect(object sender, EventArgs e)
        {
            var l = sender as ListBox;
            if (l==currentLbCards && l.SelectedIndex >= 0)
            {
                gCtx.SelectThisOneCard(currentLbCards.SelectedIndex); 
            }
        }

        void PlayerSelect(object sender, EventArgs e)
        {
            var pb = sender as PlayerBox;
            if (pb != currentPlayerBox || gCtx.cardDress.VirtualCard is Glucose)
            {
                pb.BackColor = Color.LightCoral;
                int index = PlayerBoxes.IndexOf(pb);
                gCtx.SetTheTargets(new int[] { index }); 
            }
        }

        void InvokeSynchronize(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => Synchronize()));
        }


        #endregion

        #region 辅助程序
        void WorkMethod(object stateInfo)
        {
            Dictionary<string, object> wfArgs = new Dictionary<string, object>();
            wfArgs.Add("ctx", gCtx);

            Activity workflow1 = new GameMain();
            WorkflowInvoker.Invoke(workflow1, wfArgs);
        }

        void Synchronize()
        {
            lblAvailableCount.Text = gCtx.availableCards.Count.ToString();
            lblDroppedCount.Text = gCtx.droppedCards.Count.ToString();
            foreach (var pb in PlayerBoxes)
            {
                pb.Synchronize();
                pb.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            if (currentPlayerBox != null)
            {
                currentPlayerBox.BackColor = Color.LightYellow;
            }
        }
        void AddMessage(string msg)
        {
            lbMessage.Items.Add(msg);
            lbMessage.TopIndex = lbMessage.Items.Count - (int)(lbMessage.Height / lbMessage.ItemHeight);
        }
        void InvokeAddMessage(string msg)
        {
            this.Invoke(new Action(() => AddMessage(msg)));
        }
        #endregion

        #region 按钮事件
        private void btnGameBegin_Click(object sender, EventArgs e)
        {
            InitGame();

            ThreadPool.QueueUserWorkItem(new WaitCallback(WorkMethod));

        }

        private void btnDealCard_Click(object sender, EventArgs e)
        {
            gCtx.DealThisOneCard();
        }

        private void bntFinishDeal_Click(object sender, EventArgs e)
        {
            gCtx.FinishDealCards();
        }

        private void btnRespond_Click(object sender, EventArgs e)
        {
            gCtx.Rules.Defend(currentTargetListBox.SelectedIndex);
        }

        private void btnTolerate_Click(object sender, EventArgs e)
        {
            gCtx.Rules.Tolerate();
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            gCtx.DropTheCards(currentLbCards.SelectedIndices.Cast<int>().ToArray());
        }
        #endregion
    }
}
