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
        GameContext gCtx = new GameContext();
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
        List<PlayerBox> otherPlayerBoxes => PlayerBoxes.Where(b => b.Player != gCtx.currentPlayer).ToList();

        ListBox currentListBox => currentPlayerBox.lbCards;
        ListBox currentTargetListBox => currentTargetPlayerBox.lbCards;

        public Form1()
        {
            InitializeComponent();

            PlayerBoxes = new List<PlayerBox>(gCtx.players.Count);
            foreach (var player in gCtx.players)
            {
                PlayerBoxes.Add(new PlayerBox(player));
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
                    panelDealCard.Visible = true;
                    lblSelectCard.Show();

                    currentListBox.SelectedIndexChanged += CardSelect;
                }));
            };
            gCtx.OnEndSelectOneCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblSelectCard.Hide();
                    currentListBox.SelectedIndexChanged -= CardSelect;
                    AddMessage($"玩家{gCtx.currentPlayer.name}选择了牌{gCtx.currentCard}");
                }));
            };
            gCtx.OnBeginSetTarget += delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblSelectTarget.Show();
                    AddMessage($"玩家{gCtx.currentPlayer.name}正在选择目标……");
                    foreach (var gb in otherPlayerBoxes)
                    {
                        gb.Click += PlayerSelect;
                    }
                }));
            };
            gCtx.OnEndSetTarget += delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblSelectTarget.Hide();
                    AddMessage($"玩家{gCtx.currentPlayer.name}选择了目标{gCtx.targets.First().name}");
                    foreach (var gb in otherPlayerBoxes)
                    {
                        gb.Click -= PlayerSelect;
                    }
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
            gCtx.OnEndDealCard += InvokeSynchronize;
            gCtx.OnEndDealCard += delegate {
                InvokeAddMessage($"玩家{gCtx.currentPlayer.name}对玩家{string.Join(",", gCtx.targets.Select(t=>t.name))}出牌{gCtx.currentCard}");
            };

            gCtx.Rules.OnBeginDefend += delegate
              {
                  this.Invoke(new Action(() =>
                  {
                      panelRespond.Show();
                  }));
              };
            gCtx.Rules.OnEndDefend += InvokeSynchronize;
        }
        #region 自定义事件
        void CardSelect(object sender, EventArgs e)
        {
            gCtx.SelectThisOneCard(currentListBox.SelectedIndex);
        }

        void PlayerSelect(object sender, EventArgs e)
        {
            var gb = sender as GroupBox;
            gb.BackColor = Color.LightCoral;
            var gbs = flowLayoutPanel1.Controls.Find("groupbox", false);
            int index = gbs.ToList().IndexOf(gb);
            gCtx.SetTheTargets(new int[] { index });
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
        }
        void InvokeAddMessage(string msg)
        {
            this.Invoke(new Action(() => AddMessage(msg)));
        }
        #endregion

        #region 按钮事件
        private void btnGameBegin_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
   new WaitCallback(WorkMethod), gCtx.autoEvent);

        }

        private void btnDealCard_Click(object sender, EventArgs e)
        {
            gCtx.DealThisOneCard(currentListBox.SelectedIndex);
            panelDealCard.Hide();
            Synchronize();
        }

        private void bntFinishDeal_Click(object sender, EventArgs e)
        {
            gCtx.FinishDealCards();
        }

        private void btnRespond_Click(object sender, EventArgs e)
        {
            gCtx.Rules.Defend(currentTargetListBox.SelectedIndex);
            panelRespond.Hide();
        }

        private void btnTolerate_Click(object sender, EventArgs e)
        {
            gCtx.Rules.Tolerate();
            panelRespond.Hide();
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
