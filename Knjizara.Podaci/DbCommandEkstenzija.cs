using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Podaci
{
    public static class DbCommandEkstenzija
    {
        public static IDbDataParameter NamestiParametar<T>(this IDbCommand komanda, string naziv, T vrednost)
        {
            IDbDataParameter parametar = komanda.CreateParameter();
            parametar.ParameterName = naziv;
            parametar.Value = vrednost;

            return parametar;
        }
    }
}
