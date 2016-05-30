using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Chirper.API.Models;

namespace Chirper.API.Infrastructure
{
    public class DataContext : IdentityDbContext<ChirperUser>
    {
        public DataContext() : base("Chirper")
        {
        }

        public IDbSet<Chirp> Chirps { get; set; }
        public IDbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one to many relationship between chirp and comment
            modelBuilder.Entity<Chirp>()
                .HasMany(c => c.Comments)
                .WithRequired(c => c.Chirp)
                .HasForeignKey(c => c.ChirpId);
            
            // configure one to many relationship between user and chirp
            modelBuilder.Entity<ChirperUser>()
                .HasMany(u => u.Chirps)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId);

            // configure one to many relationship between user and comment
            modelBuilder.Entity<ChirperUser>()
                .HasMany(u => u.Comments)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}