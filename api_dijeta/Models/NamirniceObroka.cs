using System;
using System.Collections.Generic;
using System.Text;

namespace api_dijeta_data.Models
{
  public  class NamirniceObroka
    {
        public int NamirniceObrokaId { get; set; }
        public int Kolicina { get; set; }
        public float Kalorije { get; set; }
        public int ObrokId { get; set; }
        public Obrok Obrok { get; set; }

        public int NamirnicaId { get; set; }
        public Namirnica Namirnica { get; set; }
    }
}
