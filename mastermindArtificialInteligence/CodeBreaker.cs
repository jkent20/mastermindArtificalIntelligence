using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class CodeBreaker {
        // initialise variables
        private int[][] possibleCodes;
        private List<int[]> updatedCodeList = new List<int[]>();
        private int[] initialGuess = new int[] { 0, 0, 1, 1 };
        private string[] colours = new string[] { "Red", "Blue", "Green", "Purple", "Orange", "Yellow" };
        private string[] tempAnswer;
        private string[] tempGuess;
        private List<string[]> outcomes = new List<string[]>();
        
        // codebreaker contructor, takes number of colour and number of pegs for easy variablility
        public CodeBreaker(int numColours, int numPegs) {
            // casts the result of numColours ^ numPegs to an int (truncates. doesn't round)
            possibleCodes = new int[(int)Math.Pow(numColours, numPegs)][];
            
            // tracks index for total number of possible Codes
            int i= 0;

            // loop over posible combinations to compile possible combinations -- WOULD NEED TO BE AJUSTED TO ALLOW FOR DIFFERENT PEG COUNT
            for (int posOne = 0; posOne < numColours; posOne++) {
                for (int posTwo = 0; posTwo < numColours; posTwo++) {
                    for (int posThree = 0; posThree < numColours; posThree++) {
                        for (int posFour = 0; posFour < numColours; posFour++) {
                            possibleCodes[i] = new int[] { posOne, posTwo, posThree, posFour };
                            i++;
                        }
                    }
                }
            }
        }
        
        // Makes guess, 0011 for first round, else runs MinCombination to find best code to guess
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

        // Calculates which combination would remove the most from the total number of still possible combinations
        public int[] MinCombination() {

            // initialise variales
            int min = int.MaxValue;
            int[] guess = new int[4];
            string[] guessString = new string[4];

            // loop over all possible codes left
            for (int code = 0; code < possibleCodes.Length; code++) {

                int max = 0;

                // loop over outcomes to check which codes work with the current and with previous responses
                foreach (var outcome in outcomes) {
                    int count = 0;

                    // loop over all possible codes left
                    // needs two of these to check one against the other and see which guess would remove the most from the list
                    for (int solution = 0; solution < possibleCodes.Length; solution++) {

                        if ( outcome.SequenceEqual(CompareCodeResponses(possibleCodes[code], possibleCodes[solution]))) {
                            count++;
                        }
                    }
                    // save number that would have satisfied the condition
                    if (count > max) {
                        max = count;
                    }
                }
                // if number that satisfied the condition is less than the last number that satisfied the condition, overwrite 'min'
                // and set guess to equal current code
                if (max < min) {
                    min = max;
                    guess = possibleCodes[code];
                }
            }

            return guess;
        }

        // remove from possible code list those which no longer satisfy response
        public void UpdatePossibleCodes(string[] guess, Response response, Board board, int round) {
            
            // append to outcomes current resposne
            outcomes.Add(response.Responses);

            // loop over all possible codes
            for (int i = 0; i < possibleCodes.Length; i++) {

                // initialise variables
                string[] comparisonAnswer = new string[4];

                // loop over current possible code array (possibleCodes[i]) and save in comparisonAnswer
                for (int idx = 0; idx < possibleCodes[i].Length; idx++) {
                    int colour = possibleCodes[i][idx];
                    comparisonAnswer[idx] = colours[colour];
                }

                // if code works, append to possible code list
                if (CompareCodeResponses(guess, comparisonAnswer, response.Responses)) {
                    updatedCodeList.Add(possibleCodes[i]);
                }
            }

            // convert list to array, reset list to empty
            possibleCodes = updatedCodeList.ToArray();
            updatedCodeList = new List<int[]>();
        }

        // run test comparison code ( 2 inputs)
        public string[] CompareCodeResponses(int[] code, int[] solution) {
            return TestComparison(code, solution);
        }

        // run test comparison and check for matches (3 inputs)
        public bool CompareCodeResponses(string[] guess, string[] comparisonAnswer, string[] answerResponse) {

            Response comparisonResponse = new Response();
            comparisonResponse = TestComparison(guess, comparisonAnswer);

            return comparisonResponse.Responses.SequenceEqual(answerResponse);
        }

        // test for black matches
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

        // test for white matches
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

        // save in temp array (string)
        public string[] SaveInTempArray(string[] input) {

            string[] value = new string[input.Length];

            for (int i = 0; i < input.Length; i++) {
                value[i] = input[i];
            }
            return value;
        }

        // save in temp array (int)
        public string[] SaveInTempArray(int[] input) {
            string[] value = new string[input.Length];

            for (int i = 0; i < input.Length; i++) {
                value[i] = input[i].ToString();
            }
            return value;
        }

        // TestComparison code to gain possible code answer (sting)
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

        // TestComparison (int)
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
