using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DB
{
    public class CollectionService
    {
        private ApplicationContext _context;

        public CollectionService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Collection> GetUserCollections(string userId)
        {
            return _context.Collections.Where(c => c.UserId == userId).ToList();
        }

        public async Task CreateCollectionAsync(Collection collection)
        {
            await _context.Collections.AddAsync(collection);
            await _context.SaveChangesAsync();
        }

        public void DeleteCollection(int id)
        {
            _context.Collections.Remove(_context.Collections.Find(id));
            _context.SaveChanges();
        }

        public async Task<Collection> GetCollectionAsync(int id)
        {
            var collection = await _context.Collections.Include(c => c.RevisionWords)
                .ThenInclude(r => r.Word)
                .SingleOrDefaultAsync(c => c.Id == id);
            collection.RevisionWords.Reverse();
            return collection;
        }

        public string GetCollectionName(int id)
        {
            return _context.Collections.Find(id).Name;
        }

        public async Task AddRevisionWordAsync(int wordId, int collectionId)
        {
            var word = await _context.Words.SingleOrDefaultAsync(w => w.Id == wordId);
            var revisionWord = new RevisionWord
            {
                Word = word,
                CollectionId = collectionId,
                NextReview = DateTime.Today + new TimeSpan(1, 0, 0, 0),
                DaysUntilReview = 1
            };
            await _context.RevisionWords.AddAsync(revisionWord);
            await _context.SaveChangesAsync();
        }

        public void DeleteRevisionWord(int id)
        {
            var revisionWord = _context.RevisionWords.Find(id);
            _context.RevisionWords.Remove(revisionWord);
            _context.SaveChanges();
        }
    }
}
