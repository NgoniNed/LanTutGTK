using System;
using System.Collections.Generic;
using LanTutor.DataModels;

namespace LanTutor.Interfaces
{
    public interface ILanTutorFrontend
    {
        List<WordTransDef> LoadSession(string language);
        WordTransDef GetQuestion(int index);
        void SubmitAnswer(int index, string userAnswer);
        void EndSession();
        int GetTotalQuestions();
        ScoreParameters GetScoreForQuestion(int index);
        void UpdateScore(int index, ScoreParameters wordScore, ScoreParameters descriptionScore);

    }

}
