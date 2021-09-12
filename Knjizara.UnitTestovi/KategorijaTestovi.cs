using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Knjizara.Podaci;

namespace Knjizara.UnitTestovi
{
    [TestClass]
    public class KategorijaTestovi
    {
        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Null_Nazivom_Baca_ArgumentException()
        {
            //Arrange
            string naziv = null;
            Guid id = Guid.Empty;

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Kategorija(id, naziv));
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Praznim_Nazivom_Baca_ArgumentException()
        {
            //Arrange
            string naziv = "";
            Guid id = Guid.Empty;

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Kategorija(id, naziv));
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Razmacima_U_Nazivu_Baca_ArgumentException()
        {
            //Arrange
            string naziv = "    ";
            Guid id = Guid.Empty;

            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() => new Kategorija(id, naziv));
        }
    }
}
