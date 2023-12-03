using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int GetCardValue()
        {
            string[] ranks = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
            return Array.IndexOf(ranks, Rank);
        }
    }
}
