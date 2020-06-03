using MyProject.DB;
using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.Services
{
    public class DictionaryOptionsService
    {
        private readonly ApplicationContext _context;

        public DictionaryOptionsService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Dictionary> GetDictionaries()
        {
            return _context.Dictionaries;
        }
    }
}
