using System;
using System.Collections.Generic;
using System.Linq;
using Journals.Common;
using Journals.Dom.Models;
using Journals.Dom.Repositories;

namespace Journals.Dom.Services.Impl
{
    class SubscriberServices : ISubscriberServices
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberServices(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public Subscriber FindSubscriber(string subscriberName)
        {
            return _subscriberRepository.GetAll().FirstOrDefault(x => x.Name == subscriberName);
        }

        public IEnumerable<Journal> FindJounalsBySubscriber(int subscriberId)
        {
            var subscriber = _subscriberRepository.GetById(subscriberId);
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            return subscriber.Journals;
        }

        public byte[] FindJounalBySubscriber(int subscriberId, int journalId)
        {
            var subscriber = _subscriberRepository.GetById(subscriberId);
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            var journal = subscriber.Journals.FirstOrDefault(x => x.Id == journalId);
            if (journal == null)
                return null;

            return CryptoUtils.Encrypt(journal.Content, subscriber.Name);
        }
    }
}