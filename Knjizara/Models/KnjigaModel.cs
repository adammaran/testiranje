using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class KnjigaModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Polje mora biti popunjeno")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage ="Mora biti veca od 0")]
        public double Cena { get; set; }
        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        public Guid Kategorija { get; set; }
    }
}