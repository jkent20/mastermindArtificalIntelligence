using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class Response {
        private string[] responses = new string[4];

        public string[] Responses {
            get { return responses; }
            set { value = responses; }
        }

        public void PrintResponse(string[] toPrint) {
            Console.WriteLine(string.Join(". ", toPrint));
        }
    }
}