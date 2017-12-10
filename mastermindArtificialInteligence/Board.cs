using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class Board {

        // initialise variables, not including jagged array for the board as initialising this in the contructor allows for variable round lengths.
        private Code answer = new Code();
        private string[] tempAnswer = new string[4];
        private string[] tempGuess = new string[4]; 
        private string[][] rounds;
        private int boardWidth = 80;
        private bool victory = false;
        private int numberOfColours = 6;

        // contructor, initialises jagged array to print board from.
        public Board(int numberOfRounds) {

            rounds = new string[numberOfRounds][];

            for (int i = 0; i < numberOfRounds; i++) {                
                this.rounds[i] = new string[8] { "", "", "", "", "", "", "", "" };
            }
        }

        // accessors
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

        // create random code to be broken
        public void RandomAnswer() {
            // generate randon answer here
            string[] availableColours = { "Red", "Blue", "Green", "Purple", "Orange", "Yellow" };
            Random rng = new Random();

            // choose a random colour 4 times and save it in an array
            for (int i = 0; i < 4; i++) {
                int choice = rng.Next(0, 5);
                answer.Colours[i] = availableColours[choice];
            }

            return;
        }

        // save guess in variable for later use.
        public void SaveGuess(Code guess, int round) {
            for (int i = 0; i < guess.Colours.Length; i++) {
                rounds[round][i] = guess.Colours[i];
            }
        }

        // test for number of matches that are correct colour in correct place
        // reference to array is passed in to origional array is changed.
        public int TestForBlackMatches(string[] guess, string[] answer) {
            int blackPegs = 0;
            // change matched pegs so they arent matched by the white algorithm.
            for (int i = 0; i < guess.Length; i++) {
                if (answer[i] == guess[i]) {
                    answer[i] = "Guess Matched";
                    guess[i] = "Answer Matched";
                    blackPegs++;
                }
            }
            return blackPegs;
        }

        // test for number of matches that are the correct colour in the wrong place.
        // reference array is passed in so origianal array is changed.
        public int TestForWhiteMatches(string[] guess, string[] answer) {
            int whitePegs = 0;
            // for each peg in answer, check each peg in guess for match, change text if match found.
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

        // save input array in temp array so values can be changed
        public string[] SaveInTempArray(string[] input) {

            string[] value = new string[input.Length]; 

            // for each value in input, create value in output 
                // arrays are passed through as references, not doing this would allow changing of origional guess and therefore not match correctly in future
                // changing is required to check for correct number of black and white guesses.
            for (int i = 0; i < input.Length; i++) {
                value[i] = input[i];
            }
            return value;
        }

        // test input guess against answer.
        public Response TestGuess(Code guess) {

            // save answer and input guess into temp arrays
            tempAnswer = SaveInTempArray(answer.Colours);
            tempGuess = SaveInTempArray(guess.Colours);

            // initialise Response class with number of white and black pegs
            Response response = new Response {
                BlackPegs = TestForBlackMatches(tempGuess, tempAnswer),
                WhitePegs = TestForWhiteMatches(tempGuess, tempAnswer)
            };

            // for the number of black matches found, add "Black" to response array
            for (int i = 0; i < response.BlackPegs; i++) {
                response.Responses[i] = "Black";
            }
            // for number of white matches found, add "White" to response array, at number of black peg offset
            for (int i = 0; i < response.WhitePegs; i++) {
                response.Responses[i + response.BlackPegs] = "White";
            }

            // if black peg number is equal to 4, set victory to true.
            if (response.BlackPegs == 4) {
                victory = true;
            }
            return response;
        }

        // update board
        public void PrintBoard() {

            // set headers
            string[] headers = { "Guesses", "Responses" };
            
            // print header row with table lines
            PrintLine();
            PrintRow(headers);
            PrintLine();
            
            // print row containing details in the rounds jagged array (guesses and responses)
            for (int i = 0; i < rounds.Length; i++) {
                PrintRow(rounds[i]);
            }

            PrintLine();

        }

        // print line method to create new line of '-'
        private void PrintLine() {
            Console.WriteLine(new string('-', boardWidth));
        }

        // print row method to populate table
        private void PrintRow(params string[] columns) {
            // initialise variables
            int width = (boardWidth - columns.Length) / columns.Length;
            string row = "|";

            // for each coloumn, align to centre and add pipe to be board edges.
            for (int i = 0; i < columns.Length; i++) {
                row += AlignCentre(columns[i], width) + "|";
                // check if column is halfway down the board, ie between guess and response sections, if yes add second pipe for clarity
                if (i == (columns.Length / 2) - 1) {
                    row += "|";
                }
            }
            // print row
            Console.WriteLine(row);
        }

        // print text in board 'cell'
        private string AlignCentre(string text, int width) {
            // use tertiary oparator to check if text is too long for the board cell width, if yes, create substring from begining to 3 from width of cell
            // then add ... to substring and print that
            // if text fits in cell then use text
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            // check if string is null or empty, if yes return new string of width number of ' '
            if (string.IsNullOrEmpty(text)) {
                return new string(' ', width);
            } // uf string is not empty, add spaces to left and right to centre text in the cell.
            else {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        
    }
}
