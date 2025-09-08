using System;
using System.Collections.Generic;
using System.Linq;
using LanTutor.Database;
using LanTutor.DataModels;
using LanTutor.Interfaces;
using LanTutor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LanTutor.Adapters
{
    public class SqliteAdapter : ILanTutorFrontend
    {
        private List<WordTransDef> sessionWords;
        private readonly IWordService wordService;
        private readonly ScoreService scoreService;
        private readonly ISessionService sessionService;
        private readonly int userId = 1;
        private Session currentSession;
        private readonly IConfigurationService _configurationService;

        public SqliteAdapter(IWordService wordService, ScoreService scoreService, ISessionService sessionService, IConfigurationService configurationService)
        {
            this.wordService = wordService ?? throw new ArgumentNullException(nameof(wordService));
            this.scoreService = scoreService ?? throw new ArgumentNullException(nameof(scoreService));
            this.sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));

            userId = 1;
            var language = _configurationService.GetUserSettings().ActiveLanguage;
            currentSession = sessionService.StartSession(userId, language);
        }

        public List<WordTransDef> LoadSession(string language)
        {

            var userId = 1; 
            var priorityIds = scoreService.GetPriorityWordIds(userId);
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

        public WordTransDef GetQuestion(int index)
        {
            if (sessionWords == null || sessionWords.Count == 0 || index < 0 || index >= sessionWords.Count)
            {
                return null;
            }
            return sessionWords[index];
        }

        public void SubmitAnswer(int index, string userAnswer)
        {
            var word = sessionWords[index];

            double wordScoreIncrement = 0;
            double descriptionScoreIncrement = 0;
            bool wordCorrect = string.Equals(userAnswer, word.lTrans, StringComparison.OrdinalIgnoreCase);
            bool descriptionCorrect = word.ldef.Any(def => userAnswer.Contains(def));

            if (wordCorrect)
            {
                wordScoreIncrement = 10;
            }
            else
            {
                wordScoreIncrement = -5;
            }

            if (descriptionCorrect)
            {
                descriptionScoreIncrement = 5;
            }
            else
            {
                descriptionScoreIncrement = -2;
            }

            word.lWordScore.Score = Math.Max(0, Math.Min(100, word.lWordScore.Score + wordScoreIncrement));
            word.lDescriptionScore.Score = Math.Max(0, Math.Min(100, word.lDescriptionScore.Score + descriptionScoreIncrement));
            word.lWordScore.Attempts += 1;
            word.lDescriptionScore.Attempts += 1;

            scoreService.SaveScore(1, word.Id, word.lWordScore, word.lDescriptionScore);
        }

        public void EndSession()
        {
            sessionService.EndSession(currentSession.SessionId);
        }

        public int GetTotalQuestions()
        {
            return sessionWords.Count;
        }

        public ScoreParameters GetScoreForQuestion(int index)
            => sessionWords[index].lWordScore;

        public void UpdateScore(int index, ScoreParameters wordScore, ScoreParameters descriptionScore)
        {
            var sessionWord = sessionWords[index];
            sessionWord.lWordScore.Score = wordScore.Score;
            sessionWord.lWordScore.Attempts = wordScore.Attempts;
            sessionWord.lDescriptionScore.Score = descriptionScore.Score;
            sessionWord.lDescriptionScore.Attempts = descriptionScore.Attempts;
        }
    }
}

