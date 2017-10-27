using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class Response {
        private string[] responses = new string[] {
        "", "", "", ""
        };
        private int blackPegs = new int();
        private int whitePegs = new int();

        public int BlackPegs {
            get { return blackPegs;  }
            set { blackPegs = value; }
        }

        public int WhitePegs {
            get { return whitePegs;  }
            set { whitePegs = value; }
        }

        public string[] Responses {
            get { return responses; }
            set { value = responses; }
        }

        public void PrintResponse(string[] toPrint, Board board, int round) {
            for (int i = 0; i < 4; i++) {
                board.Rounds[round][i + 4] = responses[i];
            }
        }
    }
}