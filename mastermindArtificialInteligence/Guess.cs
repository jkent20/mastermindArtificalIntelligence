using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence
{
    class Guess {
        private string[] colours = new string[4];

        public string[] Colours {
            get { return colours; }
            set { colours = value; }
        }
    }
}
