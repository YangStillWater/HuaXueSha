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
        GroupBox currentGroupBox
        {
            get
            {
                var p = gCtx.currentPlayer;
                int i = gCtx.players.IndexOf(p);
                var gb = flowLayoutPanel1.Controls.Find("groupbox", false)[i] as GroupBox;
                return gb;
            }
        }
        List<GroupBox> otherGroupBoxes
        {
            get
            {
                var gbs = flowLayoutPanel1.Controls.Find("groupbox", false);
                var gbList = new List<GroupBox>(gbs.Length);
                var p = gCtx.currentPlayer;
                int pi = gCtx.players.IndexOf(p);
                for (int i = 0; i < gbs.Length; i++)
                {
                    if (i!=pi)
                    {
                        gbList.Add(gbs[i] as GroupBox);
                    }
                }
                return gbList;
            }
        }
        ListBox currentListBox
        {
            get
            {
                return currentGroupBox.Controls.Find("cardList", false).FirstOrDefault() as ListBox;
            }
        }

        public Form1()
        {
            InitializeComponent();

            foreach (var player in gCtx.players)
            {
                var lb = new ListBox();
                lb.Name = "cardList";
                //lb.SelectedIndexChanged += delegate
                //{
                //    MessageBox.Show(lb.SelectedIndex.ToString());
                //};
                var lbl = new Label();
                lbl.Text = player.Blood.ToString();
                var groupbox = new GroupBox();
                groupbox.Text = player.name;
                groupbox.Name = "groupbox";
                groupbox.Controls.Add(lb);
                groupbox.Controls.Add(lbl);
                flowLayoutPanel1.Controls.Add(groupbox);
            }


            gCtx.OnBeginPrepareCards += delegate
            {
                this.Invoke(new Action(() => label2.Text = gCtx.availableCards.Count.ToString()));
            };
            gCtx.OnEndPrepareCards += delegate
            {
                this.Invoke(new Action(() =>
                {
                    for (int i = 0; i < gCtx.players.Count; i++)
                    {
                        var p = gCtx.players[i];
                        var gb = flowLayoutPanel1.Controls.Find("groupbox", false)[i] as GroupBox;
                        var l = gb.Controls.Find("cardList", false).FirstOrDefault() as ListBox;
                        l.Items.AddRange(p.Cards.ToArray());
                    }
                    label2.Text = gCtx.availableCards.Count.ToString();
                }));
            };
            gCtx.OnEndGetCards += delegate
            {
                this.Invoke(new Action(() =>
                {
                    currentGroupBox.BackColor = Color.LightYellow;
                    currentListBox.Items.Clear();
                    currentListBox.Items.AddRange(gCtx.currentPlayer.Cards.ToArray());

                    label2.Text = gCtx.availableCards.Count.ToString();
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
                    foreach (var gb in otherGroupBoxes)
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
                    foreach (var gb in otherGroupBoxes)
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
            gCtx.OnBloodDrop += delegate
            {

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

        private void btnGameBegin_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
   new WaitCallback(WorkMethod), gCtx.autoEvent);

        }

        private void btnDealCard_Click(object sender, EventArgs e)
        {
            foreach (var gb in otherGroupBoxes)
            {
                gb.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            btnDealCard.Enabled = false;
            btnDealCard.Hide();
            gCtx.DealThisOneCard(currentListBox.SelectedIndex);
            panel1.Hide();
        }

    }
}
