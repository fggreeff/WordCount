using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordCount.Library;

namespace WordCountApplication.Tests
{
    [TestFixture]
   public  class PrimeNumberTests
    {
        //NOTE: I am keeping all tests so you can follow my thought process. 


        //Find if a number is a prime number
        [Test]
        public void Check_For_PrimeNumber([Values(3, 5, 7, 13, 17)] int number)
        {
            
            //Arrange
            bool isPrimeNumber = true;
            int factor;
            //Act
            factor = number / 2;

            //0 AND 1 IS NOT A PRIME NUMBER
            isPrimeNumber = (number > 1);

            for (int i = 2; i <= factor; i++)
            {
                if ((number % i) == 0)
                    isPrimeNumber = false;
            }

            //Assert
            Assert.IsTrue(isPrimeNumber);
        }

        //Using class level implementation. The above test is usually improved and changed to work as below. 
        //Find if a number is a prime number
        [Test]
        public void Check_For_PrimeNumber_UsingClass([Values(7)] int number)
        {

            //Arrange
            bool isPrime = false;
            //Act
            isPrime = number.IsPrimeNumer();
            //Assert
            Assert.IsTrue(isPrime);
        }



            
    }
}
