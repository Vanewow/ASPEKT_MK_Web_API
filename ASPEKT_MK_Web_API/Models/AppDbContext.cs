using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using ASPEKT_MK_Web_API.Models;
using ASPEKT_MK_Web_API.Models.DTO;

namespace ASPEKT_MK_Web_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contact { get; set; }


    }
}
