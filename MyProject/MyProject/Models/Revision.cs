namespace MyProject.Models
{
    public class Revision
    {
        public int RevisionWordId { get; set; }
        public string Word { get; set; }
        public string CorrectOption { get; set; }
        public int[] Options { get; set; }
    }
}
