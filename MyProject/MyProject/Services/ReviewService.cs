using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.DB
{
    public class ReviewService
    {
        private ApplicationContext _context;

        public ReviewService(ApplicationContext context)
        {
            _context = context;
        }

        public Collection GetWordsForRevision(int collectionId)
        {
            var revisionWords = _context.RevisionWords.Include(r => r.Word).Where(r => r.CollectionId == collectionId)
                .Where(r => r.NextReview.Date <= DateTime.Today.Date).ToList();
            var collection = _context.Collections.Find(collectionId);
            return new Collection { RevisionWords = revisionWords, Id = collectionId, Name = collection.Name };
        }
    }
}
