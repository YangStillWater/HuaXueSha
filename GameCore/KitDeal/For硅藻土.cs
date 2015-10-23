using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.KitDeal
{
    /// <summary>
    /// 出牌者和接牌者决斗
    /// </summary>
    public class For硅藻土 : ForKit
    {
        public For硅藻土(GameContext gCtx) : base(gCtx)
        {

        }

        Player Player1;
        Player Player2;
        public override void Deal()
        {
            Player1 = Context.currentPlayer;
            Player2 = Context.targets.First();
            Context.Rules.currentTarget = Player1;
            Context.TurnCtx.IsFor硅藻土 = true;
            bool canDefend;
            do
            {
                canDefend = Context.Rules.RespondOne();
                ToNext();
            } while (canDefend);

        }
        void ToNext()
        {
            if (Context.Rules.currentTarget == Player1)
            {
                Context.Rules.currentTarget = Player2;
            }
            else
            {
                Context.Rules.currentTarget = Player1;
            }
        }
    }
}
