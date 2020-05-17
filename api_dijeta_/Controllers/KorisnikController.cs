using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using api_dijeta_data;
using api_dijeta_data.Models;
using Microsoft.AspNetCore.Mvc;
using api_dijeta_.Helper;

namespace api_dijeta_.Controllers
{
    public class KorisnikController : Controller
    {
        MyContext db;
        public KorisnikController(MyContext context) { db = context; }
        [HttpGet]
        public IActionResult Index()
        {
            string heder = Request.Headers["Authorization"];
            int korisnikId = HttpContext.Autorizovan(heder);
            if (korisnikId == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
          Korisnik  k = db.korisnici.Where(x => x.KorisnikId==korisnikId).FirstOrDefault();

           
            return Ok(k);

        }
        [HttpPost]
        public IActionResult Postavke([FromBody]Korisnik korisnik)
        {

            if (korisnik == null)
            {
                return BadRequest("Podatci nisu poslani.");
            }
            string heder = Request.Headers["Authorization"];
            int korisnikId = HttpContext.Autorizovan(heder);
            if (korisnikId == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            Korisnik k = db.korisnici.Where(x => x.KorisnikId == korisnikId).FirstOrDefault();
            k.Ime = korisnik.Ime;
            k.Prezime = korisnik.Prezime;
            k.Lozinka = korisnik.Lozinka;
            k.KorisnickoIme = korisnik.KorisnickoIme;
            k.Email = korisnik.Email;
            db.korisnici.Update(k);
            db.SaveChanges();
            return Ok(k);
        }

        [HttpPost]
        public IActionResult Registracija([FromBody]Korisnik korisnik)
        {
            
            if (korisnik==null)
            {
                return BadRequest("Podatci nisu poslani.");
            }
            List<Korisnik> korisniks = db.korisnici.ToList();
            foreach (var item in korisniks)
            {
                // Da se nebi desilo da postoje dva korisnika sa istim usenrame
                if (item.KorisnickoIme==korisnik.KorisnickoIme)
                {
                    return BadRequest("Korisnik sa istim korisničkim imenom već postoji, možete li unjeti drgo?");
                }
            }
            Korisnik k = new Korisnik()
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Email = korisnik.Email,
                Lozinka = korisnik.Lozinka,
                KorisnickoIme = korisnik.KorisnickoIme

            };
            db.korisnici.Add(k);
            db.SaveChanges();
            return Ok(korisnik) ;
        }
    }
}
