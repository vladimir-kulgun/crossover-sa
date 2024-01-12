using System.Data.Entity;
using Journals.Dom.Models;

namespace Journals.Dom.Repositories.DbContext
{
    /// <summary>
    /// DB Context initializer for <see cref="JournalsDbContext"/>
    /// </summary>
    public class JournalsDbContextInitializer : DropCreateDatabaseIfModelChanges<JournalsDbContext>
    {

        protected override void Seed(JournalsDbContext context)
        {
            AddPublisher(context);

            AddSubsriber(context);

            context.SaveChanges();
        }

        private void AddSubsriber(JournalsDbContext context)
        {
            context.Subscribers.Add(new Subscriber
            {
                Name = "Ivanov"
            });

            context.Subscribers.Add(new Subscriber
            {
                Name = "Petrov"
            });
        }

        private void AddPublisher(JournalsDbContext context)
        {
            context.Publishers.Add(new Publisher
            {
                Name = "John Smit",
            });

            context.Publishers.Add(new Publisher
            {
                Name = "Emily White"
            });
        }
    }
}