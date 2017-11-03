using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class CodeBreaker {
        private int[][] possibleCodes;
        private int[] initialGuess = new int[] { 1, 1, 2, 2 };

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

        public int[] makeGuess(int roundNum) {

        }


    

    }
}
