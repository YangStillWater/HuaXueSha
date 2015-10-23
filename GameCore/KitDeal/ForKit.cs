using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.KitDeal
{
    public abstract class ForKit
    {
        protected GameContext Context;
        public ForKit(GameContext gCtx)
        {
            Context = gCtx;
        }
        public abstract void Deal();
    }
}
