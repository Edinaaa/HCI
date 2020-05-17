using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_dijeta_data;
using api_dijeta_data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_dijeta_.Helper;

namespace api_dijeta_.Controllers
{
    public class ObrokController : Controller
    {
        MyContext db;
        public ObrokController(MyContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index([FromQuery]int DanId)
        {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder)==-1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            
            if (DanId == 0)
            {

                return BadRequest("Podatci nisu poslani.");
            }
            List<Obrok> obroks = db.obroci.Where(x => x.DanId == DanId).ToList();

            return Ok(obroks);
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]Obrok obrok) {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }

            if (obrok==null)
            {
                return BadRequest("Podatci nisu poslani.");
            }

            Obrok o = new Obrok() { 
            NazivObroka=obrok.NazivObroka,
            BrojKalorija=0,
            DanId=obrok.DanId
            };
            db.obroci.Add(o);
            db.SaveChanges();
            return Ok();
        }



        [HttpDelete]
        public IActionResult Remove([FromQuery]int obrokId)
        {
            if (obrokId == 0)
            {
                return BadRequest("Podatci nisu poslani.");
            }

            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }

            Obrok obroks = db.obroci.Where(x => x.ObrokId == obrokId).FirstOrDefault();
            Dan dan = db.dani.Where(x => x.DanId == obroks.DanId).FirstOrDefault();
            Dijeta d = db.dijete.Where(x => x.DijetaId == dan.DijetaId).FirstOrDefault();
            //brise sve NamirniceObroka obroka
          List<NamirniceObroka> namirniceObrokas = db.namirniceObroka.Where(x => x.ObrokId == obroks.ObrokId).ToList();
                foreach (var n in namirniceObrokas)
                {
                    NamirniceObroka no = n;
                    db.namirniceObroka.Remove(no);
                }
            
            d.BrojDana -= 1;
            d.Kalorije -= obroks.BrojKalorija;
            db.dijete.Update(d);
            dan.BrojKalorija -= obroks.BrojKalorija;
            db.dani.Update(dan);
            db.obroci.Remove(obroks);
            db.SaveChanges();

            return Ok(obroks);
        }
    }
}