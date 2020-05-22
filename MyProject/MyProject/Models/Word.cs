using System.Collections.Generic;

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
