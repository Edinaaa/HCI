using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace api_dijeta_data.Models
{
    public class Dijeta
    {
        [Key]
        public int DijetaId { get; set; }
        public string Naziv { get; set; }
        public int BrojDana { get; set; }
        public float Kalorije { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

    }
}
