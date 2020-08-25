using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Gtk;

namespace LanTutor
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();*/
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            if((Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards").Length <= 0)|| !(Directory.Exists(Environment.CurrentDirectory + "/ReportCards")))
            {
                LTPhaseOneCore.LanTutEnvironmentSetup();
            }
            else
            {
                Console.WriteLine("Environment Is Ready...\n Preparing session");
            }
            //
            //get the reportcard of current user
            //load question for user
            /*
             * lantutxmlmoving
             *      loadxmlfile
             *      getall the session questions
             *      getcurrentquestion
             */
            System.Xml.XmlNodeList nodeList = LanTutorXMLMoving.LoadSessionQuestions(LanTutorXMLMoving.LoadXMLFile(Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards")[0]), "WordTransDefLibrary/SessionLibrary/WordTransDef");
            Console.WriteLine(nodeList.Count);
            WordTransDef currentQuestion = LanTutorXMLMoving.GetCurrentQuestionl(5,ref nodeList);
            bool userQ = currentQuestion.PrintInfo;
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
            userQ = currentQuestion.PrintInfo;*/
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
            LanTutorXMLMoving.UpdateCurrentNodeList(currentQuestion,5,ref nodeList);
            Console.WriteLine(nodeList.Count);
            currentQuestion = LanTutorXMLMoving.GetCurrentQuestionl(5, ref nodeList);
            userQ = currentQuestion.PrintInfo;
            /*
             * flush usersessionreportcard to file
             *      over write the whole reportcard
             */
            Console.WriteLine(Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards")[0]);
            LTWriteFile.WriteNodeListToXml(Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards")[0], nodeList);
            //LTPhaseOneCore.DataPrep();
            /*
             * load LanTut dictionary in memory
             * prepare LTScoreCard for the current session
             * save session scorecardreport to file
             */
            //string[] lfiles = Directory.GetFiles("/Volumes/Secondary/Projects/PersonalGTK/LanTutor/LanTutor/Dictionary/");
            /*string lusername = "Ngoni1";
            
            string dict = "eng-rus_LanTut_.xml";
            Console.Clear();
            Console.WriteLine("New Session");
            //PrepareUserReportCard(lmainDirectory,lusername);
            /*XmlNodeList mySessionLists = LanTutorXMLMoving.LoadSessionQuestions(LanTutorXMLMoving.LoadXMLFile(lmainDirectory+"/ReportCards/Ngoni_ReportCard.xml"));
            
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


        
        private static void PrepareUserReportCard(string mainDirectory,string username)
        {
            foreach (string lfiles in Directory.GetFiles(mainDirectory + "/Dictionary/"))
            {
                if (Regex.IsMatch(lfiles, "_LanTut_"))
                {
                    object SessionWordsScoreCard = new object();
                    LTSessionScoreCard mysession = LTPhaseOneCore.GenerateSessionScoreCard(lfiles);
                    LTWriteFile.WriteSchemeToxml(mysession, username, mainDirectory + "/ReportCards/");
                    break;
                }

            }
        }

        private static void WriteXmlData()
        {
            ScoreParameters wordScor = new ScoreParameters()
            {
                Attempts = 5,
                Score = 23,
                TimeSpent = new TimeSpan(5, 3, 0).TotalSeconds.ToString()
            };
            ScoreParameters discScor = new ScoreParameters()
            {
                Attempts = 2,
                Score = 20,
                TimeSpent = new TimeSpan(6, 13, 9).TotalSeconds.ToString()
            };
            WordObject wordObject = new WordObject()
            {
                frgnWord = new WordScoreCard()
                {
                    lname = "Makadini",
                    ScoreInfo = wordScor
                },
                localWorddiscrp = new WordScoreCard()
                {
                    lname = "Local morning greeting",
                    ScoreInfo = discScor
                },
                lword = "How are you"
            };
            List<WordObject> wordObjects = new List<WordObject>();
            wordObjects.Add(wordObject);
            wordObjects.Add(wordObject);
            wordObjects.Add(wordObject);
            LTScoreCard mySess = new LTScoreCard()
            {
                SessionLibrary = wordObjects
            };
            LTWriteFile.WriteSchemeToxml(mySess);
        }

    }
}
