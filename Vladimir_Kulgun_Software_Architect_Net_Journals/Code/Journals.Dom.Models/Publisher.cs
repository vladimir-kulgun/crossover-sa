using System;
using System.Collections.Generic;

namespace Journals.Dom.Models
{
    public class Publisher
    {
        public Publisher()
        {
            Journals = new List<Journal>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Journal> Journals { get; }

        public void AddJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            if (!Journals.Contains(journal))
                journal.Publisher = this;
        }

        public void RemoveJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            if (Journals.Contains(journal))
            {
                journal.Publisher = null;
                Journals.Remove(journal);
            }
        }
    }
}