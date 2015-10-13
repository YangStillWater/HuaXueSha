using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GameCore;

namespace WindowsForms
{
    public class PlayerBox : GroupBox
    {
        public BindingSource cardsSource;
        public ListBox lbCards;
        public Label lblBlood;
        public Player Player;
        public bool IsCurrent => true;

        public PlayerBox(Player player)
        {
            Player = player;

            lbCards = new ListBox();
            lbCards.Name = "cardList";
            lbCards.Top = 10;
            lbCards.Left = 5;
            lbCards.Width = 100;
            lbCards.Height = 120;
            cardsSource = new BindingSource(player.Cards, null);
            lbCards.DataSource = cardsSource;

            lblBlood = new Label();
            lblBlood.Text = player.Blood.ToString();
            lblBlood.Top = 130;

            this.Text = player.name;
            this.Name = "groupbox";
            this.Height = 150;
            this.Width = 120;

            this.Controls.Add(lbCards);
            this.Controls.Add(lblBlood);
        }

        public void Synchronize()
        {
            cardsSource.ResetBindings(true);
            lblBlood.Text = Player.Blood.ToString();
            lbCards.ClearSelected();
        }
    }
}
