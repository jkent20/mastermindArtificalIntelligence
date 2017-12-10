using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence
{
    class Code {
        // initialise variables
        private string[] colours = new string[4];

        // Default constructor
        public Code() {

        }

        // Constructor for accepting 4 strings.
        public Code(string colourOne, string colourTwo, string colourThree, string colourFour) {
            colours[0] = colourOne;
            colours[1] = colourTwo;
            colours[2] = colourThree;
            colours[3] = colourFour;
        }

        // accessor for code
        public string[] Colours {
            get { return colours; }
            set { colours = value; }
        }
    }
}
