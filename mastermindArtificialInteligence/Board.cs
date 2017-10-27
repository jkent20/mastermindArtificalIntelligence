using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class Board {
        private Code answer = new Code();
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

        public Response TestGuess(Code guess) {

            Response response = new Response();

            for (int i = 0; i < answer.Colours.Length; i++) {
                for (int j = 0; j < guess.Colours.Length; j++) {
                    if (answer.Colours[i] == guess.Colours[i]) {
                        response.BlackPegs++;
                    }
                    else {
                        response.WhitePegs++;
                    }
                    j++;
                }
            }

            for (int i = 0; i < response.BlackPegs; i++) {
                response.Responses[i] = "Black";
            }
            for (int i = 0; i < response.WhitePegs; i++) {
                response.Responses[i + response.BlackPegs] = "White";
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
