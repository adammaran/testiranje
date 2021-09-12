using System;
using System.Collections.Generic;
using System.Data;
using Knjizara.Logika;
using Knjizara.Podaci;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Knjizara.IntegracioniTestovi
{
    [TestClass]
    public class RepozitorijumTestovi
    {
        private Servis _servis;
        private DbPristup _db;
        [TestInitialize]
        public void InicijalnaPodestavanja()
        {
            _db = new DbPristup();
            IRepozitorijum repozitorijum = new Repozitorijum();
            _servis = new Servis(repozitorijum);

            using (IDbConnection konekcija = _db.KonekcijaKaTestBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "INSERT INTO Kategorija(Id, Naziv) VALUES(@Id,'TestKategorija')";

                    komanda.Parameters.Add(komanda.NamestiParametar("Id", new Guid("00000000-0000-0000-0000-000000000001")));

                    komanda.ExecuteNonQuery();

                    komanda.CommandText = "INSERT INTO Knjiga(Id, Naziv, Cena, Kategorija) VALUES(@Id, 'TestKnjiga', 20.0, @Id)";

                    komanda.ExecuteNonQuery();
                }
            }
        }

        [TestCleanup]
        public void BrisanjePosleTesta()
        {
            using (IDbConnection konekcija = _db.KonekcijaKaTestBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "DELETE FROM Knjiga";
                    komanda.ExecuteNonQuery();
                    komanda.CommandText = "DELETE FROM Kategorija";
                    komanda.ExecuteNonQuery();
                }
            }
        }

        [TestMethod]
        public void Testiranje_DajSve_Vraca_Listu_Knjiga()
        {
            //Arrange
            List<Knjiga> lista;
            int ocekivana_velicina_liste = 1;
            string ocekivani_naziv_usluge = "TestKnjiga";

            //Act
            lista = _servis.DajSve();
            //Assert
            Assert.AreEqual(lista.Count, ocekivana_velicina_liste);
            Assert.AreEqual(lista[0].Naziv, ocekivani_naziv_usluge);
        }

        [TestMethod]
        public void Testiranje_DajSveKategorije_Vraca_Listu_Kategorija()
        {
            //Arrange
            List<Kategorija> lista;
            int ocekivana_velicina_liste = 1;
            string ocekivani_naziv_kategorije = "TestKategorija";

            //Act
            lista = _servis.DajSveKategorije();
            //Assert
            Assert.AreEqual(lista.Count, ocekivana_velicina_liste);
            Assert.AreEqual(lista[0].Naziv, ocekivani_naziv_kategorije);
        }

        [TestMethod]
        public void Testiranje_DajPoId_Sa_Postojecim_Id_Vraca_Knjiga()
        {
            //Arrange
            Knjiga usluga;
            Guid id_usluge = new Guid("00000000-0000-0000-0000-000000000001");
            string ocekivani_naziv_usluge = "TestKnjiga";
            double ocekivana_cena_usluge = 20.0;

            //Act
            usluga = _servis.DajPoId(id_usluge);
            //Assert
            Assert.AreEqual(usluga.Cena, ocekivana_cena_usluge);
            Assert.AreEqual(usluga.Naziv, ocekivani_naziv_usluge);
        }

        [TestMethod]
        public void Testiranje_DajKategorijuPoId_Sa_Postojecim_Id_Vraca_Kategoriju()
        {
            //Arrange
            Kategorija kategorija;
            Guid id_kategorije = new Guid("00000000-0000-0000-0000-000000000001");
            string ocekivani_naziv_kategorije = "TestKategorija";

            //Act
            kategorija = _servis.DajKategorijuPoId(id_kategorije);
            //Assert
            Assert.AreEqual(kategorija.Naziv, ocekivani_naziv_kategorije);
        }

        [TestMethod]
        public void Testiranje_Dodaj_Dodaje_Knjiga_U_Bazu()
        {
            //Arrange
            Knjiga knjiga = new Knjiga(Guid.NewGuid(), "TestKnjiga2", 10.0, new Kategorija(new Guid("00000000-0000-0000-0000-000000000001"), "TestKategorija"));
            List<Knjiga> podaci;
            int ocekivana_velicina_liste_nakon_dodavanja = 2;

            //Act
            _servis.Dodaj(knjiga);
            podaci = _servis.DajSve();
            //Assert
            Assert.AreEqual(podaci.Count, ocekivana_velicina_liste_nakon_dodavanja);
        }

        [TestMethod]
        public void Testiranje_Obrisi_Sa_Postojecim_Id_Brise_Uslugu_Iz_Baze()
        {
            //Arrange
            Guid id_usluge = new Guid("00000000-0000-0000-0000-000000000001");
            List<Knjiga> podaci;
            int ocekivana_velicina_liste_nakon_brisanja = 0;

            //Act
            _servis.Obrisi(id_usluge);
            podaci = _servis.DajSve();
            //Assert
            Assert.AreEqual(podaci.Count, ocekivana_velicina_liste_nakon_brisanja);
        }

        [TestMethod]
        public void Testiranje_Obrisi_Sa_Nepostojecim_Id_Ne_menja_podatke()
        {
            //Arrange
            Guid id_usluge = new Guid("00000000-0000-0000-0000-000000000002");
            List<Knjiga> podaci;
            int ocekivana_velicina_liste_nakon_brisanja = 1;

            //Act
            _servis.Obrisi(id_usluge);
            podaci = _servis.DajSve();
            //Assert
            Assert.AreEqual(podaci.Count, ocekivana_velicina_liste_nakon_brisanja);
        }

        [TestMethod]
        public void Testiranje_Izmeni_Menja_Uslugu_U_Bazi()
        {
            //Arrange
            Guid id_usluge = new Guid("00000000-0000-0000-0000-000000000001");
            Knjiga izmenjena_usluga = new Knjiga(
                id_usluge,
                "NoviNaziv",
                100.0,
                _servis.DajKategorijuPoId(new Guid("00000000-0000-0000-0000-000000000001"))
            );

            Knjiga knjiga;

            //Act
            _servis.Izmeni(izmenjena_usluga);
            knjiga = _servis.DajPoId(id_usluge);

            //Assert
            Assert.AreEqual(izmenjena_usluga.Naziv, knjiga.Naziv);
            Assert.AreEqual(izmenjena_usluga.Cena, knjiga.Cena);

        }
    }
}
