using LanTutor.Interfaces;
using LanTutor.DataModels;
using LanTutor.Services;
using System.Collections.Generic;
using System.Linq;

namespace LanTutor.Adapters
{
    public class AutoAdapter : ILanTutorFrontend
    {
        private readonly ILanTutorFrontend activeAdapter;

        public AutoAdapter(IWordService wordService, Services.ScoreService scoreService)
        {
            var settings = LTReadFile.getUserSettings();
            var language = settings.ActiveLanguage;

            bool dbHasWords = wordService.GetAllWords().Any();

            if (settings.ActiveLanguage.Contains("xml") || !dbHasWords)
            {
                var xmlAdapter = new XmlAdapter();
                var words = xmlAdapter.LoadSession(language);

                if (!dbHasWords)
                {
                    foreach (var word in words)
                    {
                        wordService.AddWord(word);
                        foreach (var def in word.ldef)
                        {
                            wordService.AddDefinition(word.Id, def);
                        }
                    }
                }
                activeAdapter = new SqliteAdapter(wordService, scoreService); //Switch to DB after migration. DI.
            }
            else
            {
                activeAdapter = new SqliteAdapter(wordService, scoreService);  //DI.
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