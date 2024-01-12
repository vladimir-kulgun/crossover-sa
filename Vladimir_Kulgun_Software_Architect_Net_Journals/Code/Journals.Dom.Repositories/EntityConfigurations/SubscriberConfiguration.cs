using System.Data.Entity.ModelConfiguration;
using Journals.Dom.Models;

namespace Journals.Dom.Repositories.EntityConfigurations
{
    public class SubscriberConfiguration : EntityTypeConfiguration<Subscriber>
    {
        public SubscriberConfiguration()
        {
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}