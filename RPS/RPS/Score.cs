using System;
using System.Collections.Generic;
using System.Text;

namespace RPS
{
    public class Score
    {
        // tracks win, loss, and ties

        public int winCount = 0;
        public int lossCount = 0;
        public int tieCount = 0;

        // methods to increment scores
        public void winSet()
        {
            winCount++;
        }        
        public void loseSet()
        {
            lossCount++;
        }       
        public void tieSet()
        {
            tieCount++;
        }
    }
}
