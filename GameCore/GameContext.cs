using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YangPeng.Extensions;

namespace GameCore
{
    public class GameContext
    {
        /// <summary>
        /// 发牌堆
        /// </summary>
        public List<Card> availableCards = new List<Card>();
        /// <summary>
        /// 弃牌堆
        /// </summary>
        public List<Card> droppedCards = new List<Card>();
        public List<Player> players = new List<Player>();
        public Player currentPlayer;
        public Card currentCard;

        public List<Player> targets = new List<Player>();

        public List<Card> dealedCardsInTurn = new List<Card>();

        public event EventHandler<EventArgs> OnBeginPrepareCards;
        public event EventHandler<EventArgs> OnEndPrepareCards;

        public event EventHandler<EventArgs> OnBeginGetCards;
        public event EventHandler<EventArgs> OnEndGetCards;

        public event EventHandler<EventArgs> OnBeginSelectOneCard;
        public event EventHandler<EventArgs> OnCannotSelectThisCard;
        public event EventHandler<EventArgs> OnEndSelectOneCard;

        public event EventHandler<EventArgs> OnBeginSetTarget;
        public event EventHandler<EventArgs> OnEndSetTarget;

        public event EventHandler<EventArgs> OnBeginDealCard;
        public event EventHandler<EventArgs> OnEndDealCard;
        public event EventHandler<EventArgs> OnDealCardsFinish;

        public event EventHandler<EventArgs> OnBeginDropCard;
        public event EventHandler<EventArgs> OnCardsDropped;
        public event EventHandler<EventArgs> OnEndDropCard;

        public event EventHandler<EventArgs> OnGameOver;

        public int round = 1;//第几轮
        public AutoResetEvent autoEvent = new AutoResetEvent(false);
        public bool isFinishDeal = false;
        public GameState State;

        public CardRules Rules;

        public int CardsCountToDrop => currentPlayer.Cards.Count - currentPlayer.Blood;

        public GameContext()
        {
            #region 添加牌
            AddCards(new Acid(), 24);
            AddCards(new Base(), 24);
            AddCards(new Glucose(), 8);

            AddCards(new 重铬酸钾(), 1);
            AddCards(new 王水(), 6);
            AddCards(new EDTA(), 1);
            AddCards(new 万碱齐发(), 2);
            AddCards(new 硫酸飞溅(), 2);
            AddCards(new 硅藻土(), 3);
            AddCards(new 冷凝回流(), 5);
            AddCards(new 负催化剂(), 3);

            AddCards(new 试管(), 1);
            AddCards(new pH计(), 1);
            AddCards(new 酸式滴定管(), 1);
            AddCards(new 碱式滴定管(), 1);
            AddCards(new 锥形瓶(), 1);
            AddCards(new 滴管(), 1);
            AddCards(new 温度计(), 1);

            AddCards(new 聚四氟乙烯(), 1);
            AddCards(new 废液缸(), 1);
            AddCards(new 玻璃塞(), 1);

            AddCards(new 拉曼光谱(), 1);
            AddCards(new 络合滴定(), 1);
            #endregion

            #region 添加玩家
            players.Add(new Player() { name = "A" });
            players.Add(new Player() { name = "B" });
            #endregion

            Rules = new CardRules(this);
        }
        public void GameBegin()
        {
            OnBeginPrepareCards(this, new EventArgs());
            //给每个玩家发牌
            foreach (var player in players)
            {
                player.Cards.AddRange(availableCards.TakeRandom(player.CardCountWhenBegin));
            }
            OnEndPrepareCards(this, new EventArgs());
        }
        public void TurnToFirstPlayer()
        {
            currentPlayer = players.First();
        }
        public void TurnToNextPlayer()
        {
            int index = players.IndexOf(currentPlayer);
            if (players.IndexOf(currentPlayer) < players.Count - 1)
            {
                currentPlayer = players[++index];
            }
            else
            {
                round++;
                currentPlayer = players.First();
            }
        }
        public void Prepare()
        {
            State = GameState.OnPrepare;
        }
        public void Determine()
        {
            State = GameState.OnDetermine;
        }
        public void GetCards()
        {
            State = GameState.OnGetCards;
            OnBeginGetCards(this, new EventArgs());
            for (int i = 0; i < currentPlayer.CardCountToGet; i++)
            {
                if (availableCards.Count == 0)
                {
                    RecycleCards();
                }
                currentPlayer.Cards.Add(availableCards.TakeRandom());
            }
            OnEndGetCards(this, new EventArgs());
        }
        public void SelectOneCard()
        {
            State = GameState.OnSelectCard;
            targets.Clear();
            OnBeginSelectOneCard(this, new EventArgs());
            if (autoEvent.WaitOne(15000))
            {
                if (isFinishDeal)
                {
                    isFinishDeal = false;
                    throw new FinishDealCardsException();
                }
                OnEndSelectOneCard(this, new EventArgs());
            }
            else
            {
                throw new TimeOutException();
            }
        }
        public void SelectThisOneCard(int cardIndex)
        {
            if (State==GameState.OnSelectCard)
            {
                var card = currentPlayer.Cards[cardIndex];
                if (dealedCardsInTurn.Any(c => c is Acid || c is Base) && (card is Acid || card is Base))
                {
                    OnCannotSelectThisCard(this, new EventArgs());
                }
                else
                {
                    currentCard = card;
                    autoEvent.Set();  
                }
            }
        }
        public void SetTargets()
        {
            State = GameState.OnSetTargets;
            OnBeginSetTarget(this, new EventArgs());
            if (autoEvent.WaitOne(15000))
            {
                if (isFinishDeal)
                {
                    isFinishDeal = false;
                    throw new FinishDealCardsException();
                }
                else
                {
                    OnEndSetTarget(this, new EventArgs());
                }
            }
            else
            {
                throw new TimeOutException();
            }
        }
        public void SetTheTargets(int[] playerIndexes)
        {
            if (State==GameState.OnSetTargets)
            {
                foreach (int i in playerIndexes)
                {
                    targets.Add(players[i]);
                }
                autoEvent.Set(); 
            }
        }
        public void DealOneCard()
        {
            State = GameState.OnDealCard;
            OnBeginDealCard(this, new EventArgs());
            if (autoEvent.WaitOne(15000))
            {
                if (isFinishDeal)
                {
                    isFinishDeal = false;
                    throw new FinishDealCardsException();
                }
                else
                {
                    OnEndDealCard(this, new EventArgs());
                }
            }
            else
            {
                throw new TimeOutException();
            }
        }
        public void DealThisOneCard()
        {
            if (State == GameState.OnDealCard)
            {
                currentPlayer.Cards.Remove(currentCard);
                droppedCards.Add(currentCard);
                dealedCardsInTurn.Add(currentCard);
                autoEvent.Set(); 
            }
        }
        public void Respond()
        {
            Rules.Respond();
        }

        public void FinishDealCards()
        {
            isFinishDeal = true;
            autoEvent.Set();
        }
        public void DealCardsFinish()
        {
            OnDealCardsFinish(this, new EventArgs());
        }
        public void DropCards()
        {
            State = GameState.OnDropCards;
            dealedCardsInTurn.Clear();
            OnBeginDropCard?.Invoke(this, new EventArgs());
            if (autoEvent.WaitOne(15000))
            {
            }
            else
            {
                currentPlayer.Cards.MoveRangeTo(droppedCards, 0, CardsCountToDrop);
            }
            OnEndDropCard?.Invoke(this, new EventArgs());
        }
        public void DropTheCards(int[] cardIndexes)
        {
            if (State == GameState.OnDropCards)
            {
                currentPlayer.Cards.MoveRangeTo(droppedCards, cardIndexes);
                OnCardsDropped?.Invoke(this, new EventArgs());
                if (currentPlayer.Cards.Count<=currentPlayer.Blood)
                {
                    autoEvent.Set(); 
                }
            }
        }
        public void RoundEnd()
        {

        }
        public void GameOver()
        {
            OnGameOver(this, new EventArgs());
        }

        #region 辅助程序
        private void RecycleCards()
        {
            availableCards.AddRange(droppedCards);
            droppedCards.Clear();
        }
        private void AddCards(Card card, int count)
        {
            for (int i = 0; i < count; i++)
            {
                availableCards.Add(card);
            }
        }
        #endregion
    }

    public enum GameState
    {
        OnPrepare,
        OnDetermine,
        OnGetCards,
        OnSelectCard,
        OnSetTargets,
        OnDealCard,
        OnRespond,
        OnDropCards,
    }
}
