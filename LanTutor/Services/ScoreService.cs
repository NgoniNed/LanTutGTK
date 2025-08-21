using LanTutor.DataModels;
using LanTutor.Database;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LanTutor.Services
{
    public class ScoreService
    {
        private readonly LanTutorContext _context;

        public ScoreService(LanTutorContext context)
        {
            _context = context;
        }

        public void SaveScore(int userId, int wordId, ScoreParameters wordScoreParams, ScoreParameters descriptionScoreParams)
        {
            var word = _context.Words.FirstOrDefault(w => w.Id == wordId);
            if (word == null) return;

            var wordScore = _context.WordScores.FirstOrDefault(ws => ws.WordTransDefId == wordId);
            if (wordScore == null)
            {
                wordScore = new WordScore { WordTransDefId = wordId, Score = 0, Attempts = 0, TimeSpent = "0" };
                _context.WordScores.Add(wordScore);
            }

            var descriptionScore = _context.DescriptionScores.FirstOrDefault(ds => ds.WordTransDefId == wordId);
            if (descriptionScore == null)
            {
                descriptionScore = new DescriptionScore { WordTransDefId = wordId, Score = 0, Attempts = 0, TimeSpent = "0" };
                _context.DescriptionScores.Add(descriptionScore);
            }

            wordScore.Score = wordScoreParams.Score;
            wordScore.Attempts = wordScoreParams.Attempts;
            wordScore.TimeSpent = wordScoreParams.TimeSpent;

            descriptionScore.Score = descriptionScoreParams.Score;
            descriptionScore.Attempts = descriptionScoreParams.Attempts;
            descriptionScore.TimeSpent = descriptionScoreParams.TimeSpent;

            _context.SaveChanges();
        }

        public List<object> GetScoresByUser(int userId)
        {
            var wordScores = _context.WordScores
                .Where(ws => ws.WordTransDef.Id == userId)
                .ToList<object>();

            var descriptionScores = _context.DescriptionScores
               .Where(ds => ds.WordTransDef.Id == userId)
               .ToList<object>();

            return wordScores.Concat(descriptionScores).ToList();
        }


        public List<int> GetPriorityWordIds(int userId)
        {
            return _context.Scores
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Attempts)
                .ThenBy(s => s.ScoreValue)
                .Select(s => s.WordId)
                .ToList();
        }
    }
}