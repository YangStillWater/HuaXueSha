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

        public Form1()
        {
            InitializeComponent();

            foreach (var player in gCtx.players)
            {
                var lb = new ListBox();
                lb.Name = "cardList";
                lb.SelectedIndexChanged += delegate
                {
                    MessageBox.Show(lb.SelectedIndex.ToString());
                };
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
                    var p = gCtx.currentPlayer;
                    int i = gCtx.players.IndexOf(p);
                    var gb = flowLayoutPanel1.Controls.Find("groupbox", false)[i] as GroupBox;
                    gb.BackColor = Color.LightYellow;
                    var l = gb.Controls.Find("cardList", false).FirstOrDefault() as ListBox;
                    l.Items.Clear();
                    l.Items.AddRange(p.Cards.ToArray());

                    label2.Text = gCtx.availableCards.Count.ToString();
                }));
            };
            gCtx.OnBeginDealCard += delegate
            {
                this.Invoke(new Action(() =>
                {
                    panel1.Visible = true;
                }));
            };
            gCtx.OnEndDealCard += delegate
            {

            };
            gCtx.OnBeginSetTarget += delegate
            {

            };
            gCtx.OnEndSetTarget += delegate
            {

            };
            gCtx.OnBloodDrop += delegate
            {

            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
   new WaitCallback(WorkMethod), gCtx.autoEvent);

        }
        void WorkMethod(object stateInfo)
        {
            Dictionary<string, object> wfArgs = new Dictionary<string, object>();
            wfArgs.Add("ctx", gCtx);

            Activity workflow1 = new GameMain();
            WorkflowInvoker.Invoke(workflow1, wfArgs);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }
    }
}
