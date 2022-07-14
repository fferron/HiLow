using HiLow.Entity.Entities.TourneyRounds;
using HiLow.Entity.Entities.Tourneys;
using HiLow.Entity.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace HiLow.Infrasctructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tourney> Tourneys { get; set; }
        public DbSet<TourneyRound> TourneyRound { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourneyRound>()
          .HasOne(d => d.Tourney)
          .WithMany(dm => dm.Rounds)
          .HasForeignKey(dkey => dkey.TourneyId);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}