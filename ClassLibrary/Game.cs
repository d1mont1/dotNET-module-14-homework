using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Game<T> where T : Card
    {
        public List<Player> Players { get; set; }
        public List<T> Deck { get; set; }

        public Game(List<Player> players)
        {
            Players = players;
            Deck = new List<T>();
        }

        public void CreateDeck() //Создание Колоды
        {
            string[] suits = { "Пики", "Трефы", "Червы", "Бубны" };
            string[] ranks = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            int maxCards = 36;
            int addedCards = 0;

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    T card = (T)Activator.CreateInstance(typeof(T), suit, rank);
                    Deck.Add(card);
                    addedCards++;

                    if (addedCards >= maxCards)
                    {
                        return;
                    }
                }
            }
        }

        public void ShuffleDeck() //Перемешка Колоды
        {
            Random rnd = new Random();
            int n = Deck.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = Deck[k];
                Deck[k] = Deck[n];
                Deck[n] = value;
            }
        }

        public void DealCards() //Раздача карт игрокам
        {
            int cardsPerPlayer = Deck.Count / Players.Count;
            int index = 0;

            foreach (var player in Players)
            {
                player.Cards.AddRange(Deck.GetRange(index, cardsPerPlayer));
                index += cardsPerPlayer;
            }
        }

        public void PlayGame() //Старт самой игры
        {
            while (Players.TrueForAll(player => player.Cards.Count > 0))
            {
                foreach (var player in Players) //Вывод карт каждого игрока
                {
                    Console.WriteLine($"{player.Name} имеет следующие карты:");
                    player.DisplayCards();
                    Console.WriteLine();
                }

                List<T> cardsInPlay = new List<T>();

                foreach (var player in Players)
                {
                    if (player.Cards.Count > 0)
                    {
                        cardsInPlay.Add((T)player.Cards[0]);
                        player.Cards.RemoveAt(0);
                        Console.WriteLine($"{player.Name} кладет карту на стол.");
                    }
                }

                if (cardsInPlay.Count > 1)
                {
                    T maxCard = cardsInPlay[0];
                    List<Player> roundWinners = new List<Player>();

                    foreach (var card in cardsInPlay)
                    {
                        if (card.GetCardValue() > maxCard.GetCardValue())
                        {
                            maxCard = card;
                        }
                    }

                    roundWinners = Players.FindAll(player => player.Cards.Contains(maxCard));

                    if (roundWinners.Count == 1)
                    {
                        Player winner = roundWinners[0];
                        Console.WriteLine($"{winner.Name} забирает карты.");

                        foreach (var card in cardsInPlay)
                        {
                            winner.Cards.Add(card);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Произошло равенство карт! Все карты возвращаются в колоду.");
                        Deck.AddRange(cardsInPlay);
                    }
                }
            }

            Player gameWinner = Players[0];
            foreach (var player in Players)
            {
                if (player.Cards.Count > gameWinner.Cards.Count)
                {
                    gameWinner = player;
                }
            }

            Console.WriteLine($"Победитель: {gameWinner.Name}");
        }
    }
}
