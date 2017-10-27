using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence
{
    class Guess {
        private string[] colours = new string[4];

        public Guess() {

        }

        public Guess(string colourOne, string colourTwo, string colourThree, string colourFour) {
            this.colours[0] = colourOne;
            this.colours[1] = colourTwo;
            this.colours[2] = colourThree;
            this.colours[3] = colourFour;
        }

        public string[] Colours {
            get { return colours; }
            set { colours = value; }
        }
    }
}
