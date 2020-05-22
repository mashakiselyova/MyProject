using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DB
{
    public class CollectionRepository
    {
        private ApplicationContext _context;

        public CollectionRepository(ApplicationContext context)
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
            return await _context.Collections.Include(c => c.RevisionWords)
                .ThenInclude(r => r.Word)
                .ThenInclude(w => w.Translations)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddRevisionWordAsync(int id, int collectionId)
        {
            var word = await _context.Words.Include(w => w.Translations).SingleOrDefaultAsync(w => w.Id == id);
            var revisionWord = new RevisionWord { Word = word, CollectionId = collectionId };
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
