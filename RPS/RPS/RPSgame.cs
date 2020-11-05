using System;
using System.Collections.Generic;
using System.Text;

namespace RPS
{ 
    public class RPSgame
    {
        // Method to compare user choice to AI choice, returns result
        public string Play(string playerChoice, string comChoice, Score score)
        {

            string winLose = null;
            if (playerChoice == comChoice)
            {
                winLose = "tie";
                score.tieSet();
            }
            if (playerChoice == "r" && comChoice == "s" || playerChoice == "p" && comChoice == "r" || playerChoice == "s" && comChoice == "p")
            {
                winLose = "win";
                score.winSet();
            }
            if (playerChoice == "r" && comChoice == "p" || playerChoice == "p" && comChoice == "s" || playerChoice == "s" && comChoice == "r")
            {
                winLose = "lose";
                score.loseSet();
            }
            Console.WriteLine($"You chose {playerChoice}. The computer chose {comChoice}! You {winLose}");
            return winLose;
        }
    }
}
