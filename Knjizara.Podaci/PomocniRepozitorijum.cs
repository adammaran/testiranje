using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Podaci
{
    //Trivijalna implementacija kako bi se testirali samo pozivi servisa.
    public class PomocniRepozitorijum : IRepozitorijum
    {
        private List<Knjiga> _knjiga;

        private List<Kategorija> _kategorije;

        public PomocniRepozitorijum()
        {
         _knjiga = new List<Knjiga> {
             new Knjiga(Guid.Empty, "TestKnjiga", 20.0, new Kategorija(Guid.Empty, "TestKategorija")),
             new Knjiga(Guid.Empty, "TestKnjiga", 20.0, new Kategorija(Guid.Empty, "TestKategorija"))
         };

        _kategorije = new List<Kategorija> {
            new Kategorija(Guid.Empty, "TestKategorija"),
            new Kategorija(Guid.Empty, "TestKategorija")
         };
        }

        public Kategorija DajKategorijuPoId(Guid id)
        {
            return new Kategorija(id, "TestKategorija");
        }

        public Knjiga DajPoId(Guid id)
        {
            return new Knjiga(id, "TestKnjiga", 20.0, new Kategorija(Guid.Empty, "TestKategorija"));
        }

        public List<Knjiga> DajSve()
        {
            return _knjiga;
        }

        public List<Kategorija> DajSveKategorije()
        {
            return _kategorije;
        }

        public void Dodaj(Knjiga usluga)
        {
            _knjiga.Add(usluga);
        }

        public void DodajKategoriju(Kategorija kategorija)
        {
            _kategorije.Add(kategorija);
        }

        public void Izmeni(Knjiga usluga)
        {
            _knjiga[0] = usluga;
        }

        public bool Obrisi(Guid id)
        {
            return true;
        }
    }
}
