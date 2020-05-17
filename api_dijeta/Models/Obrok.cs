using System;
using System.Collections.Generic;
using System.Text;

namespace api_dijeta_data.Models
{
  
  public  class Obrok
    {
        public int ObrokId { get; set; }
        public string NazivObroka { get; set; }
        public int DanId { get; set; }
        public Dan Dan { get; set; }
        public float BrojKalorija { get; set; }

    }
}
