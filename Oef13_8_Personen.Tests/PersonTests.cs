using NUnit.Framework;
using Oef13_8_Personen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oef13_8_Personen.Tests
{
    [TestFixture]
    public class PersonTests
    {
        private Persoon sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Persoon();
        }

        [Test]
        public void ShouldHaveVoornaam()
        {
            Assert.That(sut.GetType().GetProperty("Voornaam"), Is.Not.Null);
        }

        [Test]
        public void ShouldHaveNaam()
        {
            Assert.That(sut.GetType().GetProperty("Naam"), Is.Not.Null);
        }

        [Test]
        public void ShouldHaveAdres()
        {
            Assert.That(sut.GetType().GetProperty("Adres"), Is.Not.Null);
        }

        [Test]
        public void ShouldHaveGeboorteDatum()
        {
            Assert.That(sut.GetType().GetProperty("GeboorteDatum"), Is.Not.Null);
        }

        [Test]
        public void ShouldHaveTelefoon()
        {
            Assert.That(sut.GetType().GetProperty("Telefoon"), Is.Not.Null);
        }

        [Test]
        public void ShouldHaveGeslacht()
        {
            Assert.That(sut.GetType().GetProperty("Geslacht"), Is.Not.Null);
        }
    }
}
