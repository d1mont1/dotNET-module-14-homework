using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }

        public Player(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }

        public void DisplayCards()
        {
            Console.WriteLine($"Карты игрока {Name}:");
            foreach (var card in Cards)
            {
                Console.WriteLine($"{card.Rank} {card.Suit}");
            }
        }
    }
}
