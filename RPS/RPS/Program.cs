using System;

namespace RPS
{
    public class Program
    {
        static void Main(string[] args)
        { 
            Console.WriteLine("Let's Play Rock Paper Scissors!");
            // Creates new instances of game and score
            var playerGame = new RPSgame();
            var playerScore = new Score();
            // Sets initial last user choice to null so AI knows not to consider
            string lastPlay = null;
            //Gather's user's initial choiuce
            Console.WriteLine("Choose rock (r), paper (p), or scissors(s). Enter x to quit.");
            string playerChoice = Console.ReadLine();
            // Loops through game until player chooses to quit.
            while (playerChoice != "x")
            { 
                // Sends info to AI to choose play
                string comChoice = AI.chooseRPS(lastPlay);
                // Sends player and AI choice to RPSgame to resolve win/loss
                playerGame.Play(playerChoice, comChoice, playerScore);
                // Updates last play for AI decision making
                lastPlay = playerChoice;
                // Prompts for more input/exiting game
                Console.WriteLine($"Your current record is: {playerScore.winCount} Wins - {playerScore.lossCount} Losses - {playerScore.tieCount} Ties ");
                Console.WriteLine("Play again? Choose r, p, s, or x to quit.");
                playerChoice = Console.ReadLine();
            }         
        }
    }
}

