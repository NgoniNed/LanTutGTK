using System.Xml;
using LanTutor.DataModels;

namespace LanTutor
{
    /// <summary>
    /// Class used to move around the library data session object
    /// </summary>
    public static class LanTutorXMLMoving
    {
        /// <summary>
        /// Method used to retrieve specified XML nodes given
        /// a specified xml node path from an XMLDocument Object.
        /// </summary>
        /// <param name="SessionDocObj"></param>
        /// <param name="xpathtonodes"></param>
        /// <returns></returns>
        public static XmlNodeList LoadSessionQuestions(XmlDocument SessionDocObj,string xpathtonodes)
        {
            //should be at node WordTransDef
            return SessionDocObj.SelectNodes(xpathtonodes);
        }
        /// <summary>
        /// Method which returns a WordTransDefDict object reference by
        /// its position within a given xmlnodelist
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <param name="currentSessionList"></param>
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
        /// <summary>
        /// Method which returns a WordTransDef object reference by
        /// its position within a given xmlnodelist
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <param name="currentSessionList"></param>
        /// <returns></returns>
        public static WordTransDef GetCurrentQuestionl(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            XmlNode currentQ;
            if (CurrentQuestion == 50)
            {
                //LTGUIDesign.DialogBoxWindow(CurrentQuestion+"\tis greater than\t"+currentSessionList.Count);
                currentQ = currentSessionList[0];
            }
            else if (CurrentQuestion < 0)
            {
                currentQ = currentSessionList[currentSessionList.Count-1];
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
                lDescriptionScore = new DescriptionScore()
                {
                    Attempts = int.Parse(currentQ.SelectSingleNode("DescriptionScore/Attempts").InnerText),
                    Score = double.Parse(currentQ.SelectSingleNode("DescriptionScore/Score").InnerText),
                    TimeSpent = currentQ.SelectSingleNode("DescriptionScore/TimeSpent").InnerText,
                },
                lWordScore = new WordScore()
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
        /// <summary>
        /// Method which updates the current XmlNode data with updateInfo, referenced by th
        /// current position indexer(CurrentQuestion) to the currentSessionList
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <param name="CurrentQuestion"></param>
        /// <param name="currentSessionList"></param>
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
        /// Method which retrieves the next WordTransDefDict reference by
        /// its position
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <param name="currentSessionList"></param>
        /// <returns></returns>
        public static WordTransDefDict GetNextQuestion(int CurrentQuestion,ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTester(ref currentSessionList, (CurrentQuestion+1));
        }
        /// <summary>
        /// Method which retrieves the next WordTransDef reference by
        /// its position
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <param name="currentSessionList"></param>
        /// <returns></returns>
        public static WordTransDef GetNextQuestionl(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTesterl(ref currentSessionList, (CurrentQuestion + 1));
        }
        /// <summary>
        /// Method which retrieves the previous WordTransDefDict reference by
        /// its position
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <param name="currentSessionList"></param>
        /// <returns></returns>
        public static WordTransDefDict GetPreviousQuestion(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTester(ref currentSessionList, (CurrentQuestion-1));
        }
        /// <summary>
        /// Method which retrieves the previous WordTransDef reference by
        /// its position
        /// </summary>
        /// <param name="CurrentQuestion"></param>
        /// <param name="currentSessionList"></param>
        /// <returns></returns>
        public static WordTransDef GetPreviousQuestionl(int CurrentQuestion, ref XmlNodeList currentSessionList)
        {
            return ListBoundaryTesterl(ref currentSessionList, (CurrentQuestion - 1));
        }
        /// <summary>
        /// Method which retrieves WordTransDefDict object from an
        /// XmlNodeList object given that the reference index is not
        /// outside of the nodelist range, otherwise it retrieves the first
        /// or last WordTransDefDict object from the XmlNodeList
        /// </summary>
        /// <param name="currentSessionList"></param>
        /// <param name="tmpii"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method which retrieves WordTransDef object from an
        /// XmlNodeList object given that the reference index is not
        /// outside of the nodelist range, otherwise it retrieves the first
        /// or last WordTransDef object from the XmlNodeList
        /// </summary>
        /// <param name="currentSessionList"></param>
        /// <param name="tmpii"></param>
        /// <returns></returns>
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
