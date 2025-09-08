using System;
using System.Xml;
using Gtk;
using LanTutor.Adapters;
using LanTutor.DataModels;
using LanTutor.Interfaces;

namespace LanTutor
{
    public class LTGUIDesign
    {
        private static string FontStyle = "Noteworthy 20";
        private static string discrFontStyle = "Noteworthy 18";
        public Window Mainwindow;
        
        protected static string[] AvailableReportCards
        {
            get
            {
                return LTReadFile.GetReportCards;
            }
            set
            {
                AvailableReportCards = value;
            }
        }

        protected static string ActiveReportCard
        {
            get;
            set;
        }
        public static LTGUIElements UIElement
        {
            get;
            set;
        }

        private readonly ILanTutorFrontend adapter;

        //Dependency Injection constructor
        public LTGUIDesign(ILanTutorFrontend adapter, string appName = "Default Window")
        {
            this.adapter = adapter;
            Mainwindow = new Window(appName);
            UIElement = new LTGUIElements(appName, this, adapter);

            UIElement.TranslationView.Editable = false;
            UIElement.MotherTongueView.Editable = false;

            LoadInitialQuestion();
            Mainwindow.ShowAll();
        }

        public void LoadInitialQuestion()
        {
            //check combo box value
            //DialogBoxWindow("Load Initial Question\t\n"+UIElement.LanguageComboOptions.ActiveText);

            //UIElement.sessionDataList = LTPhaseOneCore.ExecuteProgramBackend(UIElement.LanguageComboOptions.ActiveText);

            //ILanTutorFrontend adapter = new AutoAdapter();
            UIElement.sessionDataList = adapter.LoadSession(UIElement.LanguageComboOptions.ActiveText);

            //reload sessiondatalist based on combo box value
            //get the current question
            //XmlNodeList tmpNodeList = UIElement.sessionDataList;

            //ILanTutorFrontend adapter = new AutoAdapter();
            WordTransDef tmp = adapter.GetQuestion(UIElement.QuestionIterator);

            //WordTransDef tmp = LanTutorXMLMoving.GetCurrentQuestionl(UIElement.QuestionIterator, ref tmpNodeList);

            if (tmp.lWordScore == null) tmp.lWordScore = new WordScore();
            if (tmp.lDescriptionScore == null) tmp.lDescriptionScore = new DescriptionScore();

            LTGUIElements.UpdateScoresComboBox(tmp.lDescriptionScore,tmp.lWordScore);
            
            UpdateMotherTongueView(tmp.lword);
            UpdateTranslationView(tmp.lTrans);
            UpdateDescriptionView(tmp.ldef);
            
        }
        
        public static void DialogBoxWindow(string msg)
        {
            Gtk.Window dWindow = new Gtk.Window("Dialog");
            Gtk.MessageDialog dialog = new Gtk.MessageDialog(dWindow, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, msg);
            dialog.SetSizeRequest(400, 200);
            dialog.WindowPosition = Gtk.WindowPosition.Center;
            dialog.Run();
            dialog.Destroy();
        }

        public void UpdateMotherTongueView(string TVMotherTongue)
        {
            UIElement.MotherTongueView.Buffer.Text = TVMotherTongue;
            UIElement.MotherTongueView.Editable = false;
            UIElement.MotherTongueView.ModifyFont(Pango.FontDescription.FromString(FontStyle));
        }

        public void UpdateTranslationView(string TVTranslation)
        {
            //DialogBoxWindow(UIElement.testingMode.ActiveText);
            System.Text.StringBuilder encrypted = new System.Text.StringBuilder();
            if(UIElement.testingMode.ActiveText.Equals("Word Test"))
            {
                foreach(char tmpCh in TVTranslation)
                {
                    encrypted.Append('#');
                }
                UIElement.TranslationView.Buffer.Text = encrypted.ToString();
                UIElement.TranslationView.Editable = true;
            }
            else
            {
                UIElement.TranslationView.Buffer.Text = TVTranslation;
                UIElement.TranslationView.Editable = false;
            }
            UIElement.TranslationView.ModifyFont(Pango.FontDescription.FromString(FontStyle));
        }

        public void UpdateDescriptionView(System.Collections.Generic.List<string> TVdescrption)
        {
            //DialogBoxWindow(UIElement.testingMode.ActiveText);

            UIElement.DescriptionView.ModifyFont(Pango.FontDescription.FromString(discrFontStyle));
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            foreach (string tmp in TVdescrption)
            {
                foreach (string tmp1 in tmp.Split(';'))
                {
                    tmp1.TrimStart('|');
                    foreach(string tmp2 in tmp1.Split('|'))
                    {
                        int ogStringLength = tmp2.Length;
                        if(ogStringLength > 70)
                        {
                            builder.AppendLine(tmp2);
                            double tm = ogStringLength / 70;
                            int ceilingVal = (int)Math.Ceiling(tm);
                            for (int ii =1;ii<= ceilingVal; ii++)
                            {
                                builder.Insert(70 * ii, "\n ");
                            }
                        }
                        else
                        {
                            builder.AppendLine(tmp2);
                        }
                    }
                }   
            }
            System.Text.StringBuilder encrypted = new System.Text.StringBuilder();
            System.Text.StringBuilder Encryptedbuilder = new System.Text.StringBuilder();
            if (UIElement.testingMode.ActiveText.Equals("Description Test"))
            {
                foreach (char tmpCh in builder.ToString())
                {
                    encrypted.Append('#');
                }
                int ogStringLength = encrypted.Length;
                if (ogStringLength > 20)
                {
                    Encryptedbuilder.AppendLine(encrypted.ToString());
                    double tm = ogStringLength / 20;
                    int ceilingVal = (int)Math.Ceiling(tm);
                    for (int ii = 1; ii <= ceilingVal; ii++)
                    {
                        Encryptedbuilder.Insert(20 * ii, "\n ");
                    }
                }
                else
                {
                    Encryptedbuilder.AppendLine(encrypted.ToString());
                }
                UIElement.DescriptionView.Buffer.Text = Encryptedbuilder.ToString();
                UIElement.DescriptionView.Editable = true;
            }
            else
            {
                UIElement.DescriptionView.Buffer.Text = builder.ToString();
                UIElement.DescriptionView.Editable = false;
            }
        }
        
    }
}