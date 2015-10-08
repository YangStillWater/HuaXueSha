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

        public PlayerBox(Player player)
        {
            Player = player;

            lbCards = new ListBox();
            lbCards.Name = "cardList";
            cardsSource = new BindingSource(player.Cards, null);
            lbCards.DataSource = cardsSource;

            lblBlood = new Label();
            lblBlood.Text = player.Blood.ToString();
            this.Text = player.name;
            this.Name = "groupbox";
            this.Controls.Add(lbCards);
            this.Controls.Add(lblBlood);
        }

        public void Synchronize()
        {
            cardsSource.ResetBindings(true);
            lblBlood.Text = Player.Blood.ToString();
        }
    }
}
