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
        private readonly ApplicationContext _context;
        private const int NumberOfOptions = 3;

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
                word.Options = PopulateTranslationOptions(word.CorrectOption);
            }
            return words;
        }

        private List<string> PopulateTranslationOptions(string correctOption)
        {
            var options = new List<string>();
            options.Add(correctOption);
            while (options.Count < 3)
            {
                var option = ChooseOption();
                if (options.Contains(option))
                    continue;
                else
                    options.Add(option);
            }
            return options;
        }

        private string ChooseOption()
        {
            var wordQuantity = _context.Words.Count();
            var random = new Random();
            var option = _context.Words.Select(w=>w.Translation).Skip(random.Next(wordQuantity)).Take(1);
            return option.First();
        }
    }
}
