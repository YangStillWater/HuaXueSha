using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class Player
    {
        public string name;
        public string ConnectionID;
        /// <summary>
        /// 血，三国杀中的体力
        /// </summary>
        public int Blood => blood;
        private int blood = 4;
        public void DropBlood(int num = 1)
        {
            blood -= num;
            if (blood <= 0)
            {
                throw new PlayerDeadException();
            }
        }
        public void AddBlood(int num = 1)
        {
            blood = Math.Min(4, blood + num);
        }
        public List<Card> Cards = new List<Card>();
        public Card Gear;
        public Card Armor;

        public int CardCountWhenBegin = 4;
        public int CardCountToGet = 2;

        public Character Character { get; set; }
    }
}
