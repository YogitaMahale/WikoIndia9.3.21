using Autofocus.Entity;
using Autofocus.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Autofocus.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext//<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Your code setting up foreign keys of whatever!
        }
    
        public DbSet<CountryRegistration> CountryRegistration { get; set; }
        public DbSet<StateRegistration> StateRegistration { get; set; }
        public DbSet<CityRegistration> CityRegistration { get; set; }
        public DbSet<slider> slider { get; set; }
        public DbSet<packingType> packingType { get; set; }
        public DbSet<packingSize> packingSize { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<MainCategory> MainCategory { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }
       public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<productSize> productSize { get; set; }
         

    }
}

