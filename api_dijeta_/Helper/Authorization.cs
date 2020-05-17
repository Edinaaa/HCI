using api_dijeta_data;
using api_dijeta_data.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Linq;


namespace api_dijeta_.Helper
{
    public static class Authorization
    {
        public static int Autorizovan(this HttpContext httpContext , string heder)
        {
            MyContext db = httpContext.RequestServices.GetService<MyContext>();
            if (heder == null)
            {
                return -1;
            }
            string[] input;
            Korisnik k;
            if (heder.StartsWith("Basic "))
            {

                string base64encoded = heder.Substring(6);
                string base64Decoded;
                byte[] data = Convert.FromBase64String(base64encoded);
                base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
                input = base64Decoded.Split(":");

            }
            else
            {
                return -1;
            }
            if (input[0] == null || input[1] == null)
            {
                return -1;
            }

            k = db.korisnici.Where(x => x.KorisnickoIme == input[0] && x.Lozinka == input[1]).FirstOrDefault();

            if (k == null)
            {
                return -1;
            }


            return k.KorisnikId;
        }
    }
}
