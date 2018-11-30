using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.TastyMoon.DomainModel;

namespace ppedv.TastyMoon.Device.BinFord.Tests
{
    [TestClass]
    public class CoffeeTurbo5000Tests
    {

        [TestMethod]
        public void CoffeeTurbo5000_can_make_coffee()
        {
            var ct = new CoffeeTurbo5000();
            var rez = new Rezept() { KaffeeMenge = 70, MilchMenge = 60, Zucker = 20 };
            ct.SendeRezepte(new[] { rez });
            ct.MacheKaffee(rez);

            Debug.WriteLine("HALLOOO");
        }
    }
}
