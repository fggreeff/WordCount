using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordCount.Library;

namespace WordCountApplication.Tests
{
    [TestFixture]
    public class WordCountTests
    {

        //I deliberately left commented code in, so you can follow my dev process
        //Remove modern roman numerals (Case insensitive)
        //Source: https://www.safaribooksonline.com/library/view/regular-expressions-cookbook/9780596802837/ch06s09.html
        [Test]
        public void Remove_RomanNumerals_FollowedBy_FullStop()
        {
            //Arrange
           // string pattern = @"(?=[MDCLXVI])M*(C[MD]|D?C{0,3})(X[CL]|L?X{0,3})(I[XV]|V?I{0,3}[.])";
            string  inputText = "I. The beginning of things.II. Peter's coal-mine. I don't suppose they had...";
            string expectedOutput = "  The beginning of things.  Peter's coal-mine. I don't suppose they had...";
            var myWordCountUtils = new WordCountUtils();
            var myTextBook = new TextBook();
            //Act
            //inputText = System.Text.RegularExpressions.Regex.Replace(inputText, pattern, " ");
            inputText = myWordCountUtils.RegexCleaner(inputText, myTextBook.Pattern);

            //Assert
            Assert.AreEqual(expectedOutput, inputText);
        }


        //Remove Punctuation from text file
        [Test]
        public void Remove_Punctuation()
        {
            //Arrange
            string inputText = "CHILDREN!\r\n\r\nBy E. () [] ^ _ - ;\r\n\r\n\r\n  I.    The beginning of things.\r\n";
            string expectedOutputText = "CHILDREN\r\n\r\nBy E   ^   \r\n\r\n\r\n  I    The beginning of things\r\n";
            string outputText = "";
            var myWordCountUtils = new WordCountUtils();

            //Act
            //outputText = new string(inputText.Where(c => !char.IsPunctuation(c)).ToArray());
           outputText = myWordCountUtils.RemovePunctuation(inputText);
            

            //Assert
            Assert.AreEqual(expectedOutputText, outputText);
        }

        //Remove new line, tab and numbers
        [Test]
        public void Remove_Specific_Characters()
        {
            //Arrange
            string inputText = "CHILDREN\r\n\r\nBy E   ^   \r\n\r\n\r\n  I    The beginning of things\r\n";
            // Define characters to strip from the input and do it
            string[] stripChars = { "^", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\n", "\t", "\r" };
            string expectedOutputText = "CHILDREN    By E               I    The beginning of things  ";
            var myWordCountUtils = new WordCountUtils();


            //Act
            inputText = myWordCountUtils.CleanDistinctCharacters(inputText, stripChars);

            //Assert
            Assert.AreEqual(expectedOutputText, inputText);
        }



        //Format any capitalisation to lower case in text file
        [Test]
        public void FormatText_To_LowerCase()
        {
            //Arrange
            string inputText = "THE RAILWAY CHILDREN They were not railway children to begin with";
            string expectedOutput = "the railway children they were not railway children to begin with";
            //Act
            inputText = inputText.ToLower();
            //Assert
            Assert.AreEqual(expectedOutput, inputText);

        }

        //Split text file by spaces into a list and remove empty entries
        [Test]
        public void SplitString_By_Space_And_RemoveEmptyEntries()
        {
            //Arrange
            string inputText = "the railway children  they  ";
            string[] outputArray = {"the", "railway", "children", "they"};

            var myWordCountUtils = new WordCountUtils();

            //Act
            var myWordList = myWordCountUtils.SplitWords(inputText);
            //List<string> wordList = inputText.Split(new []{' '},StringSplitOptions.RemoveEmptyEntries).ToList();
            //Assert
            CollectionAssert.AreEqual(outputArray, myWordList);

        }



        //Add words to a dictionary object with the corresponding word frequency
        [Test]
        public void Add_Words_To_Dictionary()
        {
            //Arrange
            Dictionary<string, int> myWordDictionary = new Dictionary<string, int>();
            List<string> myWordList = new List<string>() { "the", "railway", "children", "the", "children", "father", "the" };
            Dictionary<string, int> myExpectedWordDictionary = new Dictionary<string, int>();
            myExpectedWordDictionary.Add("the", 3);
            myExpectedWordDictionary.Add("children", 2);
            myExpectedWordDictionary.Add("railway", 1);
            myExpectedWordDictionary.Add("father", 1);

            var myWordCountUtils = new WordCountUtils();
      

            //Act
            //Loop through words in word list
            myWordCountUtils.WordCountDictionary(myWordList, myWordDictionary);

            //Assert
            CollectionAssert.AreEquivalent(myExpectedWordDictionary, myWordDictionary);
            CollectionAssert.AreEqual(myExpectedWordDictionary, myWordDictionary);

        }

        //Sort Dictionary by word frequency (descending)
        [Test]
        public void Sort_Dictionary()
        {
            //Arrange
            Dictionary<string, int> myUnsortedWordDictionary = new Dictionary<string, int>();
            myUnsortedWordDictionary.Add("the", 3);
            myUnsortedWordDictionary.Add("railway", 1);
            myUnsortedWordDictionary.Add("children", 2);

            Dictionary<string, int> myExpectedWordDictionary = new Dictionary<string, int>();
            myExpectedWordDictionary.Add("the", 3);
            myExpectedWordDictionary.Add("children", 2);
            myExpectedWordDictionary.Add("railway", 1);


            //Act
            var sortedDict = from entry in myExpectedWordDictionary orderby entry.Value descending select entry;

            //Assert
            CollectionAssert.AreEquivalent(myExpectedWordDictionary, sortedDict);
            CollectionAssert.AreEqual(myExpectedWordDictionary, sortedDict);
        }

    }
}
