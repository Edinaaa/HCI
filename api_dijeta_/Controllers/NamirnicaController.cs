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
    public class NamirnicaController : Controller
    {
        MyContext db;
        public NamirnicaController(MyContext context) { db = context; }
        [HttpGet]
        public IActionResult Index([FromQuery] int namirnica)
        {
            if (namirnica == 0)
            {
                return BadRequest("Podatci nisu poslani.");
            }
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
     
            
    
            Namirnica n = db.namirnice.Where(x => x.NamirnicaId == namirnica).FirstOrDefault();
            if (n==null)
            {
                return BadRequest("Podatci nisu pronađeni.");

            }
            return Ok(n);
        }
        [HttpGet]
        public IActionResult Pretraga([FromQuery]string Naziv) {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
           
            if (Naziv==null)
            {
                return BadRequest("Podatci nisu poslani.");
            }
            List<Namirnica> namirnicas = db.namirnice.ToList();
            List<Namirnica> namirnice = new List<Namirnica>();
            foreach (var item in namirnicas)
            {
                if (item.Naziv.ToLower().StartsWith(Naziv.ToLower()))
                {
                    namirnice.Add(item);
                }
            }
            return Ok(namirnice);
        }
        [HttpPost]
        public IActionResult Add([FromBody]Namirnica namirnica)
        {
            string heder = Request.Headers["Authorization"];
            if (HttpContext.Autorizovan(heder) == -1)
            {
                return Unauthorized("Nemate pravo pristupa!");
            }
            if (namirnica == null)
            {
                return BadRequest("Podatci nisu poslani.");
            }

            List<Namirnica> namirnice = db.namirnice.Where(x => x.NamirnicaId == namirnica.NamirnicaId).ToList();
            foreach (var item in namirnice)
            {
                if (item.Naziv==namirnica.Naziv)
                {
                    return BadRequest("Već postoji namirnica sa tim nazivom, možete li unjeti drugi?");
                }
            }


            Namirnica n = new Namirnica() { 
            Naziv=namirnica.Naziv,
            JM=namirnica.JM,
            Kalorije=namirnica.Kalorije,
            Kolicina=namirnica.Kolicina,
            Masti=namirnica.Masti,
            Proteini=namirnica.Proteini,
            Ugljikohidrati=namirnica.Ugljikohidrati
            
            };
            db.namirnice.Add(n);
            db.SaveChanges();
            return Ok(n);
        }
    }
}