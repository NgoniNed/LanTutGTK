using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace LanTutor.DataModels
{
    /// <summary>
    /// Object which holds word, translation and definition
    /// </summary>
    [Serializable]
    public class WordTransDef
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
        /// List of the word's definition/description
        /// </summary>
        [NotMapped]
        public List<string> ldef
        {
            get;
            set;
        }

        public ICollection<Definition> Definitions
        {
            get;
            set;
        }

        /// <summary>
        /// Updates the definitions/description variable
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
        /// Word Score property
        /// </summary>
        [XmlElement(ElementName = "WordScore")]
        [NotMapped]
        public WordScore lWordScore
        {
            get;
            set;
        }

        /// <summary>
        /// Description/Definitions Score Property
        /// </summary>
        [XmlElement(ElementName = "DescriptionScore")]
        [NotMapped]
        public DescriptionScore lDescriptionScore
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
                Console.WriteLine("\nWord Scores\n\nScore: " + lWordScore.Score + "\t\tAttempts: " + lWordScore.Attempts + "\t\tTime: " + lWordScore.TimeSpent);
                Console.WriteLine("\nDescription Scores\n\nScore: " + lDescriptionScore.Score + "\t\tAttempts: " + lDescriptionScore.Attempts + "\t\tTime: " + lDescriptionScore.TimeSpent);

                Console.WriteLine();
                return true;
            }

        }

        public int Id
        {
            get;
            set;
        }
    }
}