using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermindArtificialInteligence {
    class Board {
        private Guess answer;

        public void RandomAnswer() {
            // generate randon answer here
            string[] availableColours = ["Red", "Blue", "Green", "Purple", "Orange", "Yellow"];
            Random rng = new Random();


            for (int i = 0; i < 4; i++) {
                int choice = rng.Next(0, 5);
                answer.Colours[i] = availableColours[choice];
            }

            return;
        }

        public Response GetResponse {
            get { return response; }
        }

        public Response TestGuess(Guess guess) {
            
            Response response;
            int k = 0;
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (answer.Colours[i] == guess.Colours[j]) {
                        if (i == j) {
                            response.Responses[k] = "Black";
                        }
                        else {
                            response.Responses[k] = "White";
                        }
                        k++;
                    }
                }
            }

            return response;
        }
        
    }
}
