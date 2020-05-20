using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.ViewModels
{
    public class CreateCollectionViewModel
    {
        public string CollectionName { get; set; }
        public int LanguageId { get; set; }
        public List<Language> Languages { get; set; }
    }
}
