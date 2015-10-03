using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore;

namespace HuaXueSha
{
    public class GameContextConsole : GameContext
    {
        public GameContextConsole()
        {
            BeginPrepareCards += delegate
            {
                Console.WriteLine("begin prepare cards");
            };
            EndPrepareCards += delegate
            {
                Console.WriteLine("end prepare cards");
            };
            OnGameOver += delegate
            {
                Console.WriteLine("游戏结束");
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.name}'s blood:{player.Blood}");
                }
            };
        }
    }
}
