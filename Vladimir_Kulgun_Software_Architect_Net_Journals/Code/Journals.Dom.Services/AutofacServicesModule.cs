using Autofac;
using Journals.Dom.Repositories;
using Journals.Dom.Repositories.DbContext;
using Journals.Dom.Repositories.Impl;
using Journals.Dom.Services.Impl;

namespace Journals.Dom.Services
{
    public class AutofacServicesModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            // Services
            builder.RegisterType<JournalService>().As<IJournalService>();
            builder.RegisterType<SubscriberServices>().As<ISubscriberServices>();

            // Repositories
            var context = new JournalsDbContext();

            builder.RegisterType<JournalRepository>()
                .As<IJournalRepository>()
                .WithParameter("context", context);

            builder.RegisterType<PublisherRepository>()
                .As<IPublisherRepository>()
                .WithParameter("context", context);

            builder.RegisterType<SubscriberRepository>()
                .As<ISubscriberRepository>()
                .WithParameter("context", context);
        }
    }
}