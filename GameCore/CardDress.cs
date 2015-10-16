using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class CardDress
    {
        public List<Card> ActualCards=new List<Card>();
        public Card VirtualCard;
        public CardDress(Card dealedCard)
        {
            ActualCards.Add(dealedCard);
            VirtualCard = dealedCard;
        }
        public CardDress(List<Card> actualCards, Card virtualCard)
        {
            ActualCards = actualCards;
            VirtualCard = virtualCard;
        }
    }
}
