using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class CardRules
    {
        public static bool IsDefensible(enumCard offender,enumCard defender)
        {
            switch (offender)
            {
                case enumCard.Acid:
                    if (defender == enumCard.Base) return true;
                    break;
                case enumCard.Base:
                    if (defender == enumCard.Acid) return true;
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
