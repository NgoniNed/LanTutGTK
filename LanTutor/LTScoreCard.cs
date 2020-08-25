using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace LanTutor
{
    /*public struct SessionWordSet
    {
        public List<string> motherTongue
        {
            get;
            set;
        }
        public List<string> foreignTongue
        {
            get;
            set;
        }
    }*/
    [Serializable]
    public struct LTSessionScoreCard
    {
        public List<WordTransDefDict> SessionLibrary { get; set; }
    }
    [Serializable]
    public struct LTScoreCard
    {
        public List<WordObject> SessionLibrary { get; set; }
    }
    [Serializable]
    public struct WordTransDefLibrary
    {
        public List<WordTransDef> SessionLibrary { get; set; }
        
    }
    [Serializable]
    public struct WordTransDef
    {
        public string lword
        {
            get;
            set;
        }
        public string lTrans
        {
            get;
            set;
        }
        public List<string> ldef
        {
            get;
            set;
        }

        public void Updateldef(string appendInfo)
        {
            ldef.Add(appendInfo);
        }
        public void Updatelword(string appendInfo)
        {
            lword = lword + "==>>" + appendInfo;
        }
        public void UpdatelWordScore(ScoreParameters appendInfo)
        {
            lWordScore = appendInfo;
        }
        public void UpdatelDescriptionScore(ScoreParameters appendInfo)
        {
            lDescriptionScore = appendInfo;
        }
        [XmlElement(ElementName = "WordScore")]
        public ScoreParameters lWordScore
        {
            get;
            set;
        }
        [XmlElement(ElementName = "DescriptionScore")]
        public ScoreParameters lDescriptionScore
        {
            get;
            set;
        }
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
    [Serializable]
    public struct WordTransDefDict
    {
        [XmlElement(ElementName = "Word")]
        public string lword
        {
            get;
            set;
        }
        [XmlElement(ElementName ="Translation")]
        public string lTrans
        {
            get;
            set;
        }
        [XmlElement(ElementName = "Definations")]
        public List<string> ldef
        {
            get;
            set;
        }
        [XmlElement(ElementName = "WordScore")]
        public ScoreParameters lWordScore
        {
            get;
            set;
        }
        [XmlElement(ElementName = "DescriptionScore")]
        public ScoreParameters lDescriptionScore
        {
            get;
            set;
        }
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
    [Serializable]
    public struct WordDictObject
    {
        //[XmlAttribute(AttributeName="Mother Tongue")]
        public string lword { get; set; }
        public List<string> lwordDescription { get; set; }
        public string frgnWord { get; set; }
        public ScoreParameters lWordScore { get; set; }
        public ScoreParameters lDescriptionScore { get; set; }
        
    }
    [Serializable]
    public struct WordObject
    {
        //[XmlAttribute(AttributeName="Mother Tongue")]
        public string lword { get; set; }
        public string lwordDescription { get; set; }
        public WordScoreCard frgnWord { get; set; }
        public WordScoreCard localWorddiscrp { get; set; }
        //[XmlIgnore]
        /*public void addDescription(string discr)
        {
            lwordDescription.Add(discr);
        }*/
    }
    [Serializable]
    public struct WordScoreCard
    {
        public string lname{ get; set; }
        public ScoreParameters ScoreInfo { get; set; }
    }
    [Serializable]
    public struct ScoreParameters
    {
        public double Score { get; set; }
        public int Attempts { get; set; }
        public string TimeSpent { get; set; }
        public void UpdateScore(double updateInfo)
        {
            Score = updateInfo;
        }
        public void UpdateAttemts(int updateInfo)
        {
            Attempts = updateInfo;
        }
        public void UpdateTimeSpent(string updateInfo)
        {
            TimeSpent = updateInfo;
        }
    }
}
