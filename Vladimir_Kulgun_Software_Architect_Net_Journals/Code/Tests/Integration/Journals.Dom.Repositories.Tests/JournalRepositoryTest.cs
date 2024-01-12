using System;
using FluentAssertions;
using Journals.Dom.Models;
using Journals.Dom.Repositories.DbContext;
using Journals.Dom.Repositories.Impl;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Journals.Dom.Repositories.Tests
{
    /// <summary>
    /// Tests for <see cref="JournalRepository"/>
    /// </summary>
    [TestFixture]
    public class JournalRepositoryTest
    {
        private readonly IFixture _fixture = new Fixture();
        private IJournalRepository _repository;
        private Journal _entity;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _repository = new JournalRepository(new JournalsDbContext());
        }

        [SetUp]
        public void SetUp()
        {
            _entity = _fixture
                .Build<Journal>()
                .With(x => x.Id, int.MinValue)
                .Create();
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
        public void GetAll_Should_Not_Be_Empty()
        {
            // Arrange
            _repository.Create(_entity);

            // Act
            var actual = _repository.GetAll();

            // Assert
            actual.Should().Contain(_entity);
        }

        [Test]
        public void Delete_ShouldFail_ArgumentNullException()
        {
            // Arrange

            // Act
            var act = new Action(() => _repository.Delete(null));

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Delete_Ok()
        {
            // Arrange
            _repository.Create(_entity);
            
            // Act
            _repository.Delete(_entity);

            // Assert
            _repository.GetById(_entity.Id).Should().BeNull();
        }

        [Test]
        public void GetById_ShouldReturn_Null()
        {
            // Arrange
            var id = _fixture.Create<int>();

            // Act
            var actual =_repository.GetById(id);

            // Assert
            actual.Should().BeNull();
        }

        [Test]
        public void GetById_Ok()
        {
            // Arrange
            _repository.Create(_entity);

            // Act
            var actual = _repository.GetById(_entity.Id);

            // Assert
            actual.ShouldBeEquivalentTo(_entity);
        }
    }
}
