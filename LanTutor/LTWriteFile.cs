using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System;

namespace LanTutor
{
    public static class LTWriteFile
    {
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

        internal static void WriteNodeListToXml(string v, XmlNodeList nodeList)
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
                using (FileStream fs = new FileStream(v, FileMode.Create))
                {
                    stream.WriteTo(fs);
                    fs.Flush();
                }
            }
        }
    }
}
