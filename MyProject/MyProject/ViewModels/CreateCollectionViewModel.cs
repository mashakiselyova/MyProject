using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.ViewModels
{
    public class CreateCollectionViewModel
    {
        public string CollectionName { get; set; }
        public int VocabularyId { get; set; }
        public List<Vocabulary> Vocabularies { get; set; }
    }
}
