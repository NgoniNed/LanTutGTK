using LanTutor.DataModels;
using LanTutor.Database;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LanTutor.Services
{
    

    public class WordService : IWordService
    {
        private readonly LanTutorContext _context;

        public WordService(LanTutorContext context)
        {
            _context = context;
        }

        public List<WordTransDef> GetAllWords()
        {
            var words = _context.Words
                .Include(w => w.Definitions)
                .ToList();

            foreach (var word in words)
            {
                word.ldef = word.Definitions?.Select(d => d.Description).ToList() ?? new List<string>();
            }

            return words;
        }


        public WordTransDef GetWordById(int wordId)
        {
            var word = _context.Words
                .Include(w => w.Definitions)
                .FirstOrDefault(w => w.Id == wordId);

            if (word != null)
            {
                word.ldef = word.Definitions.Select(d => d.Description).ToList();
            }

            return word;
        }


        public void AddWord(WordTransDef word)
        {
            _context.Words.Add(word);
            _context.SaveChanges();
        }

        public void AddDefinition(int wordId, string description)
        {
            var definition = new Definition
            {
                WordId = wordId,
                Description = description
            };
            _context.Definitions.Add(definition);
            _context.SaveChanges();
        }

        public List<Definition> GetDefinitionsForWord(int wordId)
        {
            return _context.Definitions
                .Where(d => d.WordId == wordId)
                .ToList();
        }
    }

}
