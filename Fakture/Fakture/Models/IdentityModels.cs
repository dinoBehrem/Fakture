using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Fakture.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fakture.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Faktura> Fakture { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Fakture", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Faktura> Fakture { get; set; }
        public virtual DbSet<StavkaFakture> StavkeFakture { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faktura>()
                .HasMany(f => f.StavkeFakture)
                .WithRequired(sf => sf.Faktura)
                .HasForeignKey(sf => sf.FakturaId);

            modelBuilder.Entity<Faktura>()
                .HasRequired(f => f.Stvaratelj)
                .WithMany(au => au.Fakture)
                .HasForeignKey(f => f.StvarateljId);
            
            modelBuilder.Entity<StavkaFakture>()
                .HasRequired(sf => sf.Faktura)
                .WithMany(f => f.StavkeFakture)
                .HasForeignKey(sf => sf.FakturaId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.Fakture)
                .WithRequired(f => f.Stvaratelj)
                .HasForeignKey(f => f.StvarateljId);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}