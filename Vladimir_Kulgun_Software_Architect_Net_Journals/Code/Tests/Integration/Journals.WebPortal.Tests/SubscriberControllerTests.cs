using System.Web.Mvc;
using Autofac;
using FluentAssertions;
using Journals.Dom.Services;
using Journals.WebPortal.Controllers;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Journals.WebPortal.Tests
{
    [TestFixture]
    public class SubscriberControllerTests
    {
        private readonly IFixture _fixture = new Fixture();
        private ILifetimeScope _scope;
        private SubscriberController _controller;
        private IJournalService _journalService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var builder = new ContainerBuilder();
            Startup.ConfigureDependencies(builder);
            Startup.ConfigureAutoMapper();
            var container = builder.Build();

            _scope = container.BeginLifetimeScope();

            _journalService = _scope.Resolve<IJournalService>();
        }

        [SetUp]
        public void SetUp()
        {
            _controller = new SubscriberController(_journalService);
        }

        [Test]
        public void Index()
        {
            // Act
            var result = _controller.Index(null, null) as ViewResult;

            // Assert
            result.ViewName.Should().Be("Index");
            result.ViewData.Model.Should().NotBeNull();
        }

        [Test]
        public void Subscribe()
        {
            // Act
            var result = _controller.Subscribe(null, null, null) as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].Should().Be("Index");
        }

        [Test]
        public void Unsubscribe()
        {
            // Act
            var result = _controller.Unsubscribe(null, null, null) as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].Should().Be("Index");
        }

    }
}