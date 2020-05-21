using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.ViewModels
{
    public class DictionaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Word> Words { get; set; }
        public int CollectionId { get; set; }
    }
}
