using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialise variables and objects
            int maxRoundNumber = 12;
            int numOfColours = 6;
            int numberOfPegs = 4;
            UserInputAndMenu userInputAndMenu = new UserInputAndMenu();
            Board board = new Board(maxRoundNumber);
            CodeBreaker codeBreaker = new CodeBreaker(numOfColours, numberOfPegs);
            int roundCounter = 0;

            // create random code to be broken
            board.RandomAnswer();

            // print empty board
            board.PrintBoard();

            // while round is less than maxRoundNumber + 1 (arrays start at 0)
            while (roundCounter < maxRoundNumber) {


                string[] inputs = new string[4];

                // display to user: round number.
                Console.WriteLine();
                Console.WriteLine("Round {0}", roundCounter + 1);
                Console.WriteLine();

                // accept input from code breaking AI - treated like user input to keep code breaker and code master as separate as possible.
                // saves in array, checks input is in correct format, AI submits numbers and is converted to colours here.
                inputs = userInputAndMenu.AcceptUserInput(codeBreaker.MakeGuess(roundCounter));

                // initialise new code with input parameters
                // accepted strings not array to minimise changes needed to remove AI component
                Code guess = new Code(inputs[0], inputs[1], inputs[2], inputs[3]);

                // save the guess code in the object board, then run method in board called TestGuess.
                // save output in Response object
                board.SaveGuess(guess, roundCounter);
                Response response = board.TestGuess(guess);

                // save response in correct place on grid and reprint board.
                response.PrintResponse(response.Responses, board, roundCounter);
                board.PrintBoard();

                // based on response: remove all codes that can not fit that response.
                codeBreaker.UpdatePossibleCodes(guess.Colours, response, board, roundCounter);

                // pause program until enter is pressed
                Console.ReadLine();
                
                //increment round counter and check for victory condition.
                roundCounter++;
                if (board.Victory) {
                    break;
                }
            }

            // run after game has stopped
            // print new line and 2 lines of '=' to create a defined visual end to the game
            Console.WriteLine("");
            Console.WriteLine(new string('=', board.BoardWidth));
            Console.WriteLine(new string('=', board.BoardWidth));

            // if round maxRoundNumber was reached without an answer being provided print "You failed!" else congratulate and display number of rounds took to guess correctly
            if (board.Victory == false) {
                Console.WriteLine("You failed!");
            }
            else {
                Console.WriteLine("Congratulations");
                Console.WriteLine("You won in {0} rounds", roundCounter);
            }

            // print 2 lines of '=' to define clear visual end
            Console.WriteLine(new string('=', board.BoardWidth));
            Console.WriteLine(new string('=', board.BoardWidth));

            // pause program.
            Console.ReadLine();
        }
    }
}
