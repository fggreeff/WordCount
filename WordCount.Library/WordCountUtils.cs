using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordCount.Library
{
   public class WordCountUtils
    {
        //Match regex and replace unwanted pattern with space (e.g Roman Numerals)
       public string RegexCleaner(string text, string pattern)
       {
           return Regex.Replace(text, pattern, " ");
       }

       //Remove punctuation from text
       public string RemovePunctuation(string text)
       {
           return new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
       }

       //Remove distinct or special case characters from text
       public string CleanDistinctCharacters(string text, string[] stripChars)
       {
           return stripChars.Aggregate(text, (current, character) => current.Replace(character, " "));
       }

       //Split text file and return list of words
       public IList<string> SplitWords(string text)
       {
           return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
       }

       //Count list of words then add the word & total count to dictionary
       public IDictionary<string, int> WordCountDictionary(IList<string> wordList, IDictionary<string, int> myWordDictionary)
       {
           //Loop through words in word list
           foreach (var word in wordList)
           {
               //Check if the dictionary already has the word
               if (myWordDictionary.ContainsKey(word))
                   //We already have the word in the dictionary, increment the frequency of how many times the word appears
                   myWordDictionary[word]++;
               else
                   //It is a new word, add it to the dictionary with an initial count of 1
                   myWordDictionary[word] = 1;
           }

           return myWordDictionary;
       }

       public IOrderedEnumerable<KeyValuePair<string, int>> SortDictionary(IDictionary<string, int> wordDictionary)
       {
           return from entry in wordDictionary orderby entry.Value descending select entry;
       }
    }
}
