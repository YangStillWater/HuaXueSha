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
                int i = gCtx.players.IndexOf(p);
                var gb = flowLayoutPanel1.Controls.Find("groupbox", false)[i] as PlayerBox;
                return gb;
            }
        }
        List<PlayerBox> PlayerBoxes;
        List<PlayerBox> otherPlayerBoxes
        {
            get
            {
                return PlayerBoxes.Where(b => b.Player != gCtx.currentPlayer).ToList();
            }
        }
        ListBox currentListBox
        {
            get
            {
                return currentPlayerBox.lbCards;
            }
        }

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
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("开始发牌");
                }));
            };
            gCtx.OnEndGetCards += delegate
            {
                this.Invoke(new Action(() =>
                {
                    currentPlayerBox.BackColor = Color.LightYellow;
                    Synchronize();
                }));
            };
            gCtx.OnBeginSelectOneCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    panel1.Visible = true;
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
                }));
            };
            gCtx.OnBeginSetTarget += delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblSelectTarget.Show();
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

            gCtx.OnBloodDrop += InvokeSynchronize;

            gCtx.Rules.OnBeginDefend+=delegate
            {
                this.Invoke(new Action(() =>
                {
                    lblDefend.Show();
                }));
            };
        }
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
            }
        }
        void InvokeSynchronize(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => Synchronize()));
        }

        private void btnGameBegin_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
   new WaitCallback(WorkMethod), gCtx.autoEvent);

        }

        private void btnDealCard_Click(object sender, EventArgs e)
        {
            foreach (var gb in otherPlayerBoxes)
            {
                gb.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            btnDealCard.Enabled = false;
            btnDealCard.Hide();
            gCtx.DealThisOneCard(currentListBox.SelectedIndex);
            panel1.Hide();
            Synchronize();
        }

    }
}
