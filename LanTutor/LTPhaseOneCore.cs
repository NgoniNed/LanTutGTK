using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;

namespace LanTutor
{

    /// <summary>
    /// Contains methods used to setup the initial backend files,
    /// folders and relevant file checks for the applications initial
    /// startup.
    /// </summary>
    public class LTPhaseOneCore : LTGUIDesign
    {
        internal static XmlNodeList ExecuteProgramBackend(string ReportCardPath)
        {
            if ((AvailableReportCards.Length < 1)&&(!(Directory.Exists(Environment.CurrentDirectory + "/ReportCards"))) )
            {


                LTPhaseOneCore.LanTutEnvironmentSetup();
                
            }
            else
            {
                
            }

            foreach (string reports in AvailableReportCards)
            {
                try
                {
                    if (reports.Contains(LTGUIDesign.UIElement.LanguageComboOptions.ActiveText))
                    {

                        return LanTutorXMLMoving.LoadSessionQuestions(LTReadFile.LoadXMLFile(reports), "WordTransDefLibrary/SessionLibrary/WordTransDef");

                    }
                }
                catch (NullReferenceException NRE)
                {
                    //LTGUIDesign.DialogBoxWindow(NRE.Message);
                    break;
                }
                
                
            }
            //update active iter for combolanguage box to be that of the one with the available report card
            //LanguageComboOptions.mo
            //look for the index of the reportcard referenced by the user interface
            return LanTutorXMLMoving.LoadSessionQuestions(LTReadFile.LoadXMLFile(AvailableReportCards[1]), "WordTransDefLibrary/SessionLibrary/WordTransDef");

        }


        internal static void LanTutEnvironmentSetup()
        {
            //get the main working directory
            string lmainDirectory = Environment.CurrentDirectory;
            //create folder for holding english .xml processed dictionaries
            Directory.CreateDirectory(lmainDirectory + "/EnglishDictionaries");
            //create the lantut english translation dictionaries folder
            Directory.CreateDirectory(lmainDirectory + "/LanTutDictionaries");
            /*
             * move tei translation dictionaries to lantutdictionaries
             */
            foreach (string fileName in LTReadFile.GetTranslationDictionaries(lmainDirectory + "/LanTutDictionaries"))
            {
                WordTransDefLibrary llibrary = DataPrep(fileName, LTReadFile.LoadDefinations(lmainDirectory + "/EnglishDictionaries"));
                FileInfo fI = new FileInfo(fileName);//LTReadFile.GetTranslationDictionaries(lmainDirectory + "/LanTutDictionaries")[0]);//LTReadFile.GetTranslationDictionaries(lmainDirectory + "/LanTutDictionaries")[0]
                                                                                                                             //create the report cards folder
                Directory.CreateDirectory(lmainDirectory + "/ReportCards");
                LTWriteFile.WriteSchemeToxml(llibrary, "/ngoni_" + fI.Name.Replace(".tei", "") + "_ReportCard.xml", lmainDirectory + "/ReportCards");
            }
            
        }
        internal static string[] GenerateLangaugeOptions
        {
            get
            {
                return Directory.GetFiles(Environment.CurrentDirectory + "/LanTutDictionaries");
            }
        }
        /// <summary>
        /// Populates information regarding the users score into the
        /// referenced file
        /// </summary>
        /// <param name="lfiles"></param>
        /// <returns></returns>
        public static LTSessionScoreCard GenerateSessionScoreCard(string lfiles)
        {
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.Load(lfiles);
            System.Xml.XmlNodeList lSessionLibrary = document.LastChild.FirstChild.ChildNodes;
            //session has started
            LTSessionScoreCard mysession = new LTSessionScoreCard();
            List<WordTransDefDict> sessionObjList = new List<WordTransDefDict>();
            foreach (System.Xml.XmlNode lWordTransDef in document.LastChild.FirstChild.ChildNodes)
            {
                List<string> defList = new List<string>();
                WordTransDefDict dictObject = new WordTransDefDict();
                int ii = 0;
                foreach (System.Xml.XmlNode dataGroup in lWordTransDef.ChildNodes)
                {
                    if (ii == 0)
                    {
                        dictObject.lword = dataGroup.InnerText;
                    }
                    if (ii == 1)
                    {
                        dictObject.lTrans = dataGroup.InnerText;
                    }
                    if (ii == 2)
                    {
                        foreach (System.Xml.XmlNode node in dataGroup.ChildNodes)
                        {
                            defList.Add(node.InnerText);
                        }
                    }
                    ii++;
                }

                dictObject.ldef = defList;
                dictObject.lDescriptionScore = DefaultScores();
                dictObject.lWordScore = DefaultScores();
                sessionObjList.Add(dictObject);
            }
            mysession.SessionLibrary = sessionObjList;
            return mysession;
        }
        private static ScoreParameters DefaultScores()
        {
            ScoreParameters sp = new ScoreParameters()
            {
                Attempts = 0,
                Score = 0,
                TimeSpent = "0"
            };
            return sp;
        }
        /// <summary>
        /// Method to prepare the the data for the user as a WordTransDefLibrary
        /// object.
        /// </summary>
        /// <param name="lfile"></param>
        /// <param name="myset"></param>
        /// <returns></returns>
        public static WordTransDefLibrary DataPrep(string lfile, List<WordObject> myset)
        {
            //load the definations of the files
            Console.WriteLine("Definations =>"+ myset.Count);

            System.Console.WriteLine(lfile + "\n Name " + LTReadFile.GetDictionaryFileInfo(lfile).Name + "\n\t Size " + LTReadFile.GetDictionaryFileInfo(lfile).Length);
            WordTransDefLibrary lwordTransDefLibrary = PrepareSessionLibrary(myset, lfile);

            System.Console.WriteLine("Pack Counter: \t" + lwordTransDefLibrary.SessionLibrary.Count);

            return lwordTransDefLibrary;

        }

        private static WordTransDefLibrary PrepareSessionLibrary(List<WordObject> myset, string lfile)
        {
            //load the translations and words from file
            WordTransDefLibrary lwordTransDefLibrary = LTReadFile.LoadDictionary(lfile, 0, 50);
            Console.WriteLine(lwordTransDefLibrary.SessionLibrary.Count);
            //link the definations to the translations-words
            for (int ii = 0; ii < lwordTransDefLibrary.SessionLibrary.Count; ii++)
            {
                foreach (WordObject Dictword in myset)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(Dictword.lword, "\\s" + lwordTransDefLibrary.SessionLibrary[ii].lword.Substring(0, (lwordTransDefLibrary.SessionLibrary[ii].lword.IndexOf(","))) + "\\s", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {

                        lwordTransDefLibrary.SessionLibrary[ii].Updatelword(Dictword.lword);
                        lwordTransDefLibrary.SessionLibrary[ii].Updateldef(Dictword.lwordDescription);
                    }
                }
            }

            return lwordTransDefLibrary;
        }     
    }
}