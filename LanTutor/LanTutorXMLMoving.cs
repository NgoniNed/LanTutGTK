using System.Xml;

namespace LanTutor
{
    public static class LanTutorXMLMoving
    {
        /// <summary>
        /// this method open an xml file for reading and returns an
        /// xmldocument object reference to the file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static XmlDocument LoadXMLFile(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            
            return doc;
        }
        /// <summary>
        /// returns the number of questions avalable in the current session
        /// </summary>
        /// <param name="SessionDocObj"></param>
        /// <returns></returns>
        /*public static XmlNodeList LoadSessionQuestions(XmlDocument SessionDocObj)
        {
            return SessionDocObj.SelectNodes("LTSessionScoreCard/SessionLibrary/WordTransDefDict");
        }*/
        public static XmlNodeList LoadSessionQuestions(XmlDocument SessionDocObj,string xpathtonodes)
        {
            //should be at node WordTransDef
            return SessionDocObj.SelectNodes(xpathtonodes);
        }
        /// <summary>
        /// returns the current question to be answered by the user
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <returns></returns>
        private static WordTransDefDict GetCurrentQuestion(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            XmlNode currentQ;
            if(CurrentQuestion>= currentSessionList.Count)
            {
                currentQ = currentSessionList[currentSessionList.Count-1];
            }
            else if (CurrentQuestion < 0)
            {
                currentQ = currentSessionList[0];
            }
            else
            {
                currentQ = currentSessionList[CurrentQuestion];
            }
            
            string lengWord = currentQ.SelectSingleNode("Word").InnerText;
            string ltransWord = currentQ.SelectSingleNode("Translation").InnerText;
            System.Collections.Generic.List<string> myDefList = new System.Collections.Generic.List<string>(); 
            foreach(XmlNode node in currentQ.SelectNodes("Definations"))
            {
                myDefList.Add(node.InnerText);
            }
            WordTransDefDict currentQurstion = new WordTransDefDict()
            {
                lword = lengWord,
                lTrans = ltransWord,
                ldef = myDefList,
                
            };
            return currentQurstion;
        }
        public static WordTransDef GetCurrentQuestionl(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            XmlNode currentQ;
            if (CurrentQuestion >= currentSessionList.Count)
            {
                currentQ = currentSessionList[currentSessionList.Count - 1];
            }
            else if (CurrentQuestion < 0)
            {
                currentQ = currentSessionList[0];
            }
            else
            {
                currentQ = currentSessionList[CurrentQuestion];
            }
            WordTransDef currentQurstion = new WordTransDef()
            {
                lword = currentQ.SelectSingleNode("lword").InnerText,
                lTrans = currentQ.SelectSingleNode("lTrans").InnerText,
                ldef = new System.Collections.Generic.List<string>(),
                lDescriptionScore = new ScoreParameters()
                {
                    Attempts = int.Parse(currentQ.SelectSingleNode("DescriptionScore/Attempts").InnerText),
                    Score = double.Parse(currentQ.SelectSingleNode("DescriptionScore/Score").InnerText),
                    TimeSpent = currentQ.SelectSingleNode("DescriptionScore/TimeSpent").InnerText,
                },
                lWordScore = new ScoreParameters()
                {
                    Attempts = int.Parse(currentQ.SelectSingleNode("WordScore/Attempts").InnerText),
                    Score = double.Parse(currentQ.SelectSingleNode("WordScore/Score").InnerText),
                    TimeSpent = currentQ.SelectSingleNode("WordScore/TimeSpent").InnerText,
                },
            };

            foreach (XmlNode node in currentQ.SelectNodes("ldef"))
            {
                currentQurstion.ldef.Add(node.InnerText);
            }
            
            return currentQurstion;
        }

        public static void UpdateCurrentNodeList(WordTransDef updateInfo,int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            foreach(XmlNode oldNode in currentSessionList)
            {
                if(oldNode.Equals(currentSessionList[CurrentQuestion]))
                {
                    System.Console.WriteLine("Found the same node\t\t\t"+oldNode.InnerText);
                    oldNode.SelectSingleNode("lword").InnerText= updateInfo.lword;
                    oldNode.SelectSingleNode("lTrans").InnerText= updateInfo.lTrans;
                    int ii = 0;
                    foreach(XmlNode cDefnode in oldNode.SelectNodes("ldef"))
                    {
                        cDefnode.InnerText = updateInfo.ldef[ii++];
                    }

                    oldNode.SelectSingleNode("WordScore/Attempts").InnerText=updateInfo.lWordScore.Attempts.ToString();
                    oldNode.SelectSingleNode("WordScore/Score").InnerText= updateInfo.lWordScore.Score.ToString();
                    oldNode.SelectSingleNode("WordScore/TimeSpent").InnerText= updateInfo.lWordScore.TimeSpent.ToString();
                    oldNode.SelectSingleNode("DescriptionScore/Attempts").InnerText= updateInfo.lDescriptionScore.Attempts.ToString();
                    oldNode.SelectSingleNode("DescriptionScore/Score").InnerText= updateInfo.lDescriptionScore.Score.ToString();
                    oldNode.SelectSingleNode("DescriptionScore/TimeSpent").InnerText= updateInfo.lDescriptionScore.TimeSpent.ToString();
                    System.Console.WriteLine(oldNode.InnerText);
                }
                
            }
            
        }
        /// <summary>
        /// returns the next question using the current question as reference location
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <returns></returns>
        public static WordTransDefDict GetNextQuestion(int CurrentQuestion,ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTester(ref currentSessionList, (CurrentQuestion+1));
        }
        public static WordTransDef GetNextQuestionl(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTesterl(ref currentSessionList, (CurrentQuestion + 1));
        }
        /// <summary>
        /// returns the previous question using the current question as reference location
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <returns></returns>
        public static WordTransDefDict GetPreviousQuestion(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTester(ref currentSessionList, (CurrentQuestion-1));
        }
        public static WordTransDef GetPreviousQuestionl(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTesterl(ref currentSessionList, (CurrentQuestion - 1));
        }
        private static WordTransDefDict ListBoundaryTester(ref XmlNodeList currentSessionList, int tmpii)
        {
            if (tmpii > currentSessionList.Count)
            {
                tmpii = currentSessionList.Count;
                return GetCurrentQuestion(tmpii, ref currentSessionList);
            }
            else if (tmpii < 0)
            {
                tmpii = 0;
                return GetCurrentQuestion(tmpii, ref currentSessionList);
            }
            else
            {
                return GetCurrentQuestion(tmpii, ref currentSessionList);
            }
        }
        private static WordTransDef ListBoundaryTesterl(ref XmlNodeList currentSessionList, int tmpii)
        {
            if (tmpii > currentSessionList.Count)
            {
                tmpii = currentSessionList.Count;
                return GetCurrentQuestionl(tmpii, ref currentSessionList);
            }
            else if (tmpii < 0)
            {
                tmpii = 0;
                return GetCurrentQuestionl(tmpii, ref currentSessionList);
            }
            else
            {
                return GetCurrentQuestionl(tmpii, ref currentSessionList);
            }
        }
    }
}
