using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class CodeBreaker {
        private int[][] possibleCodes;
        private List<int[]> updatedCodeList = new List<int[]>();
        private int[] initialGuess = new int[] { 0, 0, 1, 1 };
        private string[] colours = new string[] { "Red", "Blue", "Green", "Purple", "Orange", "Yellow" };
        private string[] tempAnswer;
        private string[] tempGuess;
        private List<string[]> outcomes = new List<string[]>();
        

        public CodeBreaker(int numColours) {
            possibleCodes = new int[1296][];

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
        }

        public int[] MakeGuess(int roundNum) {
            string[] guess = new string[4];

            if (roundNum == 0) {
                guess[0] = colours[0];
                guess[1] = colours[0];
                guess[2] = colours[1];
                guess[3] = colours[1];
                return initialGuess;
            }
            // insert here guess that will remove the most values in possibleCodes
            return MinCombination();                       

        }

        public int[] MinCombination() {

            int min = int.MaxValue;
            int[] guess = new int[4];
            string[] guessString = new string[4];

            for (int code = 0; code < possibleCodes.Length; code++) {

                int max = 0;

                foreach (var outcome in outcomes) {
                    int count = 0;

                    for (int solution = 0; solution < possibleCodes.Length; solution++) {

                        if ( outcome.SequenceEqual(CompareCodeResponses(possibleCodes[code], possibleCodes[solution]))) {
                            count++;
                        }
                    }
                    if (count > max) {
                        max = count;
                    }
                }
                if (max < min) {
                    min = max;
                    guess = possibleCodes[code];
                }
            }

            for (int i = 0; i < guess.Length; i++) {
                guessString[i] = colours[guess[i]];
            }

            return guess;
        }

        public void UpdatePossibleCodes(string[] guess, Response response, Board board, int round) {

            

            outcomes.Add(response.Responses);

            
            for (int i = 0; i < possibleCodes.Length; i++) {
                string[] comparisonAnswer = new string[4];

                for (int idx = 0; idx < possibleCodes[i].Length; idx++) {
                    int colour = possibleCodes[i][idx];
                    comparisonAnswer[idx] = colours[colour];
                }

                if (CompareCodeResponses(guess, comparisonAnswer, response.Responses)) {
                    updatedCodeList.Add(possibleCodes[i]);
                }
            }


            possibleCodes = updatedCodeList.ToArray();
            updatedCodeList = new List<int[]>();
        }

        public string[] CompareCodeResponses(int[] code, int[] solution) {
            return TestComparison(code, solution);
        }

        public bool CompareCodeResponses(string[] guess, string[] comparisonAnswer, string[] answerResponse) {

            Response comparisonResponse = new Response();
            comparisonResponse = TestComparison(guess, comparisonAnswer);

            return comparisonResponse.Responses.SequenceEqual(answerResponse);
        }

        public int TestForBlackMatches(string[] guess, string[] comparisonGuess) {
            int blackPegs = 0;
            for (int i = 0; i < guess.Length; i++) {
                if (comparisonGuess[i] == guess[i]) {
                    comparisonGuess[i] = "Guess Matched";
                    guess[i] = "Answer Matched";
                    blackPegs++;
                }
            }
            return blackPegs;
        }

        public int TestForWhiteMatches(string[] guess, string[] comparisonGuess) {
            int whitePegs = 0;
            for (int i = 0; i < comparisonGuess.Length; i++) {
                for (int j = 0; j < guess.Length; j++) {
                    if (comparisonGuess[i] == guess[j]) {
                        comparisonGuess[i] = "Part Guess Matched";
                        guess[j] = "Part Answer Matched";
                        whitePegs++;
                    }
                }
            }
            return whitePegs;
        }

        public string[] SaveInTempArray(string[] input) {

            string[] value = new string[input.Length];

            for (int i = 0; i < input.Length; i++) {
                value[i] = input[i];
            }
            return value;
        }

        public string[] SaveInTempArray(int[] input) {
            string[] value = new string[input.Length];

            for (int i = 0; i < input.Length; i++) {
                value[i] = input[i].ToString();
            }
            return value;
        }

        public Response TestComparison(string[] guess, string[] comparisonGuess) {

            tempAnswer = SaveInTempArray(comparisonGuess);
            tempGuess = SaveInTempArray(guess);

            Response response = new Response {
                BlackPegs = TestForBlackMatches(tempGuess, tempAnswer),
                WhitePegs = TestForWhiteMatches(tempGuess, tempAnswer)
            };

            for (int i = 0; i < response.BlackPegs; i++) {
                response.Responses[i] = "Black";
            }
            for (int i = 0; i < response.WhitePegs; i++) {
                response.Responses[i + response.BlackPegs] = "White";
            }

            return response;
        }

        public string[] TestComparison(int[] code, int[] solution) {

            tempAnswer = SaveInTempArray(solution);
            tempGuess = SaveInTempArray(code);

            Response response = new Response {
                BlackPegs = TestForBlackMatches(tempGuess, tempAnswer),
                WhitePegs = TestForWhiteMatches(tempGuess, tempAnswer)
            };

            for (int i = 0; i < response.BlackPegs; i++) {
                response.Responses[i] = "Black";
            }
            for (int i = 0; i < response.WhitePegs; i++) {
                response.Responses[i + response.BlackPegs] = "White";
            }

            return response.Responses;
        }




    }
}
