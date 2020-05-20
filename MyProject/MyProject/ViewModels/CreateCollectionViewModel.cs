using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.ViewModels
{
    public class CreateCollectionViewModel
    {
        public string CollectionName { get; set; }
        public int DictionaryId { get; set; }
        public List<Dictionary> Dictionaries { get; set; }
    }
}
