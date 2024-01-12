using System.Collections.Generic;
using System.Linq;
using Journals.Dom.Models;
using Journals.Dom.Repositories.DbContext;

namespace Journals.Dom.Repositories.Impl
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly JournalsDbContext _context;

        public PublisherRepository(JournalsDbContext context)
        {
            _context = context;
        }

        public IList<Publisher> GetAll()
        {
            throw new System.NotSupportedException();
        }

        public Publisher GetById(int id)
        {
            return _context.Publishers.FirstOrDefault(x => x.Id == id);
        }

        public int GetCount()
        {
            throw new System.NotSupportedException();
        }

        public Publisher Create(Publisher entity)
        {
            throw new System.NotSupportedException();
        }

        public void Update(Publisher entity)
        {
            throw new System.NotSupportedException();
        }

        public void Delete(Publisher entity)
        {
            throw new System.NotSupportedException();
        }
    }
}