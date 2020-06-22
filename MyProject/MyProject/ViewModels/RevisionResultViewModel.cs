using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.ViewModels
{
    public class RevisionResultViewModel
    {
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public List<RevisionResultWord> RevisionResultWords { get; set; }
    }
}
