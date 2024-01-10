using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BT
{
    public class Program
    {
        public static string CardsInHand(string listOfCards)
        {
            // Check if the input contains only uppercase characters
            if (listOfCards != listOfCards.ToUpper())
            {
                return "Card not recognised";
            }

            int score = 0;

            List<int> listOfNumbers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };

            Dictionary<string, int> faceCards = new Dictionary<string, int>() { { "T", 10 }, { "J", 11 }, { "Q", 12 }, { "K", 13 }, { "A", 14 } };

            // to multiply by suit
            Dictionary<string, int> suit = new Dictionary<string, int>() { { "C", 1 }, { "D", 2 }, { "H", 3 }, { "S", 4 } };

            string[] listOfCardsSeparatedWithoutJoker = listOfCards.Split(',').Where(card => card.Trim().ToUpper() != "JK").ToArray();

            int jokerCount = listOfCards.Split(',').Count(card => card.Trim().ToUpper() == "JK");

            if (listOfCardsSeparatedWithoutJoker.Length == listOfCardsSeparatedWithoutJoker.Distinct().Count())
            {
                // return 0 if only joker is hand, with a max of 2 jokers for the hand
                if (jokerCount == 1 && listOfCardsSeparatedWithoutJoker.Length == 0 || jokerCount == 2 && listOfCardsSeparatedWithoutJoker.Length == 0)
                {
                    return "0";
                }
                else if (jokerCount > 2)
                {
                    return "A hand cannot contain more than two Jokers";
                }

                for (int i = 0; i < listOfCardsSeparatedWithoutJoker.Length; i++)
                {
                    string prefix = listOfCardsSeparatedWithoutJoker[i][0].ToString();
                    string suffix = listOfCardsSeparatedWithoutJoker[i].Substring(1);

                    // Update the patterns to match the entire string
                    bool isValidPrefix = Regex.IsMatch(prefix, @"^[2-9TJQKA]+$|^[CDHS]+$", RegexOptions.IgnoreCase);
                    bool isValidSuffix = Regex.IsMatch(suffix, @"^[2-9TJQKA]+$|^[CDHS]+$", RegexOptions.IgnoreCase);

                    bool isInListOfCards = int.TryParse(prefix, out int prefixValue) && listOfCards.Contains(prefix);

                   

                    if (!isValidPrefix || !isValidSuffix)
                    {
                        
                        if (!isValidPrefix || isValidSuffix)
                        {
                            return "Card not recognised";
                        }
                        else
                        {
                            return "Invalid input string";
                        }
                    }

                    // if number exists in list & suit exists in dictionary
                    else if (listOfNumbers.Contains(prefixValue) && suit.ContainsKey(suffix))
                    {
                        // if there is no joker in hand, just add values normally, otherwise multiply by 2 for each joker (max 2)
                        if (jokerCount < 1)
                            score += prefixValue * suit[suffix];
                        else
                        {
                            score += (2 * jokerCount) * prefixValue * suit[suffix];
                        }
                    }
                    // if face card exists & suit exists in dictionary
                    else if (faceCards.ContainsKey(prefix) && suit.ContainsKey(suffix))
                    {
                        if (jokerCount < 1)
                            score += faceCards[prefix] * suit[suffix];
                        else
                        {
                            score += (2 * jokerCount) * faceCards[prefix] * suit[suffix];
                        }
                    }
                    else
                    {
                        if (i == listOfCardsSeparatedWithoutJoker.Length - 1)
                        {
                            return "Card not recognised";
                        }
                        else
                        {
                            return "Invalid input string";
                        }
                    }
                }

                return "score is " + score;
            }
            else
            {
                return "Cards cannot be duplicated";
            }
        }







        static void Main(string[] args)
        {
            var listofCards = "1S";
            Console.WriteLine(CardsInHand(listofCards));
        }
    }
}
