using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class Board {
        private Guess answer = new Guess();
        private bool finished;

        public bool Finished {
            get { return finished; }
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

        public Response TestGuess(Guess guess) {

            Response response = new Response();

            int j = 0;
            int n = 0;

            for (int i = 0; i < guess.Colours.Length; i++) {

                
                if (answer.Colours.Contains(guess.Colours[i])) {
                    if (answer.Colours[i] == guess.Colours[i]) {
                        response.Responses[j] = "Black";
                        n++;
                    }
                    else {
                        response.Responses[j] = "White";
                    }
                    j++;
                }
            }

            if (n == 4 ) {
                finished = true;
            }

            return response;
        }
        
    }
}
