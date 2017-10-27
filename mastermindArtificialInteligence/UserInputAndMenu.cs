using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class UserInputAndMenu {
        private string = ["Red", "Blue", "Green", "Purple", "Orange", "Yellow"];

        public void DrawMenu() {
            Console.WriteLine("The possible colours are as follows:");
            Console.WriteLine("1) Red");
            Console.WriteLine("2) Blue");
            Console.WriteLine("3) Green");
            Console.WriteLine("4) Purple");
            Console.WriteLine("5) Orange");
            Console.WriteLine("6) Yellow");
        }

        public string AcceptUserInput() {
            Console.WriteLine("Please enter your colour choice for position one");
            var input = Console.ReadLine();

            this.ConvertInput(input);
            return;
        }
        
        public string ConvertInput(string input) {
            bool successful = Int32.TryParse(input, out int inputInt);
            if (successful) {

            }
        }

       
    }
}
