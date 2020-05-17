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
    public class NamirniceObrokController : Controller
    {
        MyContext db;
        public NamirniceObrokController(MyContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index([FromQuery]int ObrokId)
        {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
           
            if (ObrokId == 0)
            {
                return BadRequest("Podatci nisu poslani.");
            }
           
            List<NamirniceObroka> namirniceObrokas = db.namirniceObroka.Where(x=>x.ObrokId==ObrokId).Include(x=>x.Namirnica).ToList();
           
            return Ok(namirniceObrokas);
        }

        [HttpGet]
        public IActionResult DetaljiNamirnice()
        {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            string namirnicaId = Request.Headers["namirnica"];
            if (namirnicaId == null)
            {
                return BadRequest("Podatci nisu poslani.");
            }
            int NamirnicaId = int.Parse(namirnicaId);
            Namirnica namirnica = db.namirnice.Where(x => x.NamirnicaId == NamirnicaId).FirstOrDefault();
            return Ok(namirnica);
        }
        [HttpPost]
        public IActionResult Add([FromBody]NamirniceObroka namirnicaObroka )
        {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            if (namirnicaObroka == null)
            {
                return BadRequest("Podatci nisu poslani.");
            }

            List<NamirniceObroka> namirniceObrokas = db.namirniceObroka.Where(x => x.ObrokId == namirnicaObroka.ObrokId).ToList();

            //ako namirnica vec postoi na obrdenom obroku 
            //onda se kolicina poslane N.O. doda 
            //ako ne doda se nova namirnica na obroku 
            Namirnica n = db.namirnice.Where(x => x.NamirnicaId == namirnicaObroka.NamirnicaId).FirstOrDefault();
            Obrok o = db.obroci.Where(x => x.ObrokId == namirnicaObroka.ObrokId).FirstOrDefault();
            Double Kalorije = (n.Kalorije / n.Kolicina) * namirnicaObroka.Kolicina;
           float zaDodatiKalorije =(float) Math.Round((Double)Kalorije,2);
            NamirniceObroka namirniceObroka = new NamirniceObroka();

            for (int i = 0; i < namirniceObrokas.Count(); i++)
            {
                if (namirniceObrokas[i].NamirnicaId == namirnicaObroka.NamirnicaId)
                {
                    namirniceObroka = namirniceObrokas[i];
                }
            }
            if (namirniceObroka.NamirnicaId == 0)
            {

                namirniceObroka.Kolicina = namirnicaObroka.Kolicina;
                namirniceObroka.Kalorije = zaDodatiKalorije;
                namirniceObroka.NamirnicaId = namirnicaObroka.NamirnicaId;
                namirniceObroka.ObrokId = namirnicaObroka.ObrokId;
                db.namirniceObroka.Add(namirniceObroka);

            }
            else
            {
                namirniceObroka.Kolicina += namirnicaObroka.Kolicina;
                namirniceObroka.Kalorije += zaDodatiKalorije;
                db.namirniceObroka.Update(namirniceObroka);
            }

            o.BrojKalorija += zaDodatiKalorije;
            db.obroci.Update(o);

            Dan dan = db.dani.Where(x => x.DanId == o.DanId).FirstOrDefault();
            dan.BrojKalorija += zaDodatiKalorije;
            db.dani.Update(dan);

            Dijeta dijeta = db.dijete.Where(x => x.DijetaId == dan.DijetaId).FirstOrDefault();
            dijeta.Kalorije += zaDodatiKalorije;
            db.dijete.Update(dijeta);

            db.SaveChanges();
            List<NamirniceObroka> lista = db.namirniceObroka.Where(x => x.ObrokId == namirnicaObroka.ObrokId).Include(x=>x.Namirnica).ToList();
         
            return Ok(lista);
        }

        [HttpDelete]
        public IActionResult Remove([FromQuery]int namirniceObrokaId)
        {
            if (namirniceObrokaId == 0)
            {
                return BadRequest("Podatci nisu poslani.");
            }

            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }

            NamirniceObroka namirnicao = db.namirniceObroka
                .Where(x => x.NamirniceObrokaId == namirniceObrokaId)
                .Include(o=>o.Obrok)
                .FirstOrDefault();

            Obrok obrok = db.obroci.Where(x => x.ObrokId == namirnicao.ObrokId).FirstOrDefault();
            Dan dan = db.dani.Where(x => x.DanId == obrok.DanId).FirstOrDefault();
            Dijeta dijeta = db.dijete.Where(x => x.DijetaId == dan.DijetaId).FirstOrDefault();
       

            dijeta.BrojDana -= 1;
            dijeta.Kalorije -= obrok.BrojKalorija;
            db.dijete.Update(dijeta);
            dan.BrojKalorija -= obrok.BrojKalorija;
            db.dani.Update(dan);
            obrok.BrojKalorija -= namirnicao.Kalorije ;
            db.obroci.Update(obrok);
            db.namirniceObroka.Remove(namirnicao);
            db.SaveChanges();

            return Ok(namirnicao);
        }
    }
}