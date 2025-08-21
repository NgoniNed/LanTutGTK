using LanTutor.Interfaces;
using LanTutor.DataModels;
using LanTutor.Services;
using System.Collections.Generic;
using System.Linq;

namespace LanTutor.Adapters
{
    public class AutoAdapter : ILanTutorFrontend
    {
        private ILanTutorFrontend activeAdapter;

        public AutoAdapter()
        {
            var settings = LTReadFile.getUserSettings();
            var language = settings.ActiveLanguage;

            var context = new LanTutor.Database.LanTutorContext();
            bool dbHasWords = context.Words.Any();

            if (settings.ActiveLanguage.Contains("xml") || !dbHasWords)
            {
                var xmlAdapter = new XmlAdapter();
                var words = xmlAdapter.LoadSession(language);

                if (!dbHasWords)
                {
                    var wordService = new WordService(context);
                    foreach (var word in words)
                    {
                        wordService.AddWord(word);
                        foreach (var def in word.ldef)
                        {
                            wordService.AddDefinition(word.Id, def);
                        }
                    }
                }

                activeAdapter = new SqliteAdapter(); // switch to DB after migration
            }
            else
            {
                activeAdapter = new SqliteAdapter();
            }
        }

        public List<WordTransDef> LoadSession(string language) => activeAdapter.LoadSession(language);
        public WordTransDef GetQuestion(int index) => activeAdapter.GetQuestion(index);
        public void SubmitAnswer(int index, string userAnswer) => activeAdapter.SubmitAnswer(index, userAnswer);
        public void EndSession() => activeAdapter.EndSession();
        public int GetTotalQuestions() => activeAdapter.GetTotalQuestions();
        public ScoreParameters GetScoreForQuestion(int index) => activeAdapter.GetScoreForQuestion(index);
        public void UpdateScore(int index, ScoreParameters wordScore, ScoreParameters descriptionScore) =>
            activeAdapter.UpdateScore(index, wordScore, descriptionScore);
    }
}
