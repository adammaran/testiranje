using Knjizara.Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Logika
{
    public class Servis
    {
        private IRepozitorijum _repozitorijum;
        public Servis(IRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum ?? throw new ArgumentNullException();
        }

        public void Dodaj(Knjiga usluga) => _repozitorijum.Dodaj(usluga);
        public bool Obrisi(Guid id) => _repozitorijum.Obrisi(id);
        public void Izmeni(Knjiga usluga) => _repozitorijum.Izmeni(usluga);
        public void DodajKategoriju(Kategorija kategorija) => _repozitorijum.DodajKategoriju(kategorija);
        public List<Knjiga> DajSve() => _repozitorijum.DajSve();
        public List<Kategorija> DajSveKategorije() => _repozitorijum.DajSveKategorije();
        public Knjiga DajPoId(Guid id) => _repozitorijum.DajPoId(id);
        public Kategorija DajKategorijuPoId(Guid id) => _repozitorijum.DajKategorijuPoId(id);
    }
}
