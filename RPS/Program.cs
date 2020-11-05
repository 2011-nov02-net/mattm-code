using System;

namespace RPS
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;
            Console.WriteLine("Let's Play Rock Paper Scissors!");
            Console.WriteLine("Would you like to play (p), or display old scores (s)?");
            input = Console.ReadLine();
            while (input != "p" || input != "s"){
                Console.WriteLine("Would you like to play (p), or display old scores (s)?");
                input = Console.ReadLine();
            }
            if (input == "p"){


            }
           else (input == "s") {

        }
    }
}
