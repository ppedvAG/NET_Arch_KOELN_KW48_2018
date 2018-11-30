using System;
using System.Collections.Generic;

namespace Binford.CoffeeTurbo5000
{
    public class PowerDriver
    {
        public int Power { get; private set; }

        public PowerDriver(int power)
        {
            if (power < 5000)
                throw new InvalidOperationException("Nicht genug Power!!!!");

            this.Power = power;
        }

        //name, kaffemenge, milchmenge, zuckermenge
        private List<Tuple<string, int, int, int>> rezepteStore = new List<Tuple<string, int, int, int>>();

        public void Add(string name, int cafe, int milk, int sugar)
        {
            rezepteStore.Add(new Tuple<string, int, int, int>(name, cafe, milk, sugar));
        }

        public string GetStatus()
        {
            if (Power == 5001)
                return "error";
            if (Power == 5002)
                return "beans";

            return "ok";
        }

        public (bool ok, string status) MakeCoffee(int index)
        {
            Tuple<string, int, int, int> rez = rezepteStore[index];

            Console.Beep(560, rez.Item2 * 10);
            Console.Beep(680, rez.Item3 * 10);
            Console.Beep(800, rez.Item4 * 40);

            return (true, GetStatus());
        }

    }
}
