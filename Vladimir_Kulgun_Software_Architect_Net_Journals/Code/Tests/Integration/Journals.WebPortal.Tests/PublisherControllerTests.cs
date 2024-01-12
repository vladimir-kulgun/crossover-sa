using System.IO;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Autofac;
using FluentAssertions;
using Journals.Dom.Services;
using Journals.WebPortal.Controllers;
using Journals.WebPortal.Services;
using Journals.WebPortal.ViewModel;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Journals.WebPortal.Tests
{
    [TestFixture]
    public class PublisherControllerTests
    {
        private readonly IFixture _fixture = new Fixture();
        private ILifetimeScope _scope;
        private PublisherController _controller;
        private IFileUploadService _fileUploadService;
        private IJournalService _journalService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var builder = new ContainerBuilder();
            Startup.ConfigureDependencies(builder);
            Startup.ConfigureAutoMapper();
            var container = builder.Build();

            _scope = container.BeginLifetimeScope();

            _fileUploadService = _scope.Resolve<IFileUploadService>();
            _journalService = _scope.Resolve<IJournalService>();
        }

        [SetUp]
        public void SetUp()
        {
            _controller = new PublisherController(_fileUploadService, _journalService);
        }

        [Test]
        public void Index()
        {
            // Act
            var result = _controller.Index(null) as ViewResult;

            // Assert
            result.ViewName.Should().Be("Index");
            result.ViewData.Model.Should().NotBeNull();
        }

        [Test]
        public void Delete()
        {
            // Act
            var result = _controller.Delete(null) as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].Should().Be("Index");
        }

        [Test]
        public void FileUpload()
        {
            // Act
            var result = _controller.FileUpload() as ViewResult;

            // Assert
            result.ViewName.Should().Be("FileUpload");
        }

        [Test]
        public void FileUpload_BadFile()
        {
            // Arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            var viewModel = new FileUploadViewModel
            {
                Name = _fixture.Create<string>(),
                Files = fileMock.Object
            };

            // Act
            var result = _controller.FileUpload(viewModel) as ViewResult;

            // Assert
            result.ViewName.Should().Be("FileUpload");
        }

        [Test]
        public void FileUpload_ToIndex()
        {
            // Arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock
                .Setup(x => x.ContentType)
                .Returns(MediaTypeNames.Application.Pdf);
            fileMock
                .Setup(x => x.InputStream)
                .Returns(new MemoryStream(_fixture.Create<byte[]>()));

            var viewModel = new FileUploadViewModel
            {
                Name = _fixture.Create<string>(),
                Files = fileMock.Object
            };

            // Act
            var result = _controller.FileUpload(viewModel) as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].Should().Be("Index");
        }
    }
}