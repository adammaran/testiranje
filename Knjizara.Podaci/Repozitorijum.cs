using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Podaci
{
    public class Repozitorijum : IRepozitorijum
    {
        private DbPristup _db;

        public Repozitorijum()
        {
            _db = new DbPristup();
        }

        public Kategorija DajKategorijuPoId(Guid id)
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "SELECT * FROM Kategorija WHERE Id=@Id";

                    komanda.Parameters.Add(komanda.NamestiParametar("Id", id));

                    using (IDataReader citac = komanda.ExecuteReader())
                    {
                        citac.Read();
                        return new Kategorija(
                            citac.GetGuid(0),
                            citac.GetString(1)
                        );

                    }
                }
            }
        }

        public Knjiga DajPoId(Guid id)
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "SELECT * FROM Knjiga WHERE Id=@Id";

                    komanda.Parameters.Add(komanda.NamestiParametar("Id", id));

                    using (IDataReader citac = komanda.ExecuteReader())
                    {
                        citac.Read();
                        return new Knjiga(
                            citac.GetGuid(0),
                            citac.GetString(1),
                            citac.GetDouble(2),
                            DajKategorijuPoId(citac.GetGuid(3))
                        );

                    }
                }
            }
        }

        public List<Knjiga> DajSve()
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "SELECT * FROM Knjiga";

                    using (IDataReader citac = komanda.ExecuteReader())
                    {
                        List<Knjiga> podaci = new List<Knjiga>();
                        while (citac.Read())
                        {
                            podaci.Add(new Knjiga(
                                citac.GetGuid(0),
                            citac.GetString(1),
                            citac.GetDouble(2),
                            DajKategorijuPoId(citac.GetGuid(3))
                            ));
                        }

                        return podaci;
                    }
                }
            }
        }

        public List<Kategorija> DajSveKategorije()
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "SELECT * FROM Kategorija";

                    using (IDataReader citac = komanda.ExecuteReader())
                    {
                        List<Kategorija> podaci = new List<Kategorija>();
                        while (citac.Read())
                        {
                            podaci.Add(new Kategorija(
                                citac.GetGuid(0),
                            citac.GetString(1)));
                        }

                        return podaci;
                    }
                }
            }
        }

        public void Dodaj(Knjiga stavka)
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "INSERT INTO Knjiga(Id, Naziv, Cena, Kategorija) VALUES(@Id, @Naziv, @Cena, @Kategorija)";

                    komanda.Parameters.Add(komanda.NamestiParametar("Id", stavka.Id));
                    komanda.Parameters.Add(komanda.NamestiParametar("Naziv", stavka.Naziv));
                    komanda.Parameters.Add(komanda.NamestiParametar("Cena", stavka.Cena));
                    komanda.Parameters.Add(komanda.NamestiParametar("Kategorija", stavka.Kategorija.Id));

                    komanda.ExecuteNonQuery();
                }
            }
        }

        public void DodajKategoriju(Kategorija kategorija)
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "INSERT INTO Kategorija(Id, Naziv) VALUES(@Id, @Naziv)";

                    komanda.Parameters.Add(komanda.NamestiParametar("Id", kategorija.Id));
                    komanda.Parameters.Add(komanda.NamestiParametar("Naziv", kategorija.Naziv));

                    komanda.ExecuteNonQuery();
                }
            }
        }

        public void Izmeni(Knjiga stavka)
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "UPDATE Knjiga SET Naziv = @Naziv, Cena = @Cena, Kategorija = @Kategorija WHERE id = @Id";

                    komanda.Parameters.Add(komanda.NamestiParametar("Id", stavka.Id));
                    komanda.Parameters.Add(komanda.NamestiParametar("Naziv", stavka.Naziv));
                    komanda.Parameters.Add(komanda.NamestiParametar("Cena", stavka.Cena));
                    komanda.Parameters.Add(komanda.NamestiParametar("Kategorija", stavka.Kategorija.Id));

                    komanda.ExecuteNonQuery();
                }
            }
        }

        public bool Obrisi(Guid id)
        {
            using (IDbConnection konekcija = _db.KonekcijaKaBazi())
            {
                konekcija.Open();
                using (IDbCommand komanda = konekcija.CreateCommand())
                {
                    komanda.CommandText = "DELETE Knjiga WHERE id = @Id";

                    komanda.Parameters.Add(komanda.NamestiParametar("Id", id));

                    return komanda.ExecuteScalar() != null;
                }
            }
        }


    }
}
