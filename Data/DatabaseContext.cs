using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using dotnet_mvc;

namespace dotnet_mvc.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }
    }
}

