using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Microsoft.EntityFrameworkCore.Metadata;

namespace  DAL.Model
{
    public partial class MovieBuffContext : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("customsettings.json");

                IConfigurationRoot Configuration = builder.Build();
                optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:MovieBuffDatabase"]);
            }
        }

    }
}
