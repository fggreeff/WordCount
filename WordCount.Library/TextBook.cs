using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Library
{
   public class TextBook
    {

       public TextBook()
       {
           WordDictionary = new Dictionary<string, int>();
       }

       private const string ROMANPATTERN = @"(?=[MDCLXVI])M*(C[MD]|D?C{0,3})(X[CL]|L?X{0,3})(I[XV]|V?I{0,3}[.])";
       public string Pattern { get { return ROMANPATTERN; }}

       private readonly string[] StripChars = { "^", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\n", "\t", "\r", "\"" };
       public string[] StripCharacters { get { return StripChars; } }


       public string RawText { get; set; }
       public IDictionary<string, int> WordDictionary { get; set; }

       public string[] RawTextArray { get; set; }
        
    }
}
