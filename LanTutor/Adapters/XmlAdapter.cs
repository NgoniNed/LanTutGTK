using System.Collections.Generic;
using System.Xml;
using LanTutor.DataModels;
using LanTutor.Interfaces;

namespace LanTutor.Adapters
{
    public class XmlAdapter : ILanTutorFrontend
    {
        private XmlNodeList sessionNodes;
        private List<WordTransDef> sessionWords;
        private int currentIndex = 0;

        public List<WordTransDef> LoadSession(string language)
        {
            string[] reportCards = LTReadFile.GetReportCards;
            foreach (var report in reportCards)
            {
                if (report.Contains(language))
                {
                    sessionNodes = LanTutorXMLMoving.LoadSessionQuestions(LTReadFile.LoadXMLFile(report), "WordTransDefLibrary/SessionLibrary/WordTransDef");
                    break;
                }
            }

            sessionWords = new List<WordTransDef>();
            for (int i = 0; i < sessionNodes.Count; i++)
            {
                sessionWords.Add(LanTutorXMLMoving.GetCurrentQuestionl(i, ref sessionNodes));
            }

            return sessionWords;
        }

        public WordTransDef GetQuestion(int index)
        {
            currentIndex = index;
            return LanTutorXMLMoving.GetCurrentQuestionl(index, ref sessionNodes);
        }

        public void SubmitAnswer(int index, string userAnswer)
        {

        }

        public void EndSession()
        {

        }

        public int GetTotalQuestions() => sessionNodes?.Count ?? 0;

        public ScoreParameters GetScoreForQuestion(int index)
        {
            return GetQuestion(index).lWordScore;
        }

        public void UpdateScore(int index, ScoreParameters wordScore, ScoreParameters descriptionScore)
        {
            var word = GetQuestion(index);
            word.lWordScore = wordScore;
            word.lDescriptionScore = descriptionScore;
            LanTutorXMLMoving.UpdateCurrentNodeList(word, index, ref sessionNodes);
        }
    }
}
