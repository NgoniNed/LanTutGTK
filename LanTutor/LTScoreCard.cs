using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace LanTutor
{
    /// <summary>
    /// Score Card information for all the WordTransDefDicts in the session
    /// </summary>
    [Serializable]
    public struct LTSessionScoreCard
    {
        /// <summary>
        /// variable holding the score card information for the session
        /// </summary>
        public List<WordTransDefDict> SessionLibrary { get; set; }
    }
    /// <summary>
    /// Score Card information for all the WordObjects in the session
    /// </summary>
    [Serializable]
    public struct LTScoreCard
    {
        /// <summary>
        /// variable holding the score card information for the session
        /// </summary>
        public List<WordObject> SessionLibrary { get; set; }
    }
    /// <summary>
    /// Score Card information for all the WordTransDefs in the session
    /// </summary>
    [Serializable]
    public struct WordTransDefLibrary
    {
        /// <summary>
        /// variable holding the score card information for the session
        /// </summary>
        public List<WordTransDef> SessionLibrary { get; set; }
        
    }
    /// <summary>
    /// Object which holds word, translation and defination
    /// </summary>
    [Serializable]
    public struct WordTransDef
    {
        /// <summary>
        /// word property
        /// </summary>
        public string lword
        {
            get;
            set;
        }
        /// <summary>
        /// Translation property
        /// </summary>
        public string lTrans
        {
            get;
            set;
        }
        /// <summary>
        /// List of the word's defination/description
        /// </summary>
        public List<string> ldef
        {
            get;
            set;
        }
        /// <summary>
        /// Updates the definations/description variable
        /// </summary>
        /// <param name="appendInfo"></param>
        public void Updateldef(string appendInfo)
        {
            ldef.Add(appendInfo);
        }
        /// <summary>
        /// Updates the word variable
        /// </summary>
        /// <param name="appendInfo"></param>
        public void Updatelword(string appendInfo)
        {
            lword = lword + "==>>" + appendInfo;
        }
        /// <summary>
        /// Updates the Word's score
        /// </summary>
        /// <param name="appendInfo"></param>
        public void UpdatelWordScore(ScoreParameters appendInfo)
        {
            lWordScore = appendInfo;
        }
        /// <summary>
        /// Updates the Description/Defination's score
        /// </summary>
        /// <param name="appendInfo"></param>
        public void UpdatelDescriptionScore(ScoreParameters appendInfo)
        {
            lDescriptionScore = appendInfo;
        }
        /// <summary>
        /// Word Score property
        /// </summary>
        [XmlElement(ElementName = "WordScore")]
        public ScoreParameters lWordScore
        {
            get;
            set;
        }
        /// <summary>
        /// Description/Definations Score Property
        /// </summary>
        [XmlElement(ElementName = "DescriptionScore")]
        public ScoreParameters lDescriptionScore
        {
            get;
            set;
        }
        /// <summary>
        /// Prints the Information of the WordTransDef object
        /// </summary>
        [XmlIgnore]
        public bool PrintInfo
        {
            get
            {
                Console.WriteLine("\n" + lword + "\n\t\t" + lTrans + "\n");
                foreach (string tmp in ldef)
                {
                    Console.WriteLine(tmp);
                }
                Console.WriteLine("\nWord Scores\n\nScore: " + lWordScore.Score + "\t\tAttempts: " + lWordScore.Attempts + "\t\tTime: "+ lWordScore.TimeSpent);
                Console.WriteLine("\nDescription Scores\n\nScore: " + lDescriptionScore.Score + "\t\tAttempts: " + lDescriptionScore.Attempts + "\t\tTime: " + lDescriptionScore.TimeSpent);

                Console.WriteLine();
                return true;
            }
            
        }
    }
    /// <summary>
    /// Object which holds word, translation and defination
    /// </summary>
    [Serializable]
    public struct WordTransDefDict
    {
        /// <summary>
        /// Word's property
        /// </summary>
        [XmlElement(ElementName = "Word")]
        public string lword
        {
            get;
            set;
        }
        /// <summary>
        /// Translation property
        /// </summary>
        [XmlElement(ElementName ="Translation")]
        public string lTrans
        {
            get;
            set;
        }
        /// <summary>
        /// List of the word's defination/description
        /// </summary>
        [XmlElement(ElementName = "Definations")]
        public List<string> ldef
        {
            get;
            set;
        }
        /// <summary>
        /// Translation's Score Property
        /// </summary>
        [XmlElement(ElementName = "WordScore")]
        public ScoreParameters lWordScore
        {
            get;
            set;
        }
        /// <summary>
        /// Description/Definations Score Property
        /// </summary>
        [XmlElement(ElementName = "DescriptionScore")]
        public ScoreParameters lDescriptionScore
        {
            get;
            set;
        }
        /// <summary>
        /// Prints the Information of the WordTransDefDict object
        /// </summary>
        [XmlIgnore]
        public bool PrintInfo
        {
            get
            {
                Console.WriteLine("\n" + lword + "\t\t" + lTrans + "\n");
                foreach (string tmp in ldef)
                {
                    Console.WriteLine(tmp);
                }
                Console.WriteLine();
                return true;
            }

        }
    }
    /// <summary>
    /// Object which holds word, translation and defination
    /// </summary>
    [Serializable]
    public struct WordDictObject
    {
        /// <summary>
        /// Word's property
        /// </summary>
        public string lword { get; set; }
        /// <summary>
        /// List of the word's defination/description
        /// </summary>
        public List<string> lwordDescription { get; set; }
        /// <summary>
        /// Translation property
        /// </summary>
        public string frgnWord { get; set; }
        /// <summary>
        /// Translation's Score Property
        /// </summary>
        public ScoreParameters lWordScore { get; set; }
        /// <summary>
        /// Description/Defination's Score Property
        /// </summary>
        public ScoreParameters lDescriptionScore { get; set; }
        
    }
    /// <summary>
    /// Word Object holding score, translation, defination/
    /// description and score information.
    /// </summary>
    [Serializable]
    public struct WordObject
    {
        /// <summary>
        /// The non translated word variable
        /// </summary>
        public string lword { get; set; }
        /// <summary>
        /// The defination/Description variable
        /// </summary>
        public string lwordDescription { get; set; }
        /// <summary>
        /// The translation variables score card
        /// </summary>
        public WordScoreCard frgnWord { get; set; }
        /// <summary>
        /// The defination/Description variables score card
        /// </summary>
        public WordScoreCard localWorddiscrp { get; set; }
    }
    /// <summary>
    /// Score Card for each word
    /// </summary>
    [Serializable]
    public struct WordScoreCard
    {
        /// <summary>
        /// Name of the Scored Parameter
        /// </summary>
        public string lname{ get; set; }
        /// <summary>
        /// Score Information
        /// </summary>
        public ScoreParameters ScoreInfo { get; set; }
    }
    /// <summary>
    /// The parameters under which a grading is assigned
    /// </summary>
    [Serializable]
    public struct ScoreParameters
    {
        /// <summary>
        /// Score assigned for answering the question
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Number of times the question has been attempted
        /// </summary>
        public int Attempts { get; set; }
        /// <summary>
        /// Time spent on a question
        /// </summary>
        public string TimeSpent { get; set; }
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
