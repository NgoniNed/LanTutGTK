using System.Collections.Generic;
using System;
using System.IO;

namespace LanTutor
{
    public static class LTPhaseOneCore
    {
        /*
        public object frgnWord
        {
            get;
            private set;
        }
        public object frgnWordDescription
        {
            get;
            private set;
        }
        public object mthrLangWord
        {
            get;
            private set;
        }
        public object frgnWordScore
        {
            get;
            private set;
        }
        public object frgnWordDescriptionScore
        {
            get;
            private set;
        }
        public object practiceTimelapsed
        {
            get;
            private set;
        }
        
        public LTPhaseOneCore()
        {
        }*/
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
            /*foreach(WordTransDef wordObj in llibrary.SessionLibrary)
            {
                bool wd = wordObj.PrintInfo;
            }*/
            //break;
            /*write the pack to file.xml
             * 
            string fname = LTReadFile.GetDictionaryFileInfo(lfile).Name;
            fname = fname.Substring(0, fname.IndexOf('.'));
            */
            //create the report cards folder
            Directory.CreateDirectory(lmainDirectory + "/ReportCards");
            LTWriteFile.WriteSchemeToxml(llibrary, "/ngoni" + "_ReportCard.xml", lmainDirectory + "/ReportCards");

            //Console.WriteLine();

        }

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
                //XmlNodeList dataGroup = lWordTransDef.ChildNodes;
                List<string> defList = new List<string>();
                //Console.WriteLine("========");
                WordTransDefDict dictObject = new WordTransDefDict();
                int ii = 0;
                foreach (System.Xml.XmlNode dataGroup in lWordTransDef.ChildNodes)
                {

                    //if childnodes exist add text infor to string list
                    //else retrieve the text node
                    if (ii == 0)
                    {
                        //Console.WriteLine(dataGroup.InnerText);
                        dictObject.lword = dataGroup.InnerText;

                    }
                    if (ii == 1)
                    {
                        //Console.WriteLine(dataGroup.InnerText);
                        dictObject.lTrans = dataGroup.InnerText;
                    }
                    if (ii == 2)
                    {
                        //Console.WriteLine(dataGroup.HasChildNodes);
                        //Console.WriteLine(dataGroup.ChildNodes.Count);
                        foreach (System.Xml.XmlNode node in dataGroup.ChildNodes)
                        {
                            defList.Add(node.InnerText);
                            //Console.WriteLine(node.InnerText);
                        }
                        //dictObject.lword = dataGroup.InnerText;
                    }
                    ii++;

                }

                dictObject.ldef = defList;
                dictObject.lDescriptionScore = DefaultScores();
                dictObject.lWordScore = DefaultScores();
                //place in session library
                sessionObjList.Add(dictObject);
                //Console.WriteLine("========");
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
        /// requires folder path of the xml wordnet processed dictionary files
        /// _adj.xml, _verbs.xml, _noun.xml, _adv.xml
        /// the folder path should be provided in <paramref name="llantutmaindirectory"/>
        /// </summary>
        /// <param name="llantutmaindirectory"></param>
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
            //sessionwordset type to should be replaced by wordtransdict datatype
            //SessionWordSet sws = 

            //load the translations and words from file
            WordTransDefLibrary lwordTransDefLibrary = LTReadFile.LoadDictionary(lfile, 0, 50);
            Console.WriteLine(lwordTransDefLibrary.SessionLibrary.Count);
            //wordTransDefLibrary.SessionLibrary = new List<WordTransDef>();

            //link the definations to the translations-words
            //for (int ii = 0; ii < sws.foreignTongue.Count; ii++)


            for (int ii = 0; ii < lwordTransDefLibrary.SessionLibrary.Count; ii++)
            {

                //WordTransDef wordDataPack = new WordTransDef();
                //Console.WriteLine(lwordTransDefLibrary.SessionLibrary[ii].lword.Substring(0, (lwordTransDefLibrary.SessionLibrary[ii].lword.IndexOf(","))));

                //string tmp = sws.motherTongue[ii].Substring(0, (sws.motherTongue[ii].IndexOf(",")));
                //List<string> currentWordDeffs = new List<string>();
                //string currentWord = string.Empty;
                
                foreach (WordObject Dictword in myset)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(Dictword.lword, "\\s" + lwordTransDefLibrary.SessionLibrary[ii].lword.Substring(0, (lwordTransDefLibrary.SessionLibrary[ii].lword.IndexOf(","))) + "\\s", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {

                        lwordTransDefLibrary.SessionLibrary[ii].Updatelword(Dictword.lword);
                        lwordTransDefLibrary.SessionLibrary[ii].Updateldef(Dictword.lwordDescription);
                        //Console.WriteLine("Match Found ::/t"+Dictword.lword+"\t"+ lwordTransDefLibrary.SessionLibrary[ii].lword.Substring(0, (lwordTransDefLibrary.SessionLibrary[ii].lword.IndexOf(","))));
                        //currentWordDeffs.Add(Dictword.lwordDescription);
                    }

                }
                /*
                wordDataPack.ldef = currentWordDeffs;
                wordDataPack.lTrans = sws.foreignTongue[ii];
                wordDataPack.lword = sws.motherTongue[ii] + "|" + currentWord;
                wordTransDefLibrary.SessionLibrary.Add(wordDataPack);
                //bool test = wordDataPack.PrintInfo;
                */

            }

            return lwordTransDefLibrary;
        }

        // <summary>
        /// Compiles the user's current word and description scores into a single
        /// data type that represents the users score report.
        /// </summary>
        /// <returns></returns>
        public static object CompileScoreCardReport(object lCurrentword, object lCurrentDescription, object lfirstlanguageWord)
        {
            object lScoreCardReport = new object();
            return lScoreCardReport;
        }
        /// <summary>
        /// Grades each description that the user has practiced, given the current description data object
        /// it returns an equavalent object with the users score.
        /// </summary>
        /// <param name="lCurrentWord"></param>
        /// <returns></returns>
        public static object GradeDescription(object lCurrentDescription)
        {
            return lCurrentDescription;
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
        /// <summary>
        /// Loads data from the source file using the method from the static class
        /// LTReadFile. The Return type of the loaded data is based on the custom
        /// data type LTWordData.
        /// </summary>
        /// <param name="lSourceFileName"></param>
        /// <returns></returns>
        public static List<object> LoadCurrentPhaseWords(string lSourceFileName)
        {
            return LTReadFile.ReadFile(lSourceFileName);
        }
        
        /// <summary>
        /// Writes the Score card to file given the file name and scorecard report
        /// </summary>
        /// <param name="lScoreCardName"></param>
        public static void WriteScoreCardTo(string lScoreCardName,object lScoreCardReport)
        {
            //LTWriteFile.WriteScoreCard(lScoreCardName,lScoreCardReport);
        }
        /// <summary>
        /// Loads the users session report card given the user report card file path
        /// </summary>
        /// <param name="lUserReportCardPath"></param>
        /// <returns></returns>
        internal static WordTransDefLibrary LanTutSessionLoad(string lUserReportCardPath)
        {
            throw new NotImplementedException();
        }
    }
}
