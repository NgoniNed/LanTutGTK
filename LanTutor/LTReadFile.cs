using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System;

namespace LanTutor
{

    /// <summary>
    /// This class is used to read files, folders and load information
    /// from the specified files
    /// </summary>
    public static class LTReadFile
    {
        /// <summary>
        /// method to read user settings file and returns
        /// an obect with the previous sessions settings for the user
        /// </summary>
        /// <returns></returns>
        public static UserSettings getUserSettings()
        {
            XmlDocument document = new XmlDocument();
            document.Load("user_configurations.xml");
            
            UserSettings userSettings = new UserSettings()
            {
                ActiveLanguage = document.GetElementsByTagName("ActiveLanguage")[0].InnerText,
                ActiveSessionMode = document.GetElementsByTagName("ActiveSessionMode")[0].InnerText,
                CurrentQuestion = document.GetElementsByTagName("CurrentQuestion")[0].InnerText
            };
            return userSettings;
        }
        /// <summary>
        /// Gets all the report card xml files from the report cards directory
        /// </summary>
        public static string[] GetReportCards
        {
            get
            {
                if(Directory.Exists(Environment.CurrentDirectory + "/ReportCards")&& Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards").Length>0)
                {
                    return Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards");
                }
                else
                {
                    LTPhaseOneCore.LanTutEnvironmentSetup();

                    return Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards");
                }
            }
        }
        /// <summary>
        /// Property to retrieve the list of translation dictionary options
        /// available from the specified file location.
        /// </summary>
        public static string[] GetListOfTranslationOptions
        {
            get
            {
                string[] filePaths = Directory.GetFiles(Environment.CurrentDirectory + "/LanTutDictionaries");
                string[] fileNames = new string[filePaths.Length];
                for (int ii = 0; ii < filePaths.Length; ii++)
                {
                    string tmp = new FileInfo(filePaths[ii]).Name;
                    tmp = tmp.Substring(0, tmp.IndexOf('.'));
                    fileNames[ii] = (tmp);
                }

                return fileNames;
            }
        }
        /// <summary>
        /// Method to load an XMLDocument object from an
        /// xml file
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
        /// Pulls the file names from the given file path
        /// </summary>
        /// <param name="EnglishTranslationDictsPath"></param>
        /// <returns></returns>
        public static string[] GetTranslationDictionaries(string EnglishTranslationDictsPath)
        {
            return Directory.GetFiles(EnglishTranslationDictsPath);
        }
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

                    if (loadedwords.Equals(wordlimit))
                    {
                        break;
                    }
                    else
                    {
                        loadedwords++;
                    }
                }
            }
            return lSessionlibrary;
        }
        /// <summary>
        /// Method to Load the definations or descriptions from the specified
        /// folder path into a collection list of WordObect
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<WordObject> LoadDefinations(string folderPath)
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
                        }
                        else if(ii.Equals(1))
                        {
                            wordLoaded.lwordDescription= wordinfo.InnerText;
                        }
                        ii++;
                    }
                    //add the wordobject into the session library list
                    wordObjectList.Add(wordLoaded);
                }
                
                Console.WriteLine(lfile+"\t"+ wordDefList.Count);
                
            }
            return wordObjectList;
        }
    }
}