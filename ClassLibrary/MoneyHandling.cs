using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class MoneyHandling
    {
        public static int CheckBetMoney(int betMoney, int money)
        {
            if (betMoney > 0 && betMoney <= money)
                return 1;
            else if (betMoney > money)
                return -1;
            else
                return 0;
        }

        public static int MakeMoney(int betMoney, int money, int operation)
        {
            if (operation == 1)
                return money += betMoney;
            else if (operation == -1)
                return money -= betMoney;
            else
                return money;
        }
    }
}
