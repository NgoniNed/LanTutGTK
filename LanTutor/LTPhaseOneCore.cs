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
    public static class LTPhaseOneCore
    {
        internal static XmlNodeList ExecuteProgramBackend()
        {
            if (!(Directory.Exists(Environment.CurrentDirectory + "/ReportCards")) || (Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards").Length <= 0))
            {
                LTPhaseOneCore.LanTutEnvironmentSetup();
            }
            else
            {
                Gtk.Window dWindow = new Gtk.Window("Dialog");
                Gtk.MessageDialog dialog = new Gtk.MessageDialog(dWindow, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, "Envirnment Is Ready");
                dialog.SetSizeRequest(400, 200);
                dialog.WindowPosition = Gtk.WindowPosition.Center;
                dialog.Run();
                dialog.Destroy();

                Console.WriteLine("Environment Is Ready...\n Preparing session");
            }
            
            //XmlNodeList nodeList =
            return  LanTutorXMLMoving.LoadSessionQuestions(LTReadFile.LoadXMLFile(Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards")[0]), "WordTransDefLibrary/SessionLibrary/WordTransDef");
            //Console.WriteLine(nodeList.Count);
            //int qNum = 5;
            //GetQuestion(nodeList, 1);

            //bool userQ = currentQuestion.PrintInfo;
            /*/update score parameters
            currentQuestion.lWordScore = new ScoreParameters()
            {
                Attempts = 5,
                Score = 2,
                TimeSpent = "6"
            };
            currentQuestion.lDescriptionScore = new ScoreParameters()
            {
                Attempts = 3,
                Score = 2,
                TimeSpent = "10"
            };
            userQ = currentQuestion.PrintInfo;
            //get previous question
            currentQuestion = LanTutorXMLMoving.GetPreviousQuestionl(5, ref nodeList);
            userQ = currentQuestion.PrintInfo;
            //get next question
            currentQuestion = LanTutorXMLMoving.GetNextQuestionl(5, ref nodeList);
            userQ = currentQuestion.PrintInfo;
            //done learning
            currentQuestion = LanTutorXMLMoving.GetCurrentQuestionl(5, ref nodeList);
            //update score parameters
            currentQuestion.lWordScore = new ScoreParameters()
            {
                Attempts = 5,
                Score = 2,
                TimeSpent = "6"
            };
            currentQuestion.lDescriptionScore = new ScoreParameters()
            {
                Attempts = 3,
                Score = 2,
                TimeSpent = "10"
            };
            userQ = currentQuestion.PrintInfo;
            LanTutorXMLMoving.UpdateCurrentNodeList(currentQuestion, 5, ref nodeList);
            Console.WriteLine(nodeList.Count);
            currentQuestion = LanTutorXMLMoving.GetCurrentQuestionl(5, ref nodeList);
            userQ = currentQuestion.PrintInfo;
            /*
             * flush usersessionreportcard to file
             *      over write the whole reportcard
             */
            //Console.WriteLine(Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards")[0]);
            //LTWriteFile.WriteNodeListToXml(Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards")[0], nodeList);
            //LTPhaseOneCore.DataPrep();
            /*
             * load LanTut dictionary in memory
             * prepare LTScoreCard for the current session
             * save session scorecardreport to file
             */
            /*/string[] lfiles = Directory.GetFiles("/Volumes/Secondary/Projects/PersonalGTK/LanTutor/LanTutor/Dictionary/");
            string lusername = "Ngoni1";

            string dict = "eng-rus_LanTut_.xml";
            Console.Clear();
            Console.WriteLine("New Session");
            //PrepareUserReportCard(lmainDirectory,lusername);
            XmlNodeList mySessionLists = LanTutorXMLMoving.LoadSessionQuestions(LanTutorXMLMoving.LoadXMLFile(lmainDirectory+"/ReportCards/Ngoni_ReportCard.xml"));
            
            Console.WriteLine(mySessionLists.Count);
            int ii = 1;
            WordTransDefDict qtouser = LanTutorXMLMoving.GetCurrentQuestion(ref ii,ref mySessionLists);
            bool tmp = qtouser.PrintInfo;

            WordTransDefDict nxtqtouser = LanTutorXMLMoving.GetNextQuestion(ref ii, ref mySessionLists);
            tmp = nxtqtouser.PrintInfo;

            WordTransDefDict prvqtouser = LanTutorXMLMoving.GetPreviousQuestion(ref ii, ref mySessionLists);
            tmp = prvqtouser.PrintInfo;
            
            LTScoreCard lT = new LTScoreCard();
            lT.SessionLibrary = myset;
            LTWriteFile.WriteSchemeToxml(lT, "/Volumes/Secondary/Projects/PersonalGTK/LanTutor/LanTutor/Dictionary");
            WriteXmlData();
            */
        }

        internal static void GetQuestion(ref XmlNodeList nodeList, int qNum)
        {
            WordTransDef currentQuestion = LanTutorXMLMoving.GetCurrentQuestionl(qNum, ref nodeList);

            LTGUIDesign.UpdateMotherTongueView(currentQuestion.lword);
            LTGUIDesign.UpdateTranslationView(currentQuestion.lTrans);
            LTGUIDesign.UpdateDescriptionView(currentQuestion.ldef);
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
            WordTransDefLibrary llibrary = DataPrep(LTReadFile.GetTranslationDictionaries(lmainDirectory + "/LanTutDictionaries")[0], LTReadFile.LoadDefinations(lmainDirectory+ "/EnglishDictionaries"));
            
            //create the report cards folder
            Directory.CreateDirectory(lmainDirectory + "/ReportCards");
            LTWriteFile.WriteSchemeToxml(llibrary, "/ngoni" + "_ReportCard.xml", lmainDirectory + "/ReportCards");
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
        private static WordTransDefLibrary DataPrep(string lfile, List<WordObject> myset)
        {
            //load the definations of the files
            Console.WriteLine("Definations =>"+ myset.Count);
            
            //bool dictsfound = LTReadFile.FindDictionariesFolder;
            //System.Console.WriteLine("File Found :" + LTReadFile.NumberOfAvailableDictionaries);
            
            //load each file from folder
            
            //foreach (string lfile in LTReadFile.GetTranslationDictionaries(llantutmaindirectory))
            //{
                System.Console.WriteLine(lfile + "\n Name " + LTReadFile.GetDictionaryFileInfo(lfile).Name + "\n\t Size " + LTReadFile.GetDictionaryFileInfo(lfile).Length);
                WordTransDefLibrary lwordTransDefLibrary = PrepareSessionLibrary(myset, lfile);

                System.Console.WriteLine("Pack Counter: \t" + lwordTransDefLibrary.SessionLibrary.Count);
            //break;
            /*write the pack to file.xml
            string fname = LTReadFile.GetDictionaryFileInfo(lfile).Name;
            fname = fname.Substring(0, fname.IndexOf('.'));
            LTWriteFile.WriteSchemeToxml(wordTransDefLibrary, fname + "_LanTut_.xml", "/Volumes/Secondary/Projects/PersonalGTK/LanTutor/LanTutor/Dictionary/");
            */


            //}
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

        /// <summary>
        /// Grades each word that the user has practiced, given the current word data object
        /// it returns an equavalent object with the users score.
        /// </summary>
        /// <param name="lCurrentWord"></param>
        /// <returns></returns>
        public static object GradeWord(object lCurrentWord)
        {
            return lCurrentWord;
        }       
    }
}