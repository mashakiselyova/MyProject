namespace MyProject.ViewModels
{
    public class CreateWordForRevisionViewModel
    {
        public int DictionaryId { get; set; }
        public int CollectionId { get; set; }
        public string Original { get; set; }
        public string Translation { get; set; }
    }
}
