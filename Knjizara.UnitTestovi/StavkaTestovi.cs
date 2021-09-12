using System;
using Knjizara.Podaci;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Knjizara.UnitTestovi
{
    [TestClass]
    public class StavkaTestovi
    {
        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Null_Nazivom_Baca_ArgumentException()
        {
            //Arrange
            string naziv = null;
            Guid id = Guid.Empty;
            double cena = 20.0;
            Kategorija kategorija = new Kategorija(id, "kategorija");

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Knjiga(id, naziv, cena, kategorija));
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Praznim_Nazivom_Baca_ArgumentException()
        {
            //Arrange
            string naziv = "";
            Guid id = Guid.Empty;
            double cena = 20.0;
            Kategorija kategorija = new Kategorija(id, "kategorija");

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Knjiga(id, naziv, cena, kategorija));
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Razmacima_U_Nazivu_Baca_ArgumentException()
        {
            //Arrange
            string naziv = "    ";
            Guid id = Guid.Empty;
            double cena = 20.0;
            Kategorija kategorija = new Kategorija(id, "kategorija");

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Knjiga(id, naziv, cena, kategorija));
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Nula_Cenom_Baca_ArgumentException()
        {
            //Arrange
            string naziv = "naziv";
            Guid id = Guid.Empty;
            double cena = 0.0;
            Kategorija kategorija = new Kategorija(id, "kategorija");

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Knjiga(id, naziv, cena, kategorija));
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Negativnom_Cenom_Baca_ArgumentException()
        {
            //Arrange
            string naziv = "naziv";
            Guid id = Guid.Empty;
            double cena = -20.0;
            Kategorija kategorija = new Kategorija(id, "kategorija");

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Knjiga(id, naziv, cena, kategorija));
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Null_Kategorijom_Baca_ArgumentNullException()
        {
            //Arrange
            string naziv = "naziv";
            Guid id = Guid.Empty;
            double cena = 20.0;
            Kategorija kategorija = null;

            //Act
            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Knjiga(id, naziv, cena, kategorija));
        }
    }
}
