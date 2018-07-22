using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TCGCF.API.Entities
{
    public class CardInfoContext : IdentityDbContext
    {
        /* Entities are used to tell the entity framework on how to handle the necessary tables in the database.
         * Key = Primary Key
         * DatabaseGenerated(DatabaseGeneratedOption.Identity) = Identity
         * [ForeignKey("DeckId")] = Name of foreign key, requires foreign key class to be added as well.
         * */

        //migrations used to version and control the database via code
        //Package Manager Console
        //Add-Migration to add new database version

        public CardInfoContext(DbContextOptions<CardInfoContext> options) : base(options)
        {
            // this will ensure the newest version of the migrations is run
            try {
            Database.Migrate();
            } catch (Exception e)
            {
                Exception exception = e;
            }

        }

        //set set abbreviations as unique
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Set>()
                 .HasIndex(u => u.Abbreviation)
                 .IsUnique();

            //add ModifiedDate and ModifiedBy to each table
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                builder.Entity(entityType.Name).Property<DateTime>("ModifiedDate");
                builder.Entity(entityType.Name).Property<string>("ModifiedBy");
            }
        }

        //set ModifiedDate when a record is saved
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try {
                    foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                    {
                        entry.Property("ModifiedDate").CurrentValue = DateTime.Now;
                    }
                    return await base.SaveChangesAsync();
                } catch (Exception e) {
                    return 0;
                }
        }


        //add used entities here
        public DbSet<Set> Sets { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Legality> Legalities { get; set; }
        public DbSet<CardsInDeck> CardsInDeck { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
