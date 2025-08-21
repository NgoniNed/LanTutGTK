using System;
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading XML session: {ex.Message}");
                return new List<WordTransDef>();
            }
        }

        public WordTransDef GetQuestion(int index)
        {
            try
            {
                if (sessionNodes == null || sessionNodes.Count == 0 || index < 0 || index >= sessionNodes.Count)
                {
                    return null; // Or throw an exception, log an error, etc.
                }
                currentIndex = index;
                return LanTutorXMLMoving.GetCurrentQuestionl(index, ref sessionNodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting XML question: {ex.Message}");
                return null;
            }
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
            try
            {
                return GetQuestion(index).lWordScore;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error getting XML score: {ex.Message}");
                return new ScoreParameters();
            }

        }
        public void UpdateScore(int index, ScoreParameters wordScore, ScoreParameters descriptionScore)
        {
            try
            {
                var word = GetQuestion(index);
                word.lWordScore = wordScore;
                word.lDescriptionScore = descriptionScore;
                LanTutorXMLMoving.UpdateCurrentNodeList(word, index, ref sessionNodes);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error updating XML score: {ex.Message}");
            }
        }
    }
}
