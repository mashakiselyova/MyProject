using System;

namespace MyProject.Models
{
    public class RevisionWord
    {
        public int Id { get; set; }
        public Word Word { get; set; }
        public int CollectionId { get; set; }
        public DateTime NextReview { get; set; }
        public int DaysUntilReview { get; set; }
    }
}
