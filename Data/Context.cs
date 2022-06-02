using Hranilka.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka
{
    public class Context : DbContext
    {
        //private readonly StreamWriter logStream = new StreamWriter("AppLogi.txt", true);
        public DbSet<ContentCategory> InformationCategories { get; set; }
        public DbSet<DataContainer> DataContainers { get; set; }

        public Context()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Hranilka.db");
                //.UseSqlServer(
                //@"Server=DISTHERE;Database=HranilkaDB;Trusted_Connection=True;");

            //optionsBuilder.LogTo(logStream.WriteLine, LogLevel.Information);
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), LogLevel.Information);

        }

        
    }
}
