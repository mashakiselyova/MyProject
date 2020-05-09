﻿using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DB
{
    public class VocabularyService
    {
        private ApplicationContext _context;

        public VocabularyService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddVocabularyAsync(Vocabulary vocabulary)
        {
            
            await _context.Vocabularies.AddAsync(vocabulary);
            await _context.SaveChangesAsync();
        }

        public List<Vocabulary> GetAllVocabularies()
        {
            return _context.Vocabularies.ToList();
        }

        public Vocabulary GetVocabulary(int id)
        {
            return _context.Vocabularies.Find(id);
        }
    }
}