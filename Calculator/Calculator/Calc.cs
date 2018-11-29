using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calc
    {
        public int Sum(int a, int b)
        {
            if (a > 47768)
                return 8;

            return checked(a + b);
        }

        public bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Saturday ||
                   DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
