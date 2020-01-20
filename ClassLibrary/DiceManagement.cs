using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DiceManagement
    {
        readonly Random rnd = new Random();

        public int ThrowDice()
        {
            return rnd.Next(1, 7);
        }
    }
}