using System.Data.Entity;
using Journals.Dom.Models;
using Journals.Dom.Repositories.EntityConfigurations;

namespace Journals.Dom.Repositories.DbContext
{
    /// <summary>
    /// DB context for domain models
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class JournalsDbContext : System.Data.Entity.DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsDbContext"/> class.
        /// </summary>
        public JournalsDbContext()
        {
            Database.SetInitializer(new JournalsDbContextInitializer());
        }

        /// <summary>
        /// Collections of <see cref="Journal"/>
        /// </summary>
        public DbSet<Journal> Journals { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new JournalConfiguration());
            modelBuilder.Configurations.Add(new PublisherConfiguration());
            modelBuilder.Configurations.Add(new SubscriberConfiguration());

            modelBuilder.Entity<Subscriber>()
                .HasMany(s => s.Journals)
                .WithMany(c => c.Subscribers)
                .Map(cs =>
                {
                    cs.MapLeftKey("SubscriberRefId");
                    cs.MapRightKey("JournalRefId");
                    cs.ToTable("SubscriberJournal");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}