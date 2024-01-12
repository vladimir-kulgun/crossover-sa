using System.Collections.Generic;

namespace Journals.Dom.Models
{
    public class Subscriber
    {
        public Subscriber()
        {
            Journals = new List<Journal>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Journal> Journals { get; }

        public void Subscribe(Journal journal)
        {
            Journals.Add(journal);
        }

        public void Unsubscribe(Journal journal)
        {
            Journals.Remove(journal);
        }
        
    }
}