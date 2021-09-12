using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Podaci
{
    public class Kategorija
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }

        public Kategorija(Guid id, string naziv)
        {
            if(string.IsNullOrWhiteSpace(naziv))
            {
                throw new ArgumentException();
            }

            this.Id = id;
            this.Naziv = naziv;
        }

        public Kategorija() { }
    }
}
