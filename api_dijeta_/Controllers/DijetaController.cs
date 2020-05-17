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
    //  [Route("api/[controller]")]
    //  [ApiController]

    public class TestClas {
        public string username { get; set; }
        public string pass { get; set; }

    }
    public class DijetaController : Controller
    {
        MyContext db;
        public DijetaController(MyContext context)
        {
            db = context;
        }
     
       [HttpGet]
        public IActionResult Index() {
        
            string heder = Request.Headers["Authorization"];
            int korisnikId = HttpContext.Autorizovan(heder);
            if ( korisnikId== -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            List<Dijeta> dijetas = db.dijete.Where(x => x.KorisnikId == korisnikId).ToList();
         
            return Ok(dijetas );
        }

        [HttpPost]
        public IActionResult Add([FromBody]Dijeta dijeta)
        {
            if (dijeta == null)
            { return BadRequest("Podatci nisu poslani.");
            }

            string heder = Request.Headers["Authorization"];

            int korisnikId = HttpContext.Autorizovan(heder);
            if (korisnikId == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            List<Dijeta> dijetas = db.dijete.Where(x => x.KorisnikId == korisnikId).ToList();
            foreach (Dijeta item in dijetas)
            {
                if (dijeta.Naziv == item.Naziv)
                {
                    return BadRequest("Dijeta sa istim nazivom već postoji, možete li unjeti drugi naziv?");
                }
            }

            Dijeta d = new Dijeta()
            {
                Naziv = dijeta.Naziv,
                BrojDana = dijeta.BrojDana,
                KorisnikId =  korisnikId,
                Kalorije = 0


            };
            db.dijete.Add(d);
            db.SaveChanges();
            for (int i = 0; i < d.BrojDana; i++)
            {
                Dan dan = new Dan() {
                BrojKalorija=0,
                DijetaId=d.DijetaId
                
                };
                db.dani.Add(dan);
            }

            db.SaveChanges();
            return Ok(d);
        }


        [HttpDelete]
        public IActionResult Remove([FromQuery]int dijetaId)
        {
           
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }


            if (dijetaId == 0)
            {
                return BadRequest("Podatci nisu poslani.");
            }

            Dijeta d = db.dijete.Where(x => x.DijetaId == dijetaId).FirstOrDefault();
            List<Dan> dans = db.dani.Where(x=>x.DijetaId==d.DijetaId).ToList();
          //brise sve Dane, Obroke i NamirniceObroka dijete
            foreach (var item in dans)
            {
                List<Obrok> obroks = db.obroci.Where(x => x.DanId == item.DanId).ToList();
                foreach (var o in obroks)
                {

                    List<NamirniceObroka> namirniceObrokas = db.namirniceObroka.Where(x => x.ObrokId == o.ObrokId).ToList();
                    foreach (var n in namirniceObrokas)
                    {
                        NamirniceObroka no = n;
                        db.namirniceObroka.Remove(no);
                    }
                    Obrok obrok = o;
                    db.obroci.Remove(obrok);
                }
                Dan dan = item;
                db.dani.Remove(dan);

            }

            db.dijete.Remove(d);
            db.SaveChanges();
        

           
            return Ok(d);
        }
    }
}
