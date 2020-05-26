namespace MyProject.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Original { get; set; }
        public string Translation { get; set; }
        public int DictionaryId { get; set; }
    }
}
