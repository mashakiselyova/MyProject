using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string  LanguageFrom { get; set; }
        public string LanguageTo { get; set; }

        [NotMapped]
        public string Name { get { return LanguageFrom + " - " + LanguageTo; } }
        public List<Word> Words { get; set; }
        public List<Collection> Collections { get; set; }
    }
}
