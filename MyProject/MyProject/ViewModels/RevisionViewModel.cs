using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.ViewModels
{
    public class RevisionViewModel
    {
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public List<PracticeWord> PracticeWords { get; set; }
    }
}
