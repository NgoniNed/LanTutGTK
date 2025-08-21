using System;
using System.Collections.Generic;
using System.Linq;
using LanTutor.Database;
using LanTutor.DataModels;
using LanTutor.Interfaces;
using LanTutor.Services;

namespace LanTutor.Adapters
{
    public class SqliteAdapter : ILanTutorFrontend
    {
        private List<WordTransDef> sessionWords;
        private readonly WordService wordService = new WordService(new LanTutorContext());
        /*
        public List<WordTransDef> LoadSession(string language)
        {
            sessionWords = wordService.GetAllWords();
            return sessionWords;
        }*/
        public List<WordTransDef> LoadSession(string language)
        {
            var userId = 1; // or get from context
            var priorityIds = new ScoreService(new LanTutorContext()).GetPriorityWordIds(userId);
            var allWords = wordService.GetAllWords();

            var prioritizedWords = priorityIds
                .Select(id => allWords.FirstOrDefault(w => w.Id == id))
                .Where(w => w != null)
                .ToList();

            var remainingWords = allWords
                .Where(w => !priorityIds.Contains(w.Id))
                .ToList();

            sessionWords = prioritizedWords.Concat(remainingWords).ToList();
            return sessionWords;
        }

        public WordTransDef GetQuestion(int index) => sessionWords[index];
        public void SubmitAnswer(int index, string userAnswer)
        {
            var word = sessionWords[index];
            var scoreService = new ScoreService(new LanTutorContext());

            // For now, simulate score update
            word.lWordScore.Score += 5;
            word.lWordScore.Attempts += 1;
            word.lWordScore.TimeSpent = "10s";

            word.lDescriptionScore.Score += 3;
            word.lDescriptionScore.Attempts += 1;
            word.lDescriptionScore.TimeSpent = "15s";

            scoreService.SaveScore(1, word.Id, word.lWordScore, word.lDescriptionScore); // assuming userId = 1
        }

        public void EndSession()
        {
            /* persist session */
        }

        public int GetTotalQuestions()
        {
            return sessionWords.Count;
        }

        public ScoreParameters GetScoreForQuestion(int index)
            => sessionWords[index].lWordScore;

        public void UpdateScore(int index, ScoreParameters wordScore, ScoreParameters descriptionScore)
        {
            sessionWords[index].lWordScore = wordScore;
            sessionWords[index].lDescriptionScore = descriptionScore;
        }
    }

}
