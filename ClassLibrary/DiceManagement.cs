using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DiceManagement
    {
        readonly static Random rnd = new Random();

        public static int ThrowDie()
        {
            return rnd.Next(1, 7);
        }

        public static int CheckDiceHigher(int enemyD1, int enemyD2, int myD1, int myD2)
        {
            if ((enemyD1 + enemyD2) > (myD1 + myD2))
            {
                return -1;
            }
            else if ((enemyD1 + enemyD2) < (myD1 + myD2))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static int CheckDiceLower(int enemyD1, int enemyD2, int myD1, int myD2)
        {
            if ((enemyD1 + enemyD2) < (myD1 + myD2))
            {
                return -1;
            }
            else if ((enemyD1 + enemyD2) > (myD1 + myD2))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}