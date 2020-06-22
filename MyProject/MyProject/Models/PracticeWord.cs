using System.Collections.Generic;

namespace MyProject.Models
{
    public class PracticeWord
    {
        public int RevisionWordId { get; set; }
        public string Word { get; set; }
        public string CorrectOption { get; set; }
        public string SelectedOption { get; set; }
        public List<string> Options { get; set; }
        public int DaysUntilRevision { get; set; }

        public void CalculateDaysUntilNextRevision()
        {
            if (CorrectOption == SelectedOption)
                DaysUntilRevision *= 2;
            else
                DaysUntilRevision = 1;
        }
    }
}
