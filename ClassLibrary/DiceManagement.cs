using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DiceManagement
    {
<<<<<<< HEAD
        readonly static Random rnd = new Random();
=======
<<<<<<< HEAD
        readonly static Random rnd = new Random();
=======
        readonly Random rnd = new Random();
>>>>>>> 374500772276111782d8745afd290b28ea15b875
>>>>>>> 2622d6e5dfe1072e18a217aeedfd9a7fb5b7d155

        public static int ThrowDie()
        {
            return rnd.Next(1, 7);
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 2622d6e5dfe1072e18a217aeedfd9a7fb5b7d155
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
<<<<<<< HEAD
=======
=======
>>>>>>> 374500772276111782d8745afd290b28ea15b875
>>>>>>> 2622d6e5dfe1072e18a217aeedfd9a7fb5b7d155
        }
    }
}