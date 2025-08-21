using LanTutor.DataModels;
using LanTutor.Database;
using System.Linq;

namespace LanTutor.Services
{
    public class ScoreService
    {
        private readonly LanTutorContext _context;

        public ScoreService(LanTutorContext context)
        {
            _context = context;
        }

        public void SaveScore(int userId, int wordId, ScoreParameters wordScore, ScoreParameters descriptionScore)
        {
            var existingScore = _context.Scores
                .FirstOrDefault(s => s.UserId == userId && s.WordId == wordId);

            if (existingScore != null)
            {
                existingScore.Attempts = wordScore.Attempts + descriptionScore.Attempts;
                existingScore.ScoreValue = wordScore.Score + descriptionScore.Score;
                existingScore.TimeSpent = wordScore.TimeSpent; // or combine both
            }
            else
            {
                _context.Scores.Add(new Score
                {
                    UserId = userId,
                    WordId = wordId,
                    Attempts = wordScore.Attempts + descriptionScore.Attempts,
                    ScoreValue = wordScore.Score + descriptionScore.Score,
                    TimeSpent = wordScore.TimeSpent
                });
            }

            _context.SaveChanges();
        }
    }
}
