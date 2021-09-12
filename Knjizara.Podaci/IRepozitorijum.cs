using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Podaci
{
    public interface IRepozitorijum
    {
        void Dodaj(Knjiga usluga);
        bool Obrisi(Guid id);
        void Izmeni(Knjiga usluga);
        void DodajKategoriju(Kategorija kategorija);
        List<Knjiga> DajSve();
        List<Kategorija> DajSveKategorije();
        Knjiga DajPoId(Guid id);
        Kategorija DajKategorijuPoId(Guid id);


    }
}
