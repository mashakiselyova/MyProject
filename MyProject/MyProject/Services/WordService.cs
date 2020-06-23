using Microsoft.EntityFrameworkCore;
using MyProject.DB;
using MyProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class WordService
    {
        private readonly ApplicationContext _context;

        public WordService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateWordAsync(Word word)
        {
            await _context.Words.AddAsync(word);
            await _context.SaveChangesAsync();
        }

        public void EditWord(Word word)
        {
            _context.Words.Update(word);
            _context.SaveChanges();
        }

        public async Task<Word> GetWordAsync(int id)
        {
            return await _context.Words.SingleOrDefaultAsync(w => w.Id == id);
        }

        public void DeleteWord(int id)
        {
            var word = _context.Words.SingleOrDefault(w => w.Id == id);
            _context.Words.Remove(word);
            _context.SaveChanges();
        }

        public async Task<int> GetWordIdAsync(string original, string translation)
        {
            var word = await _context.Words.Where(w => w.Original == original)
                .Where(w => w.Translation == translation)
                .SingleOrDefaultAsync();
            return word.Id;
        }
    }
}
