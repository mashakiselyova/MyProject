using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DB
{
    public class DictionaryService
    {
        private ApplicationContext _context;

        public DictionaryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateDictionaryAsync(Dictionary dictionary)
        {
            
            await _context.Dictionaries.AddAsync(dictionary);
            await _context.SaveChangesAsync();
        }

        public List<Dictionary> GetAllDictionaries()
        {
            return _context.Dictionaries.ToList();
        }
        
        public async Task<Dictionary> GetDictionaryAsync(int id)
        {
            var dictionary = await _context.Dictionaries.Include(d => d.Words).SingleOrDefaultAsync(d => d.Id == id);
            dictionary.Words.Reverse();
            return dictionary;
        }

        public async Task<int> GetDictionaryIdByWordIdAsync(int wordId)
        {
            var word = await _context.Words.SingleOrDefaultAsync(w => w.Id == wordId);
            return word.DictionaryId;
        }        

        public void DeleteDictionary(int id)
        {
            _context.Dictionaries.Remove(_context.Dictionaries.Find(id));
            _context.SaveChanges();
        }        
    }
}
