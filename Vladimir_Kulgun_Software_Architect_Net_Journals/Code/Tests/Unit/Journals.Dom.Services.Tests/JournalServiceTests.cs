using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Journals.Dom.Models;
using Journals.Dom.Repositories;
using Journals.Dom.Services.Impl;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Journals.Dom.Services.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class JournalServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private string _journalName;

        private IJournalService _service;
        private byte[] _content;
        private Mock<IJournalRepository> _journalRepositoryMock;
        private Mock<IPublisherRepository> _publisherRepositoryMock;
        private int _publisherId;
        private int _journalId;
        private Mock<ISubscriberRepository> _subscriberRepositoryMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _journalRepositoryMock = new Mock<IJournalRepository>();
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _subscriberRepositoryMock = new Mock<ISubscriberRepository>();

            _service = new JournalService(_journalRepositoryMock.Object, _publisherRepositoryMock.Object, _subscriberRepositoryMock.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _journalName = _fixture.Create<string>();
            _content = _fixture.Create<byte[]>();
            _publisherId = _fixture.Create<int>();
            _journalId = _fixture.Create<int>();
        }

        [Test]
        public void CreateJounal_ShouldFail_ArgumentNullException()
        {
            // Arrange
            _publisherRepositoryMock
                .Setup(x => x.GetById(_publisherId))
                .Returns(default(Publisher));

            // Act
            var act1 = new Action(() => _service.CreateJounal(null, _content, _publisherId));
            var act2 = new Action(() => _service.CreateJounal(_journalName, null, _publisherId));
            var act3 = new Action(() => _service.CreateJounal(_journalName, _content, _publisherId));

            // Assert
            act1.ShouldThrow<ArgumentNullException>();
            act2.ShouldThrow<ArgumentNullException>();
            act3.ShouldThrow<ArgumentNullException>();

            _publisherRepositoryMock.VerifyAll();
        }

        [Test]
        public void CreateJounal_Ok()
        {
            // Arrange
            var publisher = _fixture.Create<Publisher>();

            _journalRepositoryMock
                .Setup(x => x.Create(It.IsAny<Journal>()));

            _publisherRepositoryMock
                .Setup(x => x.GetById(_publisherId))
                .Returns(publisher);

            // Act
            var actual = _service.CreateJounal(_journalName, _content, _publisherId);

            // Assert
            actual.Publisher.ShouldBeEquivalentTo(publisher);

            _journalRepositoryMock.VerifyAll();
            _publisherRepositoryMock.VerifyAll();
        }

        [Test]
        public void DeleteJournal_ShouldFail_ArgumentNullException()
        {
            // Arrange
            var journal = new Journal();
            var publisher = new Publisher();
            publisher.AddJournal(journal);

            _journalRepositoryMock
                .Setup(x => x.GetById(_journalId))
                .Returns(journal);

            // Act
            var act1 = new Action(() => _service.DeleteJournal(It.IsAny<int>(), _publisherId));
            var act2 = new Action(() => _service.DeleteJournal(_journalId, It.IsAny<int>()));

            // Assert
            act1.ShouldThrow<ArgumentNullException>();
            act2.ShouldThrow<ArgumentNullException>();

            _journalRepositoryMock.VerifyAll();
        }

        [Test]
        public void DeleteJournal_Ok()
        {
            // Arrange
            var journal = new Journal();
            var publisher = new Publisher();
            publisher.Journals.Add(journal);
            publisher.AddJournal(journal);

            _journalRepositoryMock
                .Setup(x => x.GetById(_journalId))
                .Returns(journal);

            _publisherRepositoryMock
                .Setup(x => x.GetById(_publisherId))
                .Returns(publisher);

            // Act
            _service.DeleteJournal(_journalId, _publisherId);

            // Assert
            _journalRepositoryMock.VerifyAll();
            _publisherRepositoryMock.VerifyAll();

            journal.Publisher.Should().BeNull();
        }
    }
}
