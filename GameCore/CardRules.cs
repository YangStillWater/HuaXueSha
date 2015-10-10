using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class CardRules
    {
        public event EventHandler<EventArgs> OnBeginDefend;
        public event EventHandler<EventArgs> OnEndDefend;

        private GameContext _gctx;

        public Player currentTarget;
        public Card offenderCard;
        public Card defenderCard;

        public bool IsTolerated = false;

        public CardRules(GameContext context)
        {
            _gctx = context;
        }
        public void Respond()
        {
            offenderCard = _gctx.currentCard;
            foreach (var t in _gctx.targets)
            {
                currentTarget = t;
                OnBeginDefend?.Invoke(this, new EventArgs());
                if (_gctx.autoEvent.WaitOne(15000, false) && !IsTolerated)//超时或点击不出牌
                {
                    DefendResult();
                }
                else
                {
                    TolerateResult();
                }
                OnEndDefend(this, new EventArgs());
            }
        }

        public void Defend(int cardIndex)
        {
            defenderCard = currentTarget.Cards[cardIndex];
            if (CanDefend(offenderCard, defenderCard))
            {
                currentTarget.Cards.Remove(defenderCard);
                _gctx.droppedCards.Add(defenderCard);
                _gctx.autoEvent.Set();
            }
        }
        public void Tolerate()
        {
            IsTolerated = true;
            _gctx.autoEvent.Set();
        }
        public bool CanDefend(Card offender, Card defender)
        {
            if (offender is Acid)
            {
                return defender is Base;
            }
            if (offender is Base)
            {
                return defender is Acid;
            }
            return false;
        }
        void TolerateResult()
        {
            if (offenderCard is Acid || offenderCard is Base)
            {
                currentTarget.DropBlood();
            }
            else if (offenderCard is Glucose)
            {
                currentTarget.AddBlood();
            }
        }
        void DefendResult()
        {

        }

    }
}
