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
        public event EventHandler<EventArgs> OnDefendResult;
        public event EventHandler<EventArgs> OnTolerateResult;
        public event EventHandler<EventArgs> OnDropBlood;
        public event EventHandler<EventArgs> OnWrongCard;

        private GameContext _gctx;

        public Player currentTarget;
        public Card offenderCard;
        public Card defenderCard;

        public bool IsTolerated = false;

        public bool IsKitEnable = true;

        public CardRules(GameContext context)
        {
            _gctx = context;
        }
        public void Respond()
        {
            offenderCard = _gctx.cardDress.VirtualCard;
            foreach (var t in _gctx.targets)
            {
                currentTarget = t;
                RespondOne();
            }
        }

        public bool RespondOne()
        {
            bool isDefended = false;
            OnBeginDefend?.Invoke(this, new EventArgs());
            if (_gctx.autoEvent.WaitOne(15000) && !IsTolerated)//超时或点击不出牌
            {
                DefendResult();
                isDefended = true;
            }
            else
            {
                TolerateResult();
            }
            OnEndDefend(this, new EventArgs());
            return isDefended;
        }

        public void Defend(int cardIndex)
        {
            defenderCard = currentTarget.Cards[cardIndex];
            bool canDefend=false;
            if (_gctx.TurnCtx.IsFor硅藻土 && _gctx.TurnCtx.needCardType == null)
            {
                if (defenderCard is Acid || defenderCard is Base)
                {
                    canDefend = true;
                    _gctx.TurnCtx.needCardType = defenderCard.GetType();
                }
            }
            else if (_gctx.TurnCtx.needCardType != null)
            {
                canDefend = defenderCard.GetType() == _gctx.TurnCtx.needCardType;
            }
            else
            {
                canDefend = CanDefend(offenderCard, defenderCard);
            }
            if (canDefend)
            {
                currentTarget.Cards.Remove(defenderCard);
                _gctx.droppedCards.Add(defenderCard);
                _gctx.autoEvent.Set();
            }
            else
            {
                OnWrongCard(this, new EventArgs());
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
            else if (offender is Base)
            {
                return defender is Acid;
            }
            else if (offender is KitCard)
            {
                return defender is EDTA;
            }
            return false;
        }
        void TolerateResult()
        {
            OnTolerateResult(this, new EventArgs());
            if (offenderCard is Acid)
            {
                currentTarget.DropBlood(_gctx.TurnCtx.AcidDropBloodNum);
                OnDropBlood(this, new EventArgs());
            }
            else if (offenderCard is Base)
            {
                currentTarget.DropBlood();
                OnDropBlood(this, new EventArgs());
            }
            else if (offenderCard is 重铬酸钾)
            {
                _gctx.TurnCtx.AcidDropBloodNum = 2;
            }
            else if (offenderCard is 硅藻土)
            {
                if (_gctx.TurnCtx.IsFor硅藻土)
                {
                    currentTarget.DropBlood();
                    OnDropBlood(this, new EventArgs());
                }
                else
                {
                    new KitDeal.For硅藻土(_gctx).Deal();
                }
            }
        }
        public void PlainResult()
        {
            if (_gctx.targets.Any())
            {
                currentTarget = _gctx.targets.First();

                OnTolerateResult(this, new EventArgs());
                if (offenderCard is Glucose)
                {
                    currentTarget.AddBlood();
                }
            }
        }
        void DefendResult()
        {
            OnDefendResult(this, new EventArgs());
        }

        public void DropExtraCards()
        {

        }
    }
}
