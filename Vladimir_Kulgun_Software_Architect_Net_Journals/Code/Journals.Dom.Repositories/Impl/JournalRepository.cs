using System;
using System.Collections.Generic;
using System.Linq;
using Journals.Dom.Models;
using Journals.Dom.Repositories.DbContext;

namespace Journals.Dom.Repositories.Impl
{
    public class JournalRepository : IJournalRepository
    {
        private readonly JournalsDbContext _context;

        public JournalRepository(JournalsDbContext context)
        {
            _context = context;
        }

        public IList<Journal> GetAll()
        {
            return _context.Journals.ToList();
        }

        public Journal GetById(int id)
        {
            return _context.Journals.FirstOrDefault(x => x.Id == id);
        }

        public int GetCount()
        {
            throw new System.NotSupportedException();
        }

        public Journal Create(Journal entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Journals.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Update(Journal entity)
        {
            throw new System.NotSupportedException();
        }

        public void Delete(Journal entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Journals.Remove(entity);
            _context.SaveChanges();
        }
    }
}