using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class CardRules
    {
        private GameContext _gctx;
        public CardRules(GameContext context)
        {
            _gctx = context;
        }
        public void DealAcid()
        {
            foreach (var t in _gctx.targets)
            {
                t.Blood--;
            }
        }
        public void DealBase()
        {
            foreach (var t in _gctx.targets)
            {
                t.Blood--;
            }
        }
    }
}
