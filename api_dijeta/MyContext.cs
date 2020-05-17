using api_dijeta_data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace api_dijeta_data
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options) { 
        
        
        }
      protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

           
        }

        public DbSet<Korisnik> korisnici { get; set; }
        public DbSet<Namirnica> namirnice { get; set; }
        public DbSet<Dan> dani { get; set; }
        public DbSet<Dijeta> dijete { get; set; }
        public DbSet<NamirniceObroka> namirniceObroka { get; set; }
        public DbSet<Obrok> obroci { get; set; }
       
      





    }
}
