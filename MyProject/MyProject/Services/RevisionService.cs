﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyProject.DB;
using MyProject.Models;

namespace MyProject.Services
{
    public class RevisionService
    {
        private readonly ApplicationContext _context;
        private const int NumberOfOptions = 3;
        private const int WordsPerSession = 10;

        public RevisionService(ApplicationContext context)
        {
            _context = context;
        }

        public List<PracticeWord> GetWordsForRevision(int collectionId)
        {
            var dictionaryId = _context.Collections.Find(collectionId).DictionaryId;
            var words = _context.RevisionWords.Include(r => r.Word)
                .Where(r => r.CollectionId == collectionId)
                .Where(r => r.NextReview.Date <= DateTime.Today.Date)
                .OrderBy(r => r.NextReview)
                .Take(WordsPerSession)
                .Select(r => new PracticeWord
                {
                    RevisionWordId = r.Id,
                    Word = r.Word.Original,
                    CorrectOption = r.Word.Translation,
                    DaysUntilRevision = r.DaysUntilReview
                }).ToList();
            foreach (var word in words)
            {
                word.Options = PopulateTranslationOptions(word.CorrectOption, dictionaryId);
            }
            words.Sort((x, y) => { return new Random().Next(-1, 2); });
            return words;
        }

        public List<RevisionResultWord> EvaluateRevision(List<PracticeWord> practiceWords)
        {
            foreach (var practiceWord in practiceWords)
                practiceWord.CalculateDaysUntilNextRevision();

            var revisionResultWords = new List<RevisionResultWord>();
            foreach (var practiceWord in practiceWords)
            {
                revisionResultWords.Add(new RevisionResultWord
                {
                    RevisionWordId = practiceWord.RevisionWordId,
                    Word = practiceWord.Word,
                    CorrectOption = practiceWord.CorrectOption,
                    IsCorrect = practiceWord.CorrectOption == practiceWord.SelectedOption,
                    DaysUntilRevision = practiceWord.DaysUntilRevision
                });
            }
            return revisionResultWords;
        }

        public void SaveRevisionResult(List<RevisionResultWord> resultWords)
        {
            foreach (var resultWord in resultWords)
            {
                var revisionWord = _context.RevisionWords.Find(resultWord.RevisionWordId);
                revisionWord.DaysUntilReview = resultWord.DaysUntilRevision;
                revisionWord.NextReview = DateTime.Now.AddDays(resultWord.DaysUntilRevision);
                _context.SaveChanges();
            }
        }

        private List<string> PopulateTranslationOptions(string correctOption, int dictionaryId)
        {
            var options = new List<string>();
            options.Add(correctOption);
            while (options.Count < NumberOfOptions)
            {
                var option = ChooseOption(dictionaryId);
                if (options.Contains(option))
                    continue;
                else
                    options.Add(option);
            }
            options.Sort((x, y) => { return new Random().Next(-1, 2); });
            return options;
        }

        private string ChooseOption(int dictionaryId)
        {
            var wordQuantity = _context.Words.Where(w => w.DictionaryId == dictionaryId).Count();
            var random = new Random();
            var option = _context.Words.Where(w => w.DictionaryId == dictionaryId)
                .Select(w => w.Translation)
                .Skip(random.Next(wordQuantity))
                .Take(1);
            return option.First();
        }
    }
}
