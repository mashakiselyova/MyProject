﻿using System.Collections.Generic;

namespace MyProject.Models
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Word> Words { get; set; }
        public List<Collection> Collections { get; set; }
    }
}