using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_dijeta_data;
using api_dijeta_data.Models;
using Microsoft.AspNetCore.Mvc;
using api_dijeta_.Helper;

namespace api_dijeta_.Controllers
{
    public class DanController : Controller
    {
        MyContext db;
        public DanController(MyContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index([FromQuery]int Dijeta)
        {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
         
            if (Dijeta == 0)
            {

                return BadRequest("Podatci nisu poslani.");

            }
            List<Dan> dans = db.dani.Where(x => x.DijetaId == Dijeta).ToList();
         
            return Ok(dans);
        }
        [HttpPost]
        public IActionResult Add([FromQuery]int Dijeta) {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
         
            if (Dijeta == 0)
            {

                return BadRequest("Podatci nisu poslani.");
            }
           
            Dijeta dijeta = db.dijete.Where(x => x.DijetaId == Dijeta).FirstOrDefault();
            if (dijeta== null)
            {
                return BadRequest("Podatci nisu pronađeni.");
            }
            Dan dan = new Dan() {
                DijetaId =Dijeta,
                BrojKalorija=0
               
            };
            dijeta.BrojDana++;
            db.dijete.Update(dijeta);
            db.dani.Add(dan);
            db.SaveChanges();

            return Ok(dan);
        }



        [HttpDelete]
        public IActionResult Remove([FromQuery]int danId)
        {
            if (danId == 0)
            {
                return BadRequest("Podatci nisu poslani.");
            }

            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            Dan dan1 = db.dani.Where(x => x.DanId == danId).FirstOrDefault();
            if (dan1== null)
            {
                return BadRequest("Podatci nisu pronađeni.");
            }
            Dijeta d = db.dijete.Where(x => x.DijetaId == dan1.DijetaId).FirstOrDefault();
            
         
            //brise sve  Obroke i NamirniceObroka dan
            List<Obrok> obroks = db.obroci.Where(x => x.DanId == dan1.DanId).ToList();
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

            d.BrojDana -= 1;
            d.Kalorije -= dan1.BrojKalorija;
            db.dijete.Update(d);
            db.dani.Remove(dan1);
            db.SaveChanges();

            return Ok(dan1);
        }
    }
}