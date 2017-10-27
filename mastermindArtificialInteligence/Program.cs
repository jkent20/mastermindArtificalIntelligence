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

            while (board.Finished == false) {
                roundCounter++;

                string[] inputs = new string[4];

                userInputAndMenu.DrawMenu();

                for (int i = 0; i < inputs.Length; i++) {
                    inputs[i] = userInputAndMenu.AcceptUserInput();
                }

                Guess guess = new Guess(inputs[0], inputs[1], inputs[2], inputs[3]);
                Console.WriteLine(string.Join(". ", guess.Colours));

                Response response = board.TestGuess(guess);

                response.PrintResponse(response.Responses);
                if (roundCounter == 12) {
                    break;
                }
            }
        }
    }
}
