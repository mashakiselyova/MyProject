namespace MyProject.Models
{
    public class RevisionResultWord
    {
        public int RevisionWordId { get; set; }
        public string Word { get; set; }
        public string CorrectOption { get; set; }
        public bool IsCorrect { get; set; }
        public int DaysUntilRevision { get; set; }
    }
}
