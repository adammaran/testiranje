using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class KategorijaModel
    {
        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        public string Naziv { get; set; }
    }
}