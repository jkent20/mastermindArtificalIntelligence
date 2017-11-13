using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class Board {
        private Code answer = new Code();
        private string[] tempAnswer = new string[4];
        private string[] tempGuess = new string[4]; 
        private string[][] rounds = new string[][] {
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
            new string[8] {"", "", "", "", "", "", "", ""},
        };
        private int boardWidth = 80;
        private bool victory = false;
        private int numberOfColours = 6;

        public int NumberOfColours {
            get { return numberOfColours; }
        }

        public int BoardWidth {
            get { return boardWidth; }
        }

        public bool Victory {
            get { return victory; }
        }

        public string[][] Rounds {
            get { return rounds;  }
            set { rounds = value; }
        }

        public void RandomAnswer() {
            // generate randon answer here
            string[] availableColours = { "Red", "Blue", "Green", "Purple", "Orange", "Yellow" };
            Random rng = new Random();


            for (int i = 0; i < 4; i++) {
                int choice = rng.Next(0, 5);
                answer.Colours[i] = availableColours[choice];
            }

            return;
        }

        public void SaveGuess(Code guess, int round) {
            for (int i = 0; i < guess.Colours.Length; i++) {
                rounds[round][i] = guess.Colours[i];
            }
        }

        public int TestForBlackMatches(string[] guess, string[] answer) {
            int blackPegs = 0;
            for (int i = 0; i < guess.Length; i++) {
                if (answer[i] == guess[i]) {
                    answer[i] = "Guess Matched";
                    guess[i] = "Answer Matched";
                    blackPegs++;
                }
            }
            return blackPegs;
        }

        public int TestForWhiteMatches(string[] guess, string[] answer) {
            int whitePegs = 0;
            for (int i = 0; i < answer.Length; i++) {
                for (int j = 0; j < guess.Length; j++) {
                    if (answer[i] == guess[j]) {
                        answer[i] = "Part Guess Matched";
                        guess[j] = "Part Answer Matched";
                        whitePegs++;
                    }
                }
            }
            return whitePegs;
        }

        public string[] saveInTempArray(string[] input) {

            string[] value = new string[input.Length]; 

            for (int i = 0; i < input.Length; i++) {
                value[i] = input[i];
            }
            return value;
        }

        public Response TestGuess(Code guess) {

            tempAnswer = saveInTempArray(answer.Colours);
            tempGuess = saveInTempArray(guess.Colours);

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


            if (response.BlackPegs == 4) {
                victory = true;
            }
            return response;
        }

        public void PrintBoard() {

            string[] headers = { "Guesses", "Responses" };
            
            PrintLine();
            PrintRow(headers);
            PrintLine();
            for (int i = 0; i < rounds.Length; i++) {
                PrintRow(rounds[i]);
            }

            PrintLine();

        }

        private void PrintLine() {
            Console.WriteLine(new string('-', boardWidth));
        }

        private void PrintRow(params string[] columns) {
            int width = (boardWidth - columns.Length) / columns.Length;
            string row = "|";
            int idx = 0;

            foreach (string column in columns) {
                idx++;
                row += AlignCentre(column, width) + "|";
                if (idx == (columns.Length / 2)) {
                    row += "|";
                }
            }

            Console.WriteLine(row);
        }

        private string AlignCentre(string text, int width) {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text)) {
                return new string(' ', width);
            }
            else {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        
    }
}
