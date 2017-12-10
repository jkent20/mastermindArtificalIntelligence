using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class UserInputAndMenu {
        // initialise variables
        private string[] availableColours = { "Red", "Blue", "Green", "Purple", "Orange", "Yellow" };

        // method to draw menu, can be used if user would like to play against the bot.
        public void DrawMenu() {
            Console.WriteLine("The possible colours are as follows:");
            Console.WriteLine("1) Red");
            Console.WriteLine("2) Blue");
            Console.WriteLine("3) Green");
            Console.WriteLine("4) Purple");
            Console.WriteLine("5) Orange");
            Console.WriteLine("6) Yellow");
        }

        // accept user input, used with human user
        public string AcceptUserInput() {
            Console.WriteLine("Please enter your colour choice");
            var input = Console.ReadLine();

            string result = ConvertInput(input);
            if (result == "error") {
                Console.WriteLine("Please choose an option from the list. e.g. Green");
                result = AcceptUserInput();
            }

            return result;
        }

        // accept input overload for AI user - accepts array
        public string[] AcceptUserInput(int[] input) {

            string[] result = new string[4];

            for (int i = 0; i < input.Length; i++) {
                result[i] = availableColours[input[i]]; 
            } 

            return result;
        } 
        
        // converts input into correct format, checks for numbers and outputs words
        public string ConvertInput(string input) {
            bool successful = Int32.TryParse(input, out int inputInt);
            if (successful && inputInt <= availableColours.Length) {
                return availableColours[inputInt - 1];
            }
            else if (successful && inputInt > availableColours.Length) {
                return "error";
            }
            else {
                if (availableColours.Contains(input)) {
                    return input;
                }
                else {
                    return "error";
                }
            }
        }

       
    }
}
