using Knjizara.Logika;
using Knjizara.Podaci;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Knjizara.UnitTestovi
{
    [TestClass]
    public class ServisTestovi
    {
        private Servis _servis;

        [TestInitialize]
        public void InicijalnoPodesavanje()
        {
            _servis = new Servis(new PomocniRepozitorijum());
        }

        [TestMethod]
        public void Testiranje_Konstruktora_Sa_Null_Repozitorijumom_Baca_ArgumentNullException()
        {

            //Arange
            //Act
            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Servis(null));
        }

        [TestMethod]
        public void Testiranje_DajSve_Vraca_Listu_Iz_PomocnogRepozitorijuma()
        {
            //Arange
            List<Knjiga> lista;
            int ocekivana_velicina_liste = 2;
            string ocekivani_naziv_stavke = "TestKnjiga";

            //Act
            lista = _servis.DajSve();
            //Assert
            Assert.AreEqual(lista.Count, ocekivana_velicina_liste);
            Assert.AreEqual(lista[0].Naziv, ocekivani_naziv_stavke);
        }

        [TestMethod]
        public void Testiranje_DajSveKategorije_Vraca_Listu_Iz_PomocnogRepozitorijuma()
        {
            //Arange
            List<Kategorija> lista;
            int ocekivana_velicina_liste = 2;
            string ocekivani_naziv_stavke = "TestKategorija";

            //Act
            lista = _servis.DajSveKategorije();
            //Assert
            Assert.AreEqual(lista.Count, ocekivana_velicina_liste);
            Assert.AreEqual(lista[0].Naziv, ocekivani_naziv_stavke);
        }

        [TestMethod]
        public void Testiranje_Dodaj_Dodaje_U_Listu_Iz_PomocnogRepozitorijuma()
        {
            //Arange
            List<Knjiga> lista;
            int ocekivana_velicina_liste = 3;
            Knjiga stavka = new Knjiga(Guid.Empty, "TestDodaj", 15.0, new Kategorija(Guid.Empty, "TestDodaj"));

            //Act
            _servis.Dodaj(stavka);
            lista = _servis.DajSve();
            //Assert
            Assert.AreEqual(lista.Count, ocekivana_velicina_liste);
        }

        [TestMethod]
        public void Testiranje_DodajKategoriju_Dodaje_U_Listu_Iz_PomocnogRepozitorijuma()
        {
            //Arange
            List<Kategorija> lista;
            int ocekivana_velicina_liste = 3;
            Kategorija kategorija = new Kategorija(Guid.Empty, "TestDodaj");

            //Act
            _servis.DodajKategoriju(kategorija);
            lista = _servis.DajSveKategorije();
            //Assert
            Assert.AreEqual(lista.Count, ocekivana_velicina_liste);
        }

        [TestMethod]
        public void Testiranje_Izmeni_Menja_Podatke_U_Listi_Iz_PomocnogRepozitorijuma()
        {
            //Arange
            List<Knjiga> lista;
            Knjiga stavka = new Knjiga(Guid.Empty, "TestIzmena", 15.0, new Kategorija(Guid.Empty, "TestIzmena"));

            //Act
            _servis.Izmeni(stavka);
            lista = _servis.DajSve();
            //Assert
            Assert.AreEqual(stavka.Naziv, lista[0].Naziv);
        }

        [TestMethod]
        public void Testiranje_Obrisi_Vraca_True()
        {
            //Arange
            //Act
            //Assert
            Assert.IsTrue(_servis.Obrisi(Guid.Empty));
        }


    }
}
