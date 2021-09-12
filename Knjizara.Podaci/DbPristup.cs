using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Podaci
{
    public class DbPristup
    { 
        public IDbConnection KonekcijaKaBazi()
        {
            ConnectionStringSettings podesavanjaKonekcije = ConfigurationManager.ConnectionStrings["Konekcija"];
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(podesavanjaKonekcije.ProviderName);

            DbConnection konekcija = dbFactory.CreateConnection();
            konekcija.ConnectionString = podesavanjaKonekcije.ConnectionString;

            return konekcija;
        }

        public IDbConnection KonekcijaKaTestBazi()
        {
            ConnectionStringSettings podesavanjaKonekcije = ConfigurationManager.ConnectionStrings["Konekcija"];
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(podesavanjaKonekcije.ProviderName);

            DbConnection konekcija = dbFactory.CreateConnection();
            konekcija.ConnectionString = podesavanjaKonekcije.ConnectionString;

            return konekcija;
        }
    }
}
