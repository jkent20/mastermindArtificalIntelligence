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
            int numOfColours = 6;
            UserInputAndMenu userInputAndMenu = new UserInputAndMenu();
            Board board = new Board();
            CodeBreaker codeBreaker = new CodeBreaker(numOfColours);
            int roundCounter = 0;

            board.RandomAnswer();

            board.PrintBoard();

            while (roundCounter < 12) {

                string[] inputs = new string[4];

                //userInputAndMenu.DrawMenu();
                Console.WriteLine();
                Console.WriteLine("Round {0}", roundCounter + 1);
                Console.WriteLine();

                inputs = userInputAndMenu.AcceptUserInput(codeBreaker.MakeGuess(roundCounter));


                Code guess = new Code(inputs[0], inputs[1], inputs[2], inputs[3]);

                board.SaveGuess(guess, roundCounter);
                Response response = board.TestGuess(guess);


                response.PrintResponse(response.Responses, board, roundCounter);
                board.PrintBoard();

                codeBreaker.UpdatePossibleCodes(guess.Colours, response, board, roundCounter);
                Console.ReadLine();
                roundCounter++;
                if (board.Victory) {
                    break;
                }
            }

            Console.WriteLine("");
            Console.WriteLine(new string('=', board.BoardWidth));
            Console.WriteLine(new string('=', board.BoardWidth));

            if (board.Victory == false) {
                Console.WriteLine("You failed!");
            }
            else {
                Console.WriteLine("Congratulations");
                Console.WriteLine("You won in {0} rounds", roundCounter);
            }

            Console.WriteLine(new string('=', board.BoardWidth));
            Console.WriteLine(new string('=', board.BoardWidth));

            Console.ReadLine();
        }
    }
}
