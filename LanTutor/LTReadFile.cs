using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System;

namespace LanTutor
{
    public static class LTReadFile
    {
        //const string dictfile = "/Volumes/Secondary/Projects/PersonalGTK/LanTutor/LanTutor/EnglishDictionaries";

        public static List<object> ReadFile(string lsourcefilename)
        {
            List<object> loadedData = new List<object>();
            return loadedData;
        }
        /// <summary>
        /// Pulls the file names from the given file path
        /// </summary>
        /// <param name="EnglishTranslationDictsPath"></param>
        /// <returns></returns>
        public static string[] GetTranslationDictionaries(string EnglishTranslationDictsPath)
        {
            return Directory.GetFiles(EnglishTranslationDictsPath);
        }
        /*public static bool FindDictionariesFolder
        {
            get
            {
                return Directory.Exists(Environment.CurrentDirectory+"/");
            }
             
        }
        public static int NumberOfAvailableDictionaries
        {
            get
            {
                return Directory.GetFiles(dictfile).Length;
            }
            
        }*/
        /// <summary>
        /// pulls the file information from the specified file path
        /// </summary>
        /// <param name="dictfilePath"></param>
        /// <returns></returns>
        public static FileInfo GetDictionaryFileInfo(string dictfilePath)
        {

            return new FileInfo(dictfilePath);
            
        }
        /// <summary>
        /// pulls the english and translation per given node entry in the dictionary file, of
        /// within a given dictionary file path <paramref name="dictfilePath"/> by
        /// reading its contents using xml document, the number of xmlNodes retrieved are fixed by
        /// <paramref name="wordsInitial"/> and <paramref name="wordlimit"/>.
        /// </summary>
        /// <param name="dictfilePath"></param>
        /// <param name="wordsInitial"></param>
        /// <param name="wordlimit"></param>
        /// <returns></returns>
        public static WordTransDefLibrary LoadDictionary(string dictfilePath,int wordsInitial,int wordlimit)
        {
            WordTransDefLibrary lSessionlibrary = new WordTransDefLibrary()
            {
                SessionLibrary = new List<WordTransDef>(),
            };
            //List<WordTransDef> wordTransObject = new List<WordTransDef>();
            //List<string> wordFormList = new List<string>();
            //List<string> wordSenseList = new List<string>();
            //FileStream lfileStream = (dictfilePath);
            //lfileStream.GetAccessControl();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(dictfilePath);
            int loadedwords = 0;
            XmlNodeList wordnodes = xmlDocument.ChildNodes[1].ChildNodes[1].FirstChild.ChildNodes;
            //update the foreach loop to use direct retrieve on each node rather than looping through
            //nodes
            foreach(XmlNode wordEntry in wordnodes)
            {
                if(loadedwords<=(wordsInitial))
                {
                    loadedwords++;
                }
                else
                {
                    //System.Console.WriteLine(wordEntry.ChildNodes.Count);
                    XmlNode formNode = wordEntry.FirstChild;
                    XmlNode senseNode = wordEntry.LastChild;
                    StringBuilder formBuilder = new StringBuilder();
                    StringBuilder senseBuilder = new StringBuilder();
                    foreach (XmlNode eachForm in formNode.ChildNodes)
                    {
                        formBuilder.Append(eachForm.InnerText+", ");
                    }
                    foreach (XmlNode eachSense in senseNode.ChildNodes)
                    {
                        senseBuilder.Append(eachSense.InnerText+", ");
                    }

                    lSessionlibrary.SessionLibrary.Add(new WordTransDef()
                    {
                        lTrans = senseBuilder.ToString(),
                        lword = formBuilder.ToString(),
                        ldef = new List<string>(),
                        lWordScore = new ScoreParameters()
                        {
                            Attempts = 0,
                            Score = 0,
                            TimeSpent = "0",
                        },
                        lDescriptionScore = new ScoreParameters()
                        {
                            Attempts = 0,
                            Score = 0,
                            TimeSpent = "0",
                        },
                    });

                    //bool printdone = lSessionlibrary.SessionLibrary[lSessionlibrary.SessionLibrary.Count -1].PrintInfo;
                    //wordFormList.Add(formBuilder.ToString());
                    //wordSenseList.Add(senseBuilder.ToString());
                    if (loadedwords.Equals(wordlimit))
                    {
                        //System.Console.WriteLine("Word Forms\t" + wordFormList.Count+"\t"+wordFormList[1]);
                        //System.Console.WriteLine("Word Sense\t" + wordSenseList.Count + "\t" + wordSenseList[1]);
                        break;
                    }
                    else
                    {
                        loadedwords++;
                    }
                }
            }
            //update the return variable so at to use wordtransdef data type
            /*SessionWordSet wordSet = new SessionWordSet()
            {
                motherTongue = wordFormList,
                foreignTongue = wordSenseList
            };
            */
            
            return lSessionlibrary;
        }

        internal static List<WordObject> LoadDefinations(string folderPath)
        {
            //SessionWordSet lset = new SessionWordSet();
            string[] files = Directory.GetFiles(folderPath);
            List<WordObject> wordObjectList = new List<WordObject>();
            foreach (string lfile in files)
            {
                //open each file and retrieve the wordData node
                XmlDocument document = new XmlDocument();
                document.Load(lfile);
                XmlNodeList wordDefList = document.LastChild.FirstChild.ChildNodes;
                //partially fill the wordObject struct
                
                foreach(XmlNode wordData in wordDefList)
                {
                    int ii = 0;
                    //create new wordObject struct for the wordinfo
                    WordObject wordLoaded = new WordObject();
                    foreach(XmlNode wordinfo in wordData.ChildNodes)
                    {
                        if(ii.Equals(0))
                        {
                            wordLoaded.lword = wordinfo.InnerText;
                            //Console.Write(wordinfo.InnerText + "\t=>\t");
                        }
                        else if(ii.Equals(1))
                        {
                            wordLoaded.lwordDescription= wordinfo.InnerText;
                            //Console.Write(wordinfo.InnerText);
                        }
                        ii++;
                    }
                    //add the wordobject into the session library list
                    wordObjectList.Add(wordLoaded);
                    //Console.WriteLine();
                }
                //each wordObject represents each wordData node
                //therefor number of wordObjects created should be equal to wordData nodes
                //Console.WriteLine(wordDefList.Count+"\t==>>\t"+wordObjectList.Count);
                Console.WriteLine(lfile+"\t"+ wordDefList.Count);
                
            }
            return wordObjectList;
        }
    }
}
