using System.Xml.Serialization;
using System.IO;
using System.Xml;
using LanTutor.DataModels;

namespace LanTutor
{
    /// <summary>
    /// This class is used to write data to xml files. Data maybe written to files
    /// using xml serializations based on the data ojbects serialization markup
    /// structure
    /// </summary>
    public static class LTWriteFile
    {
        /// <summary>
        /// Method to create an xml file based on data type of object passed to it
        /// file is saved in the UserConfigurations file location
        /// </summary>
        /// <param name="dataObject"></param>
        public static void WriteGenericSchemeToXml(object dataObject)
        {
            XmlSerializer genericSerializer = new XmlSerializer(dataObject.GetType(), "");
            using (MemoryStream genericStreamer = new MemoryStream())
            {
                genericSerializer.Serialize(genericStreamer, dataObject);
                File.Delete("user_configurations.xml");
                using (FileStream fs = new FileStream("user_configurations.xml", FileMode.CreateNew))
                {
                    genericStreamer.WriteTo(fs);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// Method Used to Write LTSessionScoreCard structure define data
        /// to and xml file using data serialization as the xml files markup
        /// structure.
        /// </summary>
        /// <param name="mySess"></param>
        /// <param name="username"></param>
        /// <param name="drffilepath"></param>
        public static void WriteSchemeToxml(LTSessionScoreCard mySess,string username,string drffilepath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(mySess.GetType(), "");
            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, mySess);
                using (FileStream fs = new FileStream(drffilepath+username + "_"+"ReportCard.xml", FileMode.Create))
                {
                    stream.WriteTo(fs);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// Method Used to Write LTScoreCard structure define data
        /// to userreportcard.xml file using data serialization as the xml files markup
        /// structure.
        /// </summary>
        /// <param name="mySess"></param>
        public static void WriteSchemeToxml(LTScoreCard mySess)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(mySess.GetType(), "");
            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, mySess);
                using (FileStream fs = new FileStream("UserReportCard.xml", FileMode.Create))
                {
                    stream.WriteTo(fs);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// Method Used to Write LTScoreCard structure define data
        /// to MegaDictionary.xml file using data serialization as the xml files markup
        /// structure.
        /// </summary>
        /// <param name="mySess"></param>
        /// <param name="filePath"></param>
        public static void WriteSchemeToxml(LTScoreCard mySess,string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(mySess.GetType(), "");
            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, mySess);
                using (FileStream fs = new FileStream(filePath+"MegaDictionary.xml", FileMode.Create))
                {
                    stream.WriteTo(fs);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// Method Used to Write WordTransDefLibrary structure define data
        /// to an xml file using data serialization as the xml files markup
        /// structure.
        /// </summary>
        /// <param name="mySess"></param>
        /// <param name="lfilename"></param>
        /// <param name="filePath"></param>
        public static void WriteSchemeToxml(WordTransDefLibrary mySess,string lfilename, string filePath)
        {
            
            XmlSerializer xmlSerializer = new XmlSerializer(mySess.GetType(), "");
            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, mySess);
                using (FileStream fs = new FileStream(filePath + lfilename, FileMode.Create))
                {
                    stream.WriteTo(fs);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// Method Used to Write XmlNodeList data to an xml file using WordTransDefLibrary
        /// data serialization as the xml files markup structure.
        /// </summary>
        /// <param name="lffilename"></param>
        /// <param name="nodeList"></param>
        private static void WriteNodeListToXml(string lffilename, XmlNodeList nodeList)
        {
            WordTransDefLibrary library = new WordTransDefLibrary()
            {
                SessionLibrary = new System.Collections.Generic.List<WordTransDef>()
            };

            for(int ii=0;ii<nodeList.Count;ii++)
            {
                library.SessionLibrary.Add( LanTutorXMLMoving.GetCurrentQuestionl(ii,ref nodeList));
            }
            XmlSerializer xmlSerializer = new XmlSerializer(library.GetType(), "");
            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, library);
                using (FileStream fs = new FileStream(lffilename, FileMode.Create))
                {
                    stream.WriteTo(fs);
                    fs.Flush();
                }
            }
        }
    }
}
