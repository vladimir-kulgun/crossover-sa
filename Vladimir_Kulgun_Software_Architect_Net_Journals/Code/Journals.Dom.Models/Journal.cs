using System.Collections.Generic;

namespace Journals.Dom.Models
{
    public class Journal
    {
        public Journal()
        {
            Subscribers = new HashSet<Subscriber>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Content { get; set; }

        public virtual Publisher Publisher { get; set; }

        public ICollection<Subscriber> Subscribers { get; }
    }
}
