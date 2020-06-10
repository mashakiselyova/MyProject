using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using MyProject.Services;

namespace MyProject.DB
{
    public class RevisionService
    {
        private ApplicationContext _context;

        public RevisionService(ApplicationContext context)
        {
            _context = context;
        }

        public List<PracticeWord> GetWordsForRevision(int collectionId)
        {
            var words = _context.RevisionWords.Include(r => r.Word)
                .Where(r => r.CollectionId == collectionId)
                .Where(r => r.NextReview.Date <= DateTime.Today.Date)
                .Select(r => new PracticeWord
                {
                    RevisionWordId = r.Id,
                    Word = r.Word.Original,
                    CorrectOption = r.Word.Translation
                }).ToList();
            foreach(var word in words)
            {
                word.Options = PopulateTranslationOptions(word.CorrectOption, collectionId);
            }
            return words;
        }

        private List<string> PopulateTranslationOptions(string correctOption, int collectionId)
        {
            var options = new List<string>();
            options.Add(correctOption);
            var WordsInCollection = _context.RevisionWords.Where(r => r.CollectionId == collectionId).Count();
            options.Add(ChooseOption(collectionId, WordsInCollection));
            options.Add(ChooseOption(collectionId, WordsInCollection));
            return options;
        }

        private string ChooseOption(int collectionId, int wordsQuantity)
        {
            var random = new Random();
            var option = _context.RevisionWords
                .Where(r => r.CollectionId == collectionId)
                .Select(r => r.Word.Translation)
                .Skip(random.Next(wordsQuantity))
                .Take(1);
            return option.First();
        }
    }
}
