using AppCore.DataAccess.Configs;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.EntityFramework.Contexts {
    public class ETradeContext : DbContext {
        public DbSet<Users> Users { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            ConnectionConfig.ConnectionString = "Server=DESKTOP-09H2BRQ;Database=Rehberim;Trusted_Connection=True;MultipleActiveResultSets=true";
            //ConnectionConfig.ConnectionString = "Data Source =arya.veridyen.com\\MSSQLSERVER2014; Initial Catalog = MenteseIzin; Persist Security Info = True; User ID =MenteseIzin; Password =Jackal333!";
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
             modelBuilder.Entity<UserInformation>().Property(m => m.Email).IsRequired(false);

            modelBuilder.Entity<Users>()
           .HasOne<UserInformation>(s => s.userInfo)
           .WithOne(ad => ad.Users)
           .HasForeignKey<UserInformation>(ad => ad.UserId);

        }
    }
}
