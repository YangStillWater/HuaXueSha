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

        public CardRules(GameContext context)
        {
            _gctx = context;
        }
        public void DealOffence()
        {
            offenderCard = _gctx.currentCard;
            foreach (var t in _gctx.targets)
            {
                currentTarget = t;
                if (!Defend())
                {
                    t.Blood--; 
                }
            }
        }
        public bool Defend()
        {
            OnBeginDefend(this, new EventArgs());
            if (_gctx.autoEvent.WaitOne(15000, false))
            {
                OnEndDefend(this, new EventArgs());
                return true;
            }
            else
            {
                OnEndDefend(this, new EventArgs());
                return false;
            }
        }

        public void DefendDone(int cardIndex)
        {
            defenderCard = currentTarget.Cards[cardIndex];
            if (CanDefend(offenderCard,defenderCard))
            {
                currentTarget.Cards.Remove(defenderCard);
                _gctx.droppedCards.Add(defenderCard);
                _gctx.autoEvent.Set();
            }
        }

        public bool CanDefend(Card offender,Card defender)
        {
            if (offender.GetType()==typeof(Acid))
            {
                return defender.GetType() == typeof(Base);
            }
            if (offender.GetType()==typeof(Base))
            {
                return defender.GetType() == typeof(Acid);
            }
            return false;
        }
    }
}
