using System.Collections.Generic;

namespace MyProject.Models
{
    public class Vocabulary
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<WordEntry> Words { get; set; }
    }
}
