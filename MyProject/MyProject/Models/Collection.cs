

using System.Collections.Generic;

namespace MyProject.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<RevisionWord> RevisionWords { get; set; }
    }
}
