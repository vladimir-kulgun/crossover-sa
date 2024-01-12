using System.Data.Entity.ModelConfiguration;
using Journals.Dom.Models;

namespace Journals.Dom.Repositories.EntityConfigurations
{
    public class JournalConfiguration : EntityTypeConfiguration<Journal>
    {
        public JournalConfiguration()
        {
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(x => x.Content)
                .IsRequired();

            HasRequired(x => x.Publisher)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
