using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DB
{
    public class VocabularyRepository
    {
        private ApplicationContext _context;

        public VocabularyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateVocabularyAsync(Vocabulary vocabulary)
        {
            
            await _context.Vocabularies.AddAsync(vocabulary);
            await _context.SaveChangesAsync();
        }

        public List<Vocabulary> GetAllVocabularies()
        {
            return _context.Vocabularies.ToList();
        }

        public async Task<Vocabulary> GetVocabularyAsync(int id)
        {
            return await _context.Vocabularies.Include(v => v.Words)
                .ThenInclude(w => w.Translations)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task CreateWordAsync(Word word)
        {
            await _context.Words.AddAsync(word);
            await _context.SaveChangesAsync();
        }

        public void DeleteVocabulary(int id)
        {
            _context.Vocabularies.Remove(_context.Vocabularies.Find(id));
            _context.SaveChanges();
        }
    }
}
