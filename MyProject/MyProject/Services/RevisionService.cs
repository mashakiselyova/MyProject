using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.DB
{
    public class RevisionService
    {
        private ApplicationContext _context;

        public RevisionService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Revision> GetWordsForRevision(int collectionId)
        {
            //var revisionWords = _context.RevisionWords.Include(r => r.Word).Where(r => r.CollectionId == collectionId)
            //    .Where(r => r.NextReview.Date <= DateTime.Today.Date).ToList();

            var words = _context.RevisionWords.Include(r => r.Word).Where(r => r.CollectionId == collectionId)
                .Where(r => r.NextReview.Date <= DateTime.Today.Date).Select(r => new Revision
                {
                    RevisionWordId = r.Id,
                    Word = r.Word.Original,
                    CorrectOption = r.Word.Translation
                }).ToList();

            return words;


        }
    }
}
