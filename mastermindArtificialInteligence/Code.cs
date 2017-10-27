using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence
{
    class Code {
        private string[] colours = new string[4];

        public Code() {

        }

        public Code(string colourOne, string colourTwo, string colourThree, string colourFour) {
            colours[0] = colourOne;
            colours[1] = colourTwo;
            colours[2] = colourThree;
            colours[3] = colourFour;
        }

        public string[] Colours {
            get { return colours; }
            set { colours = value; }
        }
    }
}
