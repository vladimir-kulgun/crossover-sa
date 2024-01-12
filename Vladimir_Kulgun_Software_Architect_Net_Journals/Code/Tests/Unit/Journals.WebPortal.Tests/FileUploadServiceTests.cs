using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Mime;
using System.Web;
using FluentAssertions;
using Journals.WebPortal.Exceptions;
using Journals.WebPortal.Services;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Journals.WebPortal.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class FileUploadServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IFileUploadService _service;
        private Mock<HttpPostedFileBase> _journalFileMock;
        private string _journalName;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _journalFileMock = new Mock<HttpPostedFileBase>();
            _service = new FileUploadService();
            
        }

        [SetUp]
        public void Setup()
        {
            _journalName = _fixture.Create<string>();
        }

        [Test]
        public void Upload_ShouldFail_ArgumentNullException()
        {
            // Arrange
            _journalFileMock
                .Setup(x => x.ContentType)
                .Returns("boo/any");

            // Act
            var act = new Action(() => _service.CreateContent(null));

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Upload_ShouldFail_Not_Pfd()
        {
            // Arrange
            _journalFileMock = new Mock<HttpPostedFileBase>();
            _journalFileMock
                .Setup(x => x.ContentType)
                .Returns("boo/any");

            // Act
            var act = new Action(() => _service.CreateContent(_journalFileMock.Object));

            // Assert
            act.ShouldThrow<FileUploadException>();
        }

        [Test]
        public void Upload_Ok()
        {
            // Arrange
            var expected = _fixture.Create<byte[]>();


            _journalFileMock
                .Setup(x => x.ContentType)
                .Returns(MediaTypeNames.Application.Pdf);

            _journalFileMock
                .Setup(x => x.InputStream)
                .Returns(new MemoryStream(expected));
           
                
            // Act
            var actual = _service.CreateContent(_journalFileMock.Object);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
