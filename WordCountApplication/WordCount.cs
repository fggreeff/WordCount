using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordCount.Library;

namespace WordCountApplication
{
    public partial class WordCount : Form
    {

        TextBook myTextBook;
        WordCountUtils myWordCountUtils;

        public WordCount()
        {
            InitializeComponent();
        }




        //Word count Option 1 (read summary region)
        private void btnWordCount_Click(object sender, EventArgs e)
        {
            //Clear coutput and set objects to null
            resetValues();

            //Read text file
            ReadFileUsingStream(myTextBook);

            //Cleanup text. Todo: Complete this
            CleanupText(myTextBook);

            //Get dictionary of words
            GetWordCount(myTextBook);

            //Display Data
            OutputData();
        }


        //Read a text file
        private void ReadFileUsingStream(TextBook textBook)
        {
            using (StreamReader myReader = new StreamReader(GetFile()))
            {
                textBook.RawText = myReader.ReadToEnd(); //await myReader.ReadToEndAsync();
            }
        }


        //Ignore punctuation and capitalisation
        //I started implementing this to make it more user friendly and get more accurate results. I was trying to avoid having to use regex because of the impact it has on performance.
        //It might be worth using regex (\w+) and strip out words rather than trying to remove Romans, Punctuation and some special characters
        private void CleanupText(TextBook textBook)
        {
            //Remove modern roman numerals (NOTE: Case insensitive)
           textBook.RawText = myWordCountUtils.RegexCleaner(textBook.RawText, textBook.Pattern);

            //Format any capitalisation to lower case in text file
            textBook.RawText = textBook.RawText.ToLower();

            //Remove Punctuation from text file
            textBook.RawText = myWordCountUtils.RemovePunctuation(textBook.RawText);

            //Remove new line, tab and numbers
            textBook.RawText = myWordCountUtils.CleanDistinctCharacters(textBook.RawText, textBook.StripCharacters);
        }

        private void GetWordCount(TextBook textBook)
        {
            //Split text file by spaces into a list and remove empty entries
            var myWordList = myWordCountUtils.SplitWords(textBook.RawText);

            //Add words to a dictionary object with the corresponding word count
            myWordCountUtils.WordCountDictionary(myWordList, textBook.WordDictionary);
        }

        //Order and display the data in a readable format
        private void OutputData()
        {
            //Sort dictionary
            var pairs = from entry in myTextBook.WordDictionary orderby entry.Value descending select entry;

            
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine("Word\t\t\tCount\tIsPrime");
            sbOutput.AppendLine("-- -- --\t\t\t-- - --\t-- -- --");
            foreach (var word in pairs)
            {
                sbOutput.AppendLine(string.Format("{0}\t\t\t{1}\t{2}", word.Key, word.Value, word.Value.IsPrimeNumer()/*Get Prime number*/));
            }
            //Display output
            txtOutput.Text = sbOutput.ToString();
        }

    

        //Overloaded method taking a text string as param
        private void GetWordCount(string text, TextBook textBook)
        {
            //Split text file by spaces into a list and remove empty entries
            var myWordList = myWordCountUtils.SplitWords(text);

            //Add words to a dictionary object with the corresponding word count
            myWordCountUtils.WordCountDictionary(myWordList, textBook.WordDictionary);

        }

        //Word count option 2 (read summary region)
        private void btnWordCountTwo_Click(object sender, EventArgs e)
        {
            //Clear coutput and set objects to null
            resetValues();

            myTextBook.RawTextArray = new string[50000]; //Allocating one contiguous block of memory to this array to keep it simple and fast.
            myTextBook.RawTextArray = File.ReadAllLines(GetFile());
            
            //Loop through one line at a time and process one line at a time
            Parallel.For(0, myTextBook.RawTextArray.Length, x => { GetWordCount(myTextBook.RawTextArray[x], myTextBook); });

            //Display Data
            OutputData();
        }

        //Clear output and instantiate objects
        private void resetValues()
        {
            txtOutput.Clear();//Clear output content
            myTextBook = new TextBook();
            myWordCountUtils = new WordCountUtils();
        }

        #region Close application
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
        }

        #endregion


        private string GetFile()
        {
            using (OpenFileDialog myFileDialog = new OpenFileDialog() { Filter = "Text document|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (myFileDialog.ShowDialog() == DialogResult.OK)
                    return myFileDialog.FileName;
                else
                    return "Choose a text file";
            }
        }



        /*
NOTE: COPY TO WORD DOC FOR EASE OF READING.
         
 My initial thought process was around memory allocation and what would be the best practice for the type and size of files we are processing. I thought it would be reading data into a stringbuilder or reading the data in as bytes. 
 Consulting Mr. Google I came up with the following.
 For the purpose of this exercise I made the following assumptions. In both cases we will always want to read the whole file. We only need to read the file once, rather than making continuous calls. 
 
 [] Option 1: uses the streamreader ReadToEnd function. It will allocate the buffer size automatically, initially starting with 4096 bytes. 
 * Pros: ReadToEnd assumes that the stream knows when it has reached an end. We only read the document once. Shown from this source: http://stackoverflow.com/questions/7387085/how-to-read-an-entire-file-to-a-string-using-c-sharp, the steamreader is faster than using the File.ReadAllLines (used in option 2).  
 * Cons:  ReadToEnd might block indefinitely because it does not reach an end, and should be avoided. If you manipulate the position of the underlying stream after reading data into the buffer, the position of the underlying stream might not match the position of the internal buffer. 
 I process the data in a string as a whole. This way I split the data by a space and add it directly to the IList<string> list. Thereafter, I loop through the list and add it to the word dictionary (IDictionary>string,int>). 
 * Improvements: I could use a string builder and allocate a fixed size of memory.  The stringbuilder is more useful for when we will change the strings for example, clean up the Punctuation and unwanted characters. It is bad practice reading an entire file into memory, when working with larger files this can be improved. 

 [] Option 2: uses File.ReadAllLines for reading the text. I then micro-optimize my code for speed, by pre-allocating the size of the string array. 
 * Pros: FileStream is IDisposable, so we don't need to close the steam when done. Reading an entire file into an array and then processing line-by-line using a parallel loop proved significantly more beneficial than reading a line, processing a line. Source: http://cc.davelozinski.com/c-sharp/the-fastest-way-to-read-and-process-text-files. FileStream is sensible for processing small files, unlike working with big data. 
 * Cons: We do not know the size of the file, have to estimate size of array. There are more iterations in the code. 
 I process the data in an array of strings, line by line using Parallel.For. Each line will split the data by a space and add it to an IList<string>. I loop through each individual line adding it to the word dictionary. 
 * Improvements: Could read each line in text file to get an initial idea of the array size. I would furthermore like to measure the execution time of both options.
 
 [] Alternative options: 
 Memory mapped:. Source: http://stackoverflow.com/questions/1859213/when-to-use-memory-mapped-files. Decided against using MemoryMappedFile Class as we work with small files and only need to access the file once. Memory-mapped files enable programmers to work with extremely large files because memory can be managed concurrently, and they allow complete, random access to a file without the need for seeking

 [] Future note
 The punctuation, case, special characters, \t (tabs) are messing up the results. For example, “word\t” is not the same as “word”. This is also why the output is not displayed neatly.  Unfortunately, time caught me. Nice challenge!  
         */


    }
}
