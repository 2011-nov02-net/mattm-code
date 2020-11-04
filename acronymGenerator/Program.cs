﻿using System;

namespace acronymGenerator
{
    class Program
    {       
     static void Main(string[] args)
        {
            Console.WriteLine("Please enter a term.");
            string term = Console.ReadLine();
            string[] words = term.Split(' ');
   
            foreach(var word in words){
                if(word == "and" || word == "the"){
                    continue;
                }
                string capitalWord = word.ToUpper();
                Console.Write($"{capitalWord[0]}.");
            }
        }
    }
}
