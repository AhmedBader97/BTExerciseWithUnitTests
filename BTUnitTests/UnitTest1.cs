using NUnit.Framework;

namespace BT.Tests
{
    [TestFixture]
    public class CardGameTests
    {
        [TestCase("2C", 2)]
        [TestCase("2D", 4)]
        [TestCase("2H", 6)]
        [TestCase("2S", 8)]
        [TestCase("TC", 10)]
        [TestCase("JC", 11)]
        [TestCase("QC", 12)]
        [TestCase("KC", 13)]
        [TestCase("AC", 14)]
        [TestCase("3C,4C", 7)]
        [TestCase("TC,TD,TH,TS", 100)]
        public void ConvertListToScore_ValidInput_ReturnsCorrectScore(string input, int expectedScore)
        {
            
            var result = Program.CardsInHand(input);

            
            Assert.AreEqual($"score is {expectedScore}", result);
        }

       
        [TestCase("2C,JK", 4)]
        [TestCase("JK,2C,JK", 8)]
        [TestCase("TC,TD,JK,TH,TS", 200)]
        [TestCase("TC,TD,JK,TH,TS,JK", 400)]
        public void JokersInList_ReturnsDoubledScore(string input, int expectedScore)
        {
          
            var result = Program.CardsInHand(input);

           
            Assert.AreEqual($"score is {expectedScore}", result);
        }

        [TestCase("1S", "Card not recognised")]      
        [TestCase("2S,1S", "Card not recognised")]
        [TestCase("3H,3H", "Cards cannot be duplicated")]
        [TestCase("4D,5D,4D", "Cards cannot be duplicated")]
        [TestCase("JK,JK,JK", "A hand cannot contain more than two Jokers")]
        [TestCase("2S|3D", "Invalid input string")]
        [TestCase("2S,2S", "Cards cannot be duplicated")]
        [TestCase("3C,4C,3C", "Cards cannot be duplicated")]
        
        public void InvalidLists_ReturnsErrorMessage(string input, string expectedErrorMessage)
        {
            
            var result = Program.CardsInHand(input);

           
            Assert.AreEqual(expectedErrorMessage, result);
        }
    }
}
