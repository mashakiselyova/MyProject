using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    public class AddRevisionWordViewModel
    {
        public int DictionaryId { get; set; }
        public int CollectionId { get; set; }
        public string DictionaryName { get; set; }
        public List<Word> Words { get; set; }
    }
}
