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
    }
}
