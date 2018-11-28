using System;
using System.Text;

namespace HalloDeco
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var frühstück = new Salami(new Käse(new Brot()));
            Console.WriteLine($"{frühstück.Name} {frühstück.Preis:c}");

            var mittag = new Salami(new Käse(new Käse(new Käse(new Pizza()))));
            Console.WriteLine($"{mittag.Name} {mittag.Preis:c}");


            Console.ReadLine();
        }
    }

    interface ICompo
    {
        string Name { get; }
        decimal Preis { get; }
    }

    abstract class Deco : ICompo
    {
        protected ICompo parent;
        public Deco(ICompo parent) => this.parent = parent;

        public abstract string Name { get; }

        public abstract decimal Preis { get; }
    }

    class Pizza : ICompo
    {
        public string Name => "Pizza";

        public decimal Preis => 5m;
    }

    class Brot : ICompo
    {
        public string Name => "Brot";

        public decimal Preis => 2m;
    }

    class Käse : Deco
    {
        public Käse(ICompo parent) : base(parent)
        { }

        public override string Name => parent.Name + " Käse";

        public override decimal Preis => parent.Preis + 1;
    }

    class Salami : Deco
    {
        public Salami(ICompo parent) : base(parent)
        { }

        public override string Name => parent.Name + " Salami";

        public override decimal Preis => parent.Preis + 1.7m;
    }
}
