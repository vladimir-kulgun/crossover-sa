using System;
using System.Collections.Generic;
using System.Linq;
using Journals.Dom.Models;
using Journals.Dom.Repositories;

namespace Journals.Dom.Services.Impl
{
    public class JournalService : IJournalService
    {
        private readonly IJournalRepository _journalRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ISubscriberRepository _subscriberRepository;

        public JournalService(IJournalRepository journalRepository, IPublisherRepository publisherRepository, ISubscriberRepository subscriberRepository)
        {
            _journalRepository = journalRepository;
            _publisherRepository = publisherRepository;
            _subscriberRepository = subscriberRepository;
        }

        public Journal CreateJounal(string journalName, byte[] content, int publisherId)
        {
            if (string.IsNullOrWhiteSpace(journalName))
                throw new ArgumentNullException(nameof(journalName));

            if (content == null)
                throw new ArgumentNullException(nameof(content));

            var publisher = _publisherRepository.GetById(publisherId);
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));

            var journal = new Journal
            {
                Name = journalName,
                Content = content
            };
            publisher.AddJournal(journal);

            _journalRepository.Create(journal);

            return journal;
        }

        public void DeleteJournal(int journalId, int publisherId)
        {
            var journal = _journalRepository.GetById(journalId);
            if (journal == null)
                throw new ArgumentNullException(nameof(journal));

            var publisher = _publisherRepository.GetById(publisherId);
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));

            publisher.RemoveJournal(journal);

            _journalRepository.Delete(journal);
        }

        public void SubscribeJournal(int journalId, int subscriberId)
        {
            var journal = _journalRepository.GetById(journalId);
            if (journal == null)
                throw new ArgumentNullException(nameof(journal));

            var subscriber = _subscriberRepository.GetById(subscriberId);
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            subscriber.Subscribe(journal);

            _subscriberRepository.Update(subscriber);
        }

        public void UnsubscribeJournal(int journalId, int subscriberId)
        {
            var journal = _journalRepository.GetById(journalId);
            if (journal == null)
                throw new ArgumentNullException(nameof(journal));

            var subscriber = _subscriberRepository.GetById(subscriberId);
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            subscriber.Unsubscribe(journal);

            _subscriberRepository.Update(subscriber);
        }

        public IEnumerable<Journal> FindJounalsBySubscriber(int subscriberId)
        {
            var subscriber = _subscriberRepository.GetById(subscriberId);
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            return subscriber.Journals;
        }

        public IEnumerable<Journal> FindJounalsByPublisher(int publisherId)
        {
            var publisher = _publisherRepository.GetById(publisherId);
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));

            return publisher.Journals;
        }

        public IEnumerable<Journal> ListJournals(string searchString)
        {
            var journals = _journalRepository.GetAll();

            return string.IsNullOrWhiteSpace(searchString)
                ? journals
                : journals.Where(x => x.Name.Contains(searchString));
        }
    }
}