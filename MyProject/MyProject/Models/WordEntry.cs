using System;
using System.Collections.Generic;

namespace MyProject.Models
{
    public class WordEntry
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public List<Translation> Translations { get; set; }
        public byte[] Picture { get; set; }
        public TimeSpan TimeUntillRevision { get; set; }
    }
}
