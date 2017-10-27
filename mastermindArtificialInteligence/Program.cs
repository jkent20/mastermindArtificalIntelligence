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
            UserInputAndMenu userInputAndMenu = new UserInputAndMenu();
            Board board = new Board();
            int roundCounter = 0;

            board.RandomAnswer();

            board.PrintBoard();

            while (roundCounter < 12) {

                string[] inputs = new string[4];

                userInputAndMenu.DrawMenu();

                for (int i = 0; i < inputs.Length; i++) {
                    inputs[i] = userInputAndMenu.AcceptUserInput();
                }

                Code guess = new Code(inputs[0], inputs[1], inputs[2], inputs[3]);

                board.SaveGuess(guess, roundCounter);
                Response response = board.TestGuess(guess);


                response.PrintResponse(response.Responses, board, roundCounter);
                board.PrintBoard();
                roundCounter++;
            }
        }
    }
}
