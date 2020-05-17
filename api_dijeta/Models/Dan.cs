using System;
using System.Collections.Generic;
using System.Text;

namespace api_dijeta_data.Models
{
   public class Dan
    {
        public int DanId { get; set; }
      
        public float BrojKalorija { get; set; }
        public int DijetaId { get; set; }
        public Dijeta Dijeta { get; set; }

    }
}
