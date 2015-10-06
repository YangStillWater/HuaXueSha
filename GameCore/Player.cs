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
        /// <summary>
        /// 血，三国杀中的体力
        /// </summary>
        public int Blood = 4;
        public List<Card> Cards = new List<Card>();

        public int CardCountWhenBegin = 4;
        public int CardCountToGet = 2;

        public Character Character { get; set; }
   }
}
