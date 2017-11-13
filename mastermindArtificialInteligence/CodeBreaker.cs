using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class CodeBreaker {
        private int[][] possibleCodes;
        private List<int[]> updatedCodeList;
        private int[] initialGuess = new int[] { 0, 0, 1, 1 };
        private string[] colours = new string[] { "Red", "Blue", "Green", "Purple", "Orange", "Yellow" };
        private Board 

        public CodeBreaker(int numColours) {
            possibleCodes = new int[numColours ^ 4][];

            int i = 0;

            for (int posOne = 0; posOne < 6; posOne++) {
                for (int posTwo = 0; posTwo < 6; posTwo++) {
                    for (int posThree = 0; posThree < 6; posThree++) {
                        for (int posFour = 0; posFour < 6; posFour++) {
                            possibleCodes[i] = new int[] { posOne, posTwo, posThree, posFour };
                            i++;
                        }
                    }
                }
            }
            Console.WriteLine(possibleCodes.Length);
        }

        public string[] makeGuess(int roundNum, string[] response) {
            string[] guess = new string[4];

            if (roundNum == 0) {
                guess[0] = colours[0];
                guess[1] = colours[0];
                guess[2] = colours[1];
                guess[3] = colours[1];
                return guess;
            }
            // insert here guess that will remove the most values in possibleCodes
            

        }

        public void updatePossibleCodes(string[] guess, string[] response, Board board) {

            Response comparedResponse = new Response();
            Code comparisonCode = new Code();

            for (int i = 0; i < possibleCodes.Length; i++) {
                string[] convertedGuess = new string[4];

                for (int j = 0; j < 4; j++) {

                    int colour = possibleCodes[i][j];
                    convertedGuess[j] = colours[colour];
                }

                comparisonCode.setCode(convertedGuess);

                comparedResponse = board.TestGuess(comparisonCode);

                if (comparedResponse.Responses == response) {
                    updatedCodeList.Add(possibleCodes[i]);
                }
            }

            possibleCodes = updatedCodeList.ToArray();

        }


    

    }
}
