using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WoofpakGamingSiteServerApp.Data;

namespace WoofpakGamingSiteServerApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Startup>(true)
                .Build();

            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SecretConnection"));
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        }
        public DbSet<WoofpakGamingSiteServerApp.Data.Tournament> Tournament { get; set; }
        public DbSet<WoofpakGamingSiteServerApp.Data.Game> Game { get; set; }
        public DbSet<WoofpakGamingSiteServerApp.Data.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<WoofpakGamingSiteServerApp.Data.ExtraLifeDontation> ExtraLifeDonation { get; set; }
        public DbSet<WoofpakGamingSiteServerApp.Data.ExtraLifeEvent> ExtraLifeEvent { get; set; }
        public DbSet<WoofpakGamingSiteServerApp.Data.ExtraLifeTeam> ExtraLifeTeam { get; set; }
        public DbSet<WoofpakGamingSiteServerApp.Data.ExtraLifeParticipant> ExtraLifeParticipant { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Tournament>().HasOne<Game>()
            //.WithMany();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();
        //        var connectionString = configuration.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}
    }
}
