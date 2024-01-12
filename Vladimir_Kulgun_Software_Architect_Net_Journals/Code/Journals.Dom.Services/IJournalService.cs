using System.Collections.Generic;
using Journals.Dom.Models;

namespace Journals.Dom.Services
{
    public interface IJournalService
    {
        Journal CreateJounal(string journalName, byte[] content, int publisherId);

        void DeleteJournal(int journalId, int publisherId);

        void SubscribeJournal(int journalId, int subscriberId);

        void UnsubscribeJournal(int journalId, int subscriberId);

        IEnumerable<Journal> FindJounalsBySubscriber(int subscriberId);

        IEnumerable<Journal> FindJounalsByPublisher(int publisherId);

        IEnumerable<Journal> ListJournals(string searchString);
    }
}
