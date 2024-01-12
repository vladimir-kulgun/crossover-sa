using System.Data.Entity.ModelConfiguration;
using Journals.Dom.Models;

namespace Journals.Dom.Repositories.EntityConfigurations
{
    public class PublisherConfiguration : EntityTypeConfiguration<Publisher>
    {
        public PublisherConfiguration()
        {
            Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(255);

            HasMany(x => x.Journals)
                .WithRequired(j => j.Publisher)
                .WillCascadeOnDelete(true);
        }
    }
}