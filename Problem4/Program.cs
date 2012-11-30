using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* This program is designed to provide an answer to Problem 4 on Project Euler: 

A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 
9009 = 91 × 99.

Find the largest palindrome made from the product of two 3-digit numbers.

 * I will explain the algorithm more in depth as I go along, but, simply, we first find successively smaller palindromes
 * until we find one that can be written as the product of two 3 digit numbers.
 * 
 * The algorithm above has been thrown out due to complexity issues (specifically, finding the factors of a product is 
 * believed to be nontrivial). The revised alogrithm finds the two largest factors that form a palindrome and then test if it 
 * is the largest one found. Since both of a pair of factors of one number must be larger than the least of a pair of another,
 * the complexitiy is greatly reduced each time a new palindrome is found.
 * */

namespace Problem4
{
    class Finding6DigitPalindromes
    {
        static void Main(string[] args)
        {
            // Initializing our main variables
            int bigFactor = 999;    // Theoretically, the larger of the two factors, also the upper bound for all later factors
            int smallFactor = 100;  // Theoretically, the smaller of the two factors, also the lower bound for all later factors
            int palindrome = -1;    // The largest palindrome yet found
            int palinTest;          // The current product being tested as a plaindrome

            // And here's the for loops that'll run through the algorithm better than I can explain it
            // This is the loop that controls the bigger of the two factors
            for (int i = bigFactor; i > smallFactor; i--)
            {
                // This is the loop that controls the smaller of the two factors
                for (int j = i; j > smallFactor; j--)
                {
                    // First, let's make a product!
                    palinTest = i * j;

                    // Remember, we only care about 6-digit numbers here
                    if (palinTest / 100000 == 0)
                    {
                        // Can't get a bigger number by making a factor smaller...
                        break;
                    }
                    // But is it a palindrome?
                    if (isPalindrome(palinTest))
                    {
                        // We've got a new palindrome in town, but can it beat the current king?
                        if (palinTest > palindrome)
                        {
                            // The king is dead, long live the king!
                            palindrome = palinTest;

                            // He has a new sheriff
                            bigFactor = i;
                            // And a new...jester?
                            smallFactor = j;
                            // And we're done here (a bigger palindrome won't be found using this new bigFactor)
                            break;
                        }
                        else
                        {
                            // Well the challenger is dead and we won't find a bigger palindrome here so...
                            break;
                        }
                    }
                }
            }
            // Sanity check and output results
            if (palindrome < 0 || bigFactor * smallFactor != palindrome)
            {
                Console.Out.WriteLine("We done fucked up! D:");
                Console.Out.WriteLine("palindrome = {0}, bigFactor = {1}, smallFactor = {2}", palindrome, bigFactor, smallFactor);
                Console.In.ReadLine();
                return;
            }

            Console.Out.WriteLine("Hey, we finished!");
            Console.Out.WriteLine("Is the palindrome: {0}", palindrome);
            string ans = Console.In.ReadLine();

            if (ans != "yes")
            {
                Console.Out.WriteLine("Dang, sorry. We'll just have to try harder. v_v");
            }
            else
            {
                Console.Out.WriteLine("Yay, we did it! ^_^");
            }
            Console.In.ReadLine();
        }

        static bool isPalindrome(int test)
        {
            // Checks to see if a 6-digit number is a palindrome
            // We'll do some input testing cause sometimes users are idiots...
            if (test / 100000 == 0)
            {
                // We should never be here...
                return false;
            }

            // Now we test the pairs that should match, first up, the most and least significant digits
            // (Note, I could write this as a single test, but the code looks very bad that way)
            if (test / 100000 != test % 10)
                return false;
            
            // Good, throw tem away
            test = (test % 100000) / 10;
            
            // Now the second ones
            if (test / 1000 != test % 10)
                return false;

            // Now we're getting somewhere, throw them away
            test = (test % 1000) / 10;

            // Last ones
            if (test / 10 != test % 10)
                return false;
            
            // Killer, we've got ourselves a palindrome here boys!
            return true;
        }
        static void testPalinTester()
        {
            // Just testing to make sure my logic is right cause, if I'm wrong, we'll be here for awhile...
            int test = 543210; // Each digit is the power of ten
            int top, bottom;        // The top and bottom of the number

            // First pass
            top = test / 100000;
            bottom = test % 10;
            Console.Out.WriteLine("Testing: {0}. First is {1} and last is {2}", test, top, bottom);

            // Second pass
            test = (test % 100000) / 10;
            top = test / 1000;
            bottom = test % 10;
            Console.Out.WriteLine("Testing: {0}. First is {1} and last is {2}", test, top, bottom);

            // Final pass
            test = (test % 1000) / 10;
            top = test / 10;
            bottom = test % 10;
            Console.Out.WriteLine("Testing: {0}. First is {1} and last is {2}", test, top, bottom);
        }
    }
}
