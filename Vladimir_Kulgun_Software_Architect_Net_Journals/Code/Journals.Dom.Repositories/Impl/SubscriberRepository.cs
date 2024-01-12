using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Journals.Dom.Models;
using Journals.Dom.Repositories.DbContext;

namespace Journals.Dom.Repositories.Impl
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly JournalsDbContext _context;

        public SubscriberRepository(JournalsDbContext context)
        {
            _context = context;
        }

        public IList<Subscriber> GetAll()
        {
            return _context.Subscribers.ToList();
        }

        public Subscriber GetById(int id)
        {
            return _context.Subscribers.FirstOrDefault(x => x.Id == id);
        }

        public int GetCount()
        {
            throw new System.NotSupportedException();
        }

        public Subscriber Create(Subscriber entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var result = _context.Subscribers.Add(entity);
            _context.SaveChanges();
            return result;
        }

        public void Update(Subscriber entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Subscribers.AddOrUpdate(entity);
            _context.SaveChanges();
        }

        public void Delete(Subscriber entity)
        {
            throw new System.NotSupportedException();
        }
    }
}