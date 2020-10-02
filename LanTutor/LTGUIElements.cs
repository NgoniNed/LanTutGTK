using System;
using Gtk;

namespace LanTutor
{
    internal class LTGUIElements
    {
        private Gdk.Color cLightSlateBlue = new Gdk.Color(132, 112, 255);
        private Gdk.Color cBabyBlue = new Gdk.Color(99, 184, 255);
        public ComboBox QuestionTracker;
        protected static MenuItem LoadSession = new MenuItem("Load Session");
        protected static Button NextQuestion = new Button("Next Question");
        protected static Button PreviousQuestion = new Button("Previous Question");
        protected static Button SubmitAnswer = new Button("Submit Answer");
        protected static MenuItem exit = new MenuItem("Exit");
        protected static MenuItem EndSession = new MenuItem("End Session");
        protected static MenuItem AboutMenuItem = new MenuItem("About");
        protected static Button EndSessionbtn = new Button("End Session");
        public ComboBox LanguageComboOptions = new ComboBox(LTReadFile.GetListOfTranslationOptions);
        public TextView MotherTongueView = new TextView();
        public TextView TranslationView = new TextView();
        public TextView DescriptionView = new TextView();
        public ComboBox testingMode;
        public System.Xml.XmlNodeList sessionDataList { get; set; }
        public int QuestionIterator = 0;
        protected TreeIter QuestionTreeiter;


        private Fixed buttonFix
        {
            get;
        }
        LTGUIDesign parentWind
        {
            get;
            set;
        }
        //; = new Fixed();

        public LTGUIElements(string appTitle,LTGUIDesign mainWind)
        {
            sessionDataList= LTPhaseOneCore.ExecuteProgramBackend(LanguageComboOptions.ActiveText);
            LTGUIDesign.DialogBoxWindow("LTGUIElements"+"\n\t"+sessionDataList.Count.ToString());
            parentWind = mainWind;
            buttonFix = new Fixed();
            
            SetupScreenSize();
            SetupMenuBar();
            SetupGUIButtons();
            SetupGUITextArea();
            SetupComboBox();
            SetupGUIEventHandlers();
            //parentWind.LoadInitialQuestion(sessionDataList);
            parentWind.Add(buttonFix);
            //LTEvents eventHandlingClass = new LTEvents();
        }

        private void SetupMenuBar()
        {
            MenuBar LTmenu = new MenuBar();
            LTmenu.ModifyBg(StateType.Normal, cBabyBlue);

            Menu filemenu = new Menu();
            MenuItem file = new MenuItem("File");
            file.Submenu = filemenu;

            Menu SessionSubMenu = new Menu();
            MenuItem SessionMenu = new MenuItem("Session");
            SessionMenu.Submenu = SessionSubMenu;

            SessionSubMenu.Append(LoadSession);
            SessionSubMenu.Append(EndSession);

            LTmenu.Append(file);
            LTmenu.Append(SessionMenu);
            LTmenu.Append(exit);
            LTmenu.Append(AboutMenuItem);

            buttonFix.Put(LTmenu, 0, 0);
        }
        /// <summary>
        /// this method sets up the screen dimensions and
        /// background color of the main window
        /// </summary>
        private void SetupScreenSize()
        {
            parentWind.SetDefaultSize(525, 800);
            parentWind.SetPosition(WindowPosition.CenterOnParent);
            parentWind.ModifyBg(StateType.Normal, cLightSlateBlue);
            bool GUIIconSet = parentWind.SetIconFromFile("/Volumes/Secondary/Projects/PersonalGTK/LanTutor/LanTutor/GUIResources/LanTut.png");
            Console.WriteLine("GUI Icon Set:\t" + GUIIconSet);
        }
        private void SetupGUIButtons()
        {
            int buttonHeight, buttonWidth;
            buttonHeight = 40;
            buttonWidth = 120;

            NextQuestion.SetSizeRequest(buttonWidth, buttonHeight);
            PreviousQuestion.SetSizeRequest(buttonWidth, buttonHeight);
            SubmitAnswer.SetSizeRequest(buttonWidth, buttonHeight);
            EndSessionbtn.SetSizeRequest(buttonWidth, buttonHeight);

            buttonFix.Put(NextQuestion, ((0 * buttonWidth) + 20), 700);
            buttonFix.Put(PreviousQuestion, ((1 * buttonWidth) + 20), 700);
            buttonFix.Put(SubmitAnswer, ((2 * buttonWidth) + 20), 700);
            buttonFix.Put(EndSessionbtn, ((3 * buttonWidth) + 20), 700);
        }

        private void SetupGUITextArea()
        {

            Label MotherTongueViewLabel = new Label("Mother Language");
            MotherTongueViewLabel.SetSizeRequest(120, 40);

            MotherTongueView.ModifyBg(StateType.Normal, new Gdk.Color(20, 20, 20));
            MotherTongueView.CursorVisible = false;
            MotherTongueView.SetSizeRequest(470, 80);

            Label TranslationViewLabel = new Label("Translation");
            TranslationViewLabel.SetSizeRequest(120, 40);

            TranslationView.ModifyBg(StateType.Normal, new Gdk.Color(20, 20, 20));
            TranslationView.CursorVisible = false;
            TranslationView.SetSizeRequest(470, 80);

            Label DescriptionViewLabel = new Label("Defination");
            DescriptionViewLabel.SetSizeRequest(120, 40);

            DescriptionView.ModifyBg(StateType.Normal, new Gdk.Color(20, 20, 20));
            //DescriptionView.CursorVisible = false;
            DescriptionView.SetSizeRequest(470, 200);

            buttonFix.Put(MotherTongueViewLabel, 190, 180);
            buttonFix.Put(MotherTongueView, 25, 220);

            buttonFix.Put(TranslationViewLabel, 190, 300);
            buttonFix.Put(TranslationView, 25, 340);

            buttonFix.Put(DescriptionViewLabel, 190, 420);
            buttonFix.Put(DescriptionView, 25, 460);
        }

        private void SetupComboBox()
        {
            int maxScore = 50;
            int maxAttempts = 100;

            string[] testType = new string[]
            {
                "Practice",
                "Word Test",
                "Description Test"
            };
            Label testingModeLabel = new Label("Test Type");
            testingMode = new ComboBox(testType);
            testingMode.SetSizeRequest(120, 40);
            TreeIter Testiter;
            testingMode.Model.GetIterFirst(out Testiter);
            testingMode.SetActiveIter(Testiter);
            buttonFix.Put(testingModeLabel, 50, 40);
            buttonFix.Put(testingMode, 20, 60);

            string[] AttemptCounter = new string[maxAttempts];
            for (int i = 0; i < maxAttempts; i++)
            {
                AttemptCounter[i] = (i + " / " + maxAttempts).ToString();
            }
            Label AttemptTrackerLabel = new Label("Number of Attempts");
            ComboBox AttemptsTracker = new ComboBox(AttemptCounter);
            AttemptsTracker.Sensitive = false;
            AttemptsTracker.SetSizeRequest(80, 40);
            TreeIter Attemptiter;
            AttemptsTracker.Model.GetIterFirst(out Attemptiter);
            AttemptsTracker.SetActiveIter(Attemptiter);
            buttonFix.Put(AttemptTrackerLabel, 260, 120);
            buttonFix.Put(AttemptsTracker, 260, 140);

            string[] QuestionCounter = new string[maxScore];
            for (int i = 1; i < sessionDataList.Count; i++)
            {
                QuestionCounter[i] = (i + " / " + sessionDataList.Count).ToString();
            }
            Label QuestionTrackerLabel = new Label("Question Number");
            QuestionTracker = new ComboBox(QuestionCounter);
            QuestionTracker.Sensitive = false;
            QuestionTracker.SetSizeRequest(80, 40);

            QuestionTracker.Model.GetIterFirst(out QuestionTreeiter);
            QuestionTracker.SetActiveIter(QuestionTreeiter);
            buttonFix.Put(QuestionTrackerLabel, 400, 120);
            buttonFix.Put(QuestionTracker, 400, 140);

            string[] scoreCounter = new string[maxScore];
            for (int i = 0; i < maxScore; i++)
            {
                scoreCounter[i] = i.ToString();
            }
            Label userWordScoreLabel = new Label("Word Score");
            ComboBox UserwordScore = new ComboBox(scoreCounter);
            UserwordScore.Sensitive = false;
            UserwordScore.SetSizeRequest(80, 40);
            TreeIter worditer;
            UserwordScore.Model.GetIterFirst(out worditer);
            UserwordScore.SetActiveIter(worditer);
            buttonFix.Put(userWordScoreLabel, 20, 120);
            buttonFix.Put(UserwordScore, 20, 140);

            Label userDescrScoreLabel = new Label("Description Score");
            ComboBox UserDescriptionScore = new ComboBox(scoreCounter);
            UserDescriptionScore.Sensitive = false;
            UserDescriptionScore.SetSizeRequest(80, 40);
            TreeIter descriter;
            UserDescriptionScore.Model.GetIterFirst(out descriter);
            UserDescriptionScore.SetActiveIter(descriter);
            buttonFix.Put(userDescrScoreLabel, 140, 120);
            buttonFix.Put(UserDescriptionScore, 140, 140);

            LanguageComboOptions.SetSizeRequest(100, 40);
            TreeIter iter;
            LanguageComboOptions.Model.GetIterFirst(out iter);
            LanguageComboOptions.SetActiveIter(iter);
            Label languagelabel = new Label("Language Selection");
            buttonFix.Put(languagelabel, 400, 40);
            buttonFix.Put(LanguageComboOptions, 400, 60);
        }

        internal void SetupGUIEventHandlers()
        {
            parentWind.DeleteEvent += Exit_Activated;
            LanguageComboOptions.Changed += LanguageComboOptions_Changed;
            testingMode.Changed += TestingMode_Changed;
            NextQuestion.Clicked += NextQuestion_Clicked;
            PreviousQuestion.Clicked += PreviousQuestion_Clicked;
            SubmitAnswer.Clicked += SubmitAnswer_Clicked;
            EndSessionbtn.Clicked += EndSession_Activated;
            LoadSession.Activated += LoadSession_Activated;
            EndSession.Activated += EndSession_Activated;
            exit.Activated += Exit_Activated;
        }

        private void LanguageComboOptions_Changed(object sender, EventArgs e)
        {
            int languageIndex = 0;
            ComboBox currentSender = (ComboBox)sender;
            string activeLanguage = currentSender.ActiveText;
            foreach (string langString in LTReadFile.GetListOfTranslationOptions)
            {
                if (activeLanguage.Equals(langString))
                {
                    LoadLanguage(ref languageIndex, ref activeLanguage, ref currentSender);
                    break;
                }
            }
        }
        /// <summary>
        /// check current language selection and loads the
        /// relevent language report card
        /// </summary>
        /// <param name="currrentLanguageSelection"></param>
        public void LoadLanguage(ref int currentLanguageIndex, ref string languageSelected, ref ComboBox ogsender)
        {
            try
            {
                string[] reportCardsList;
                bool reportExists = false;
                //access the report cards folder
                if (System.IO.Directory.Exists(Environment.CurrentDirectory + "/ReportCards") && reportExists.Equals(false))
                {
                    reportCardsList = System.IO.Directory.GetFiles(Environment.CurrentDirectory + "/ReportCards");
                    foreach (string lfileName in reportCardsList)
                    {
                        System.IO.FileInfo lfileInfo = new System.IO.FileInfo(lfileName);
                        if (lfileInfo.Name.Contains(languageSelected))
                        {
                            //load the report card
                            //DialogBoxWindow(lf)
                            sessionDataList = LanTutorXMLMoving.LoadSessionQuestions(LTReadFile.LoadXMLFile(lfileInfo.FullName), "WordTransDefLibrary/SessionLibrary/WordTransDef");
                            parentWind.LoadInitialQuestion();
                        }
                        else
                        {
                            string languagefile = string.Empty;
                            //create the report card
                            foreach(string lanfile in LTReadFile.GetTranslationDictionaries(Environment.CurrentDirectory + "/LanTutDictionaries"))
                            {
                                if (lanfile.Contains(ogsender.ActiveText))
                                {
                                    languagefile = lanfile;
                                }
                            }
                            WordTransDefLibrary llibrary = LTPhaseOneCore.DataPrep(languagefile, LTReadFile.LoadDefinations(Environment.CurrentDirectory + "/EnglishDictionaries"));
                            LTWriteFile.WriteSchemeToxml(llibrary, "/ngoni_" + ogsender.ActiveText + "_ReportCard.xml", Environment.CurrentDirectory + "/ReportCards");
                            parentWind.LoadInitialQuestion();
                        }
                    }
                }
                //check if the active language has a report card in there
                //if it is then load it to the xmlnodelist for user
                //if not then prepare the report card
            }
            catch (Exception ex)
            {
                LTGUIDesign.DialogBoxWindow(ex.Message);
            }
        }
        private void TestingMode_Changed(object sender, EventArgs e)
        {
            //LoadInitialQuestion();
        }
        private void LoadSession_Activated(object sender, EventArgs e)
        {
            if (!sessionDataList.Count.Equals(0))
            {
                LTGUIDesign.DialogBoxWindow("Session Is Ready");
            }
            else
            {
                LTGUIDesign.DialogBoxWindow("Something went wrong\nReloading Session Data");
                sessionDataList = LTPhaseOneCore.ExecuteProgramBackend(LanguageComboOptions.ActiveText);
            }
        }
        private void PreviousQuestion_Clicked(object sender, EventArgs e)
        {
            //retrieve the previous question and load into the gui
            TreePath myPath = QuestionTracker.Model.GetPath(QuestionTreeiter);
            myPath.Prev();
            QuestionTracker.Model.GetIter(out QuestionTreeiter, myPath);
            QuestionTracker.SetActiveIter(QuestionTreeiter);
            LTGUIDesign.DialogBoxWindow("No Answers Submitted\nGetting Previous Question\n" + QuestionTracker.Model.GetValue(QuestionTreeiter, 0));
            QuestionIterator--;
            parentWind.LoadInitialQuestion();
        }

        private void NextQuestion_Clicked(object sender, EventArgs e)
        {
            //retrieve the next question and load into the gui
            bool tmp = QuestionTracker.Model.IterNext(ref QuestionTreeiter);
            QuestionTracker.SetActiveIter(QuestionTreeiter);
            LTGUIDesign.DialogBoxWindow("No Answers Submitted\nGetting Next Question\n" + tmp);
            QuestionIterator++;
            parentWind.LoadInitialQuestion();
        }

        private void SubmitAnswer_Clicked(object sender, EventArgs e)
        {
            LTGUIDesign.DialogBoxWindow("Submitting Answers");
            NextQuestion_Clicked(sender, e);
        }

        private void EndSession_Activated(object sender, EventArgs e)
        {
            LTGUIDesign.DialogBoxWindow("Session Is Ending");
            Application.Quit();
        }

        private void Exit_Activated(object sender, EventArgs e)
        {
            LTGUIDesign.DialogBoxWindow("Application Is Quiting");
            Application.Quit();
        }
    }
}