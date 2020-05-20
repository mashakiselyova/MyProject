using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DB
{
    public class LanguageRepository
    {
        private ApplicationContext _context;

        public LanguageRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateLanguageAsync(Language language)
        {
            
            await _context.Languages.AddAsync(language);
            await _context.SaveChangesAsync();
        }

        public List<Language> GetAllLanguages()
        {
            return _context.Languages.ToList();
        }
        
        public async Task<Language> GetLanguageAsync(int id)
        {
            return await _context.Languages.Include(v => v.Words)
                .ThenInclude(w => w.Translations)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Language> GetLanguageWithCollectionsAsync(int id)
        {
            return await _context.Languages.Include(v => v.Collections)
                .Include(v => v.Words)
                .ThenInclude(w => w.Translations)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<int> GetLanguageIdByWordIdAsync(int wordId)
        {
            var word = await _context.Words.SingleOrDefaultAsync(w => w.Id == wordId);
            return word.LanguageId;
        }

        public async Task CreateWordAsync(Word word)
        {
            await _context.Words.AddAsync(word);
            await _context.SaveChangesAsync();
        }

        public void DeleteLanguage(int id)
        {
            _context.Languages.Remove(_context.Languages.Find(id));
            _context.SaveChanges();
        }

        public void DeleteWord(int id)
        {
            var word = _context.Words.Include(w => w.Translations).SingleOrDefault(w => w.Id == id);
            _context.Words.Remove(word);
            _context.SaveChanges();
        }
    }
}
