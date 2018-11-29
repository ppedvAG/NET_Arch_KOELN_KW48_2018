using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ppedv.TastyMoon.DomainModel;
using ppedv.TastyMoon.DomainModel.Contracts;

namespace ppedv.TastyMoon.Logic.Tests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void Core_GetRezeptWithMostUsedMilk_No_Rezepte_in_DB()
        {
            var mock = new Mock<IRepository>();
            var core = new Core(mock.Object);

            var result = core.GetRezeptWithMostUsedMilk();

            result.Should().BeNull();
        }


        [TestMethod]
        public void Core_GetRezeptWithMostUsedMilk_2_Rezepte_in_DB()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Rezept>()).Returns(() =>
            {
                var r1 = new Rezept() { MilchMenge = 30, Name = "r1" };
                var r2 = new Rezept() { MilchMenge = 50, Name = "r2" };
                return new[] { r1, r2 }.AsQueryable();
            });

            var core = new Core(mock.Object);

            var result = core.GetRezeptWithMostUsedMilk();

            result.Name.Should().Be("r2");
        }

        [TestMethod]
        public void Core_GetRezeptWithMostUsedMilk_2_Rezepte_with_same_Milk_in_DB()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Rezept>()).Returns(() =>
            {
                var r1 = new Rezept() { MilchMenge = 50, Name = "b" };
                var r2 = new Rezept() { MilchMenge = 50, Name = "a" };
                return new[] { r1, r2 }.AsQueryable();
            });

            var core = new Core(mock.Object);

            var result = core.GetRezeptWithMostUsedMilk();

            result.Name.Should().Be("a");
        }

    }
}
