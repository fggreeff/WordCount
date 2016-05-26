using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace WordCount.Library
{
    public static class PrimeCheckerUtils
    {
        //Check is a number is a prime number
        public static bool IsPrimeNumer(this int number)
        {
            
            var isPrimeNumber = true;
            var factor = number / 2;

            //0 AND 1 is not a prime number
            isPrimeNumber = (number > 1);

            for (int i = 2; i <= factor; i++)
            {
                if ((number % i) == 0)
                    isPrimeNumber = false;
            }

            return isPrimeNumber;
        }
    }
}
