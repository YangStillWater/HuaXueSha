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
        public Label lblGear;
        public Label lblArmor;
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

            lblGear = new Label();
            lblGear.Text = player.Gear?.ToString();
            lblGear.Top = 150;

            lblArmor = new Label();
            lblArmor.Text = player.Armor?.ToString();
            lblArmor.Top = 170;

            this.Text = player.name;
            this.Name = "groupbox";
            this.Height = 200;
            this.Width = 120;

            this.Controls.Add(lbCards);
            this.Controls.Add(lblBlood);
            this.Controls.Add(lblGear);
            this.Controls.Add(lblArmor);
        }

        public void Synchronize()
        {
            cardsSource.ResetBindings(true);
            lblBlood.Text = Player.Blood.ToString();
            lblGear.Text = Player.Gear?.ToString();
            lblArmor.Text = Player.Armor?.ToString();
            lbCards.ClearSelected();
        }
    }
}
