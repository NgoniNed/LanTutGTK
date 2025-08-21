using System;
using System.Xml.Serialization;

namespace LanTutor
{
    /// <summary>
    /// The parameters under which a grading is assigned
    /// </summary>
    [Serializable]
    public class ScoreParameters
    {
        /// <summary>
        /// Score assigned for answering the question
        /// </summary>
        public double Score
        {
            get;
            set;
        }
        /// <summary>
        /// Number of times the question has been attempted
        /// </summary>
        public int Attempts
        {
            get;
            set;
        }
        /// <summary>
        /// Time spent on a question
        /// </summary>
        public string TimeSpent
        {
            get;
            set;
        }
        /// <summary>
        /// Updates the Score variabel
        /// </summary>
        /// <param name="updateInfo"></param>
        public void UpdateScore(double updateInfo)
        {
            Score = updateInfo;
        }
        /// <summary>
        /// Updates the attempts variable
        /// </summary>
        /// <param name="updateInfo"></param>
        public void UpdateAttemts(int updateInfo)
        {
            Attempts = updateInfo;
        }
        /// <summary>
        /// Updates time spent variable
        /// </summary>
        /// <param name="updateInfo"></param>
        public void UpdateTimeSpent(string updateInfo)
        {
            TimeSpent = updateInfo;
        }
    }
}