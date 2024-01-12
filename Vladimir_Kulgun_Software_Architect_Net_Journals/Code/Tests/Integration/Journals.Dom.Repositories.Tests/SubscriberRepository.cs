using System;
using FluentAssertions;
using Journals.Dom.Models;
using Journals.Dom.Repositories.DbContext;
using Journals.Dom.Repositories.Impl;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Journals.Dom.Repositories.Tests
{
    [TestFixture]
    public class SubscriberRepositoryTests
    {
        private readonly IFixture _fixture = new Fixture();
        private ISubscriberRepository _repository;
        private Subscriber _entity;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _repository = new SubscriberRepository(new JournalsDbContext());
        }

        [SetUp]
        public void SetUp()
        {
            _entity = _fixture.Create<Subscriber>();
        }

        [Test]
        public void Create_ShouldFail_ArgumentNullException()
        {
            // Arrange

            // Act
            var act = new Action(() => _repository.Create(null));

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Create_Ok()
        {
            // Arrange

            // Act
            var actual = _repository.Create(_entity);

            // Assert
            actual.ShouldBeEquivalentTo(_entity);
        }

        [Test]
        public void Update_ShouldFail_ArgumentNullException()
        {
            // Arrange

            // Act
            var act = new Action(() => _repository.Update(null));

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Update_Ok()
        {
            // Arrange
            var journal = _fixture.Create<Journal>();
            _repository.Create(_entity);

            // Act
            _entity.Subscribe(journal);
            _repository.Update(_entity);
            var actual = _repository.GetById(_entity.Id);

            // Assert
            actual.Journals.Should().Contain(journal);
        }
    }
}