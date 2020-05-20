using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Original { get; set; }
        public List<Translation> Translations { get; set; }
        public int DictionaryId { get; set; }
    }
}
