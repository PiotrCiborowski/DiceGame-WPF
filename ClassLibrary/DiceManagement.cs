using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    class DiceManagement
    {
        Random rnd = new Random();

        public int ThrowDice()
        {
            return rnd.Next(1, 8);
        }
    }
}
