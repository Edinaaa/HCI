using System;
using System.Collections.Generic;
using System.Text;

namespace api_dijeta_data.Models
{
   public class Namirnica
    {
        public int NamirnicaId { get; set; }
        public string Naziv { get; set; }
        public string JM { get; set; }
        public float Kalorije { get; set; }
        public float Ugljikohidrati { get; set; }
        public float Masti { get; set; }
        public float Proteini { get; set; }

        public int Kolicina { get; set; }
    }
}
