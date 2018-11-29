using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using ppedv.TastyMoon.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.TastyMoon.Data.EF.Tests
{
    [TestFixture]
    public class EfContextTests
    {
        [Test]
        public void EfContext_can_create_database()
        {
            using (var con = new EfContext("Server=.;Database=TastyMoon_CreateTest;Trusted_Connection=true;"))
            {
                if (con.Database.Exists())
                    con.Database.Delete();

                Assert.That(!con.Database.Exists());
                con.Database.Create();

                Assert.That(con.Database.Exists());
                con.Database.Exists().Should().BeTrue();
            }
        }

        [Test]
        public void EfContext_can_CRUD_KaffeeMaschinenTyp()
        {
            var testMaschine = new KaffeeMaschinenTyp()
            {
                Modell = $"X7_{Guid.NewGuid()}",
                Hersteller = $"Mura {Guid.NewGuid().ToString().Substring(0, 20)}"
            };

            string newModell = $"X8_{Guid.NewGuid()}";

            using (var con = new EfContext())
            {
                //CREATE
                con.KaffeeMaschinenTypen.Add(testMaschine);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //READ
                var loaded = con.KaffeeMaschinenTypen.Find(testMaschine.Id);
                Assert.IsNotNull(loaded);
                Assert.AreEqual(testMaschine.Modell, loaded.Modell);
                Assert.AreEqual(testMaschine.Hersteller, loaded.Hersteller);

                //UPDATE
                loaded.Modell = newModell;
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //UPDATE check
                var loaded = con.KaffeeMaschinenTypen.Find(testMaschine.Id);
                Assert.AreEqual(newModell, loaded.Modell);

                //DELETE
                con.KaffeeMaschinenTypen.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //UPDATE check
                var loaded = con.KaffeeMaschinenTypen.Find(testMaschine.Id);
                Assert.IsNull(loaded);
            }
        }

        [Test]
        public void EfContext_can_CRUD_Rezept()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());

            //GUID nach dem PropertyName nur max 10 zeichen
            fix.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 10)));

            var rezept = fix.Build<Rezept>().Create();

            using (var con = new EfContext())
            {
                con.Rezepte.Add(rezept);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Rezepte.Find(rezept.Id);
                loaded.Should().NotBeNull();

                loaded.Should().BeEquivalentTo(rezept, x =>
                {
                    x.IgnoringCyclicReferences();
                    x.Using<DateTime>(y => y.Subject.Should().BeCloseTo(y.Expectation)).WhenTypeIs<DateTime>();
                    return x;
                });
            }
        }
    }
}
