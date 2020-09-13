using System;
using System.IO;
using Gtk;

namespace LanTutor
{
    internal class LTGUIDesign : Window
    {
        private Gdk.Color cLightSlateBlue = new Gdk.Color(132, 112, 255);
        private Gdk.Color cBabyBlue = new Gdk.Color(99, 184, 255);
        private static string FontStyle = "Noteworthy 24";
        ComboBox QuestionTracker;// = new ComboBox(QuestionCounter);

        private static TextView MotherTongueView = new TextView();
        private static TextView TranslationView = new TextView();
        private static TextView DescriptionView = new TextView();
        private static Button upDescriptionBtn = new Button("Up");
        private static Button downDescriptionBtn = new Button(Stock.GoDown);
        private static System.Collections.Generic.List<string> descriptionList = new System.Collections.Generic.List<string>();
        private static Fixed buttonFix = new Fixed();
        private static int descriptioniter = 0;
        int QuestionIter = 1;
        private System.Xml.XmlNodeList sessionDataList;// = new System.Xml.XmlNodeList();
        internal LTGUIDesign() : base("Language Tutor")
        {
            //DescriptionView.Editable = false;
            TranslationView.Editable = false;
            MotherTongueView.Editable = false;

            
            SetupScreenSize(cLightSlateBlue);
            SetupMenuBar(cBabyBlue);
            SetupGUIButtons();
            SetupGUITextArea();
            //SetupGUITextInput(buttonFix);
            SetupGUIEventHandlers();
            
            //Console.WriteLine();
            sessionDataList = LTPhaseOneCore.ExecuteProgramBackend();
            
            LTPhaseOneCore.GetQuestion(ref sessionDataList,QuestionIter);
            
            Add(buttonFix);
            ShowAll();
        }

        private void SetupGUITextInput(Fixed buttonFix)
        {
            throw new NotImplementedException();
        }
        public static void UpdateMotherTongueView(string TVMotherTongue)
        {

            MotherTongueView.Buffer.Text = TVMotherTongue;
            MotherTongueView.ModifyFont(Pango.FontDescription.FromString(FontStyle));
        }
        public static void UpdateTranslationView(string TVTranslation)
        {

            TranslationView.Buffer.Text = TVTranslation;
            TranslationView.ModifyFont(Pango.FontDescription.FromString(FontStyle));
        }
        public static void UpdateDescriptionView(System.Collections.Generic.List<string> TVdescrption)
        {
            DescriptionView.ModifyFont(Pango.FontDescription.FromString(FontStyle));
            
            foreach (string tmp in TVdescrption)
            {
                foreach(string tmp1 in tmp.Split(';'))
                {
                    descriptionList.Add(tmp1);
                    //string tmp2 = DescriptionView.Buffer.Text;
                    //DescriptionView.Buffer.Text = tmp2+"\n" + tmp1;
                }
                
            }
            DescriptionView.Buffer.Text = descriptionList[descriptioniter];
        }
        private void SetupGUIEventHandlers()
        {
            DeleteEvent += LTGUIDesign_DeleteEvent;
            upDescriptionBtn.Clicked += UpDescriptionBtn_Clicked;
            downDescriptionBtn.Clicked += DownDescriptionBtn_Clicked;
        }

        private void DownDescriptionBtn_Clicked(object sender, EventArgs e)
        {
            if(descriptioniter.Equals(descriptionList.Count-1))
            {
                descriptioniter = 0;
                DescriptionView.Buffer.Text = descriptionList[descriptioniter];
            }
            else
            {
                DescriptionView.Buffer.Text = descriptionList[descriptioniter++];
            }
        }

        private void UpDescriptionBtn_Clicked(object sender, EventArgs e)
        {
            if (descriptioniter<0)
            {
                descriptioniter = descriptionList.Count-1;
                DescriptionView.Buffer.Text = descriptionList[descriptioniter];
            }
            else
            {
                DescriptionView.Buffer.Text = descriptionList[descriptioniter--];
            }
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
            //suggested ammendment is 40pix of the height from original 40
            TranslationView.SetSizeRequest(470, 80);

            Label DescriptionViewLabel = new Label("Defination");
            DescriptionViewLabel.SetSizeRequest(120, 40);

            DescriptionView.ModifyBg(StateType.Normal, new Gdk.Color(20, 20, 20));
            //DescriptionView.CursorVisible = false;
            DescriptionView.SetSizeRequest(440, 200);

            buttonFix.Put(MotherTongueViewLabel, 190, 180);
            buttonFix.Put(MotherTongueView, 25, 220);

            buttonFix.Put(TranslationViewLabel, 190, 300);
            buttonFix.Put(TranslationView, 25, 340);

            buttonFix.Put(DescriptionViewLabel, 190, 420);
            buttonFix.Put(DescriptionView, 25, 460);

            upDescriptionBtn.SetSizeRequest(40, 40);
            upDescriptionBtn.ModifyBg(StateType.Normal, new Gdk.Color(238, 213, 183));
            downDescriptionBtn.SetSizeRequest(40, 40);
            downDescriptionBtn.ModifyBg(StateType.Normal, new Gdk.Color(238, 213, 183));
            
            int xUpDownBtn = 470;
            int yUpDownBtn = 500;
            int paddingUpDown = 80;
            buttonFix.Put(upDescriptionBtn, xUpDownBtn, yUpDownBtn);
            buttonFix.Put(downDescriptionBtn, xUpDownBtn, yUpDownBtn + paddingUpDown);
            //Add(currentLayout);
        }

        private void SetupGUIButtons()
        {
            

            int buttonHeight, buttonWidth;
            buttonHeight = 40;
            buttonWidth = 120;
            int maxScore = 50;
            int maxAttempts = 100;
            string[] testType = new string[]
            {
                "Practice",
                "Word Test",
                "Description Test"
            };
            Label testingModeLabel = new Label("Test Type");
            ComboBox testingMode = new ComboBox(testType);
            
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

            string[] scoreCounter = new string[maxScore];

            for (int i = 0;i< maxScore; i++)
            {
                scoreCounter[i] = i.ToString();
            }
            
            string[] QuestionCounter = new string[maxScore];
            int totalQuestions = 50;
            for (int i = 1; i < totalQuestions; i++)
            {
                QuestionCounter[i] = (i+" / "+ totalQuestions).ToString();
            }

            Label QuestionTrackerLabel = new Label("Question Number");
            QuestionTracker = new ComboBox(QuestionCounter);
            //QuestionTracker = ComboBoxEntry(QuestionCounter);
            QuestionTracker.Sensitive = false;
            QuestionTracker.SetSizeRequest(80, 40);
            TreeIter Questioniter;
            QuestionTracker.Model.GetIterFirst(out Questioniter);
            QuestionTracker.SetActiveIter(Questioniter);
            buttonFix.Put(QuestionTrackerLabel, 400, 120);
            buttonFix.Put(QuestionTracker, 400, 140);

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

            ComboBox LanguageComboOptions = new ComboBox(LTReadFile.GetListOfTranslationOptions);
            LanguageComboOptions.SetSizeRequest(100, 40);
            TreeIter iter;
            LanguageComboOptions.Model.GetIterFirst(out iter);
            LanguageComboOptions.SetActiveIter(iter);
            Label languagelabel = new Label("Language Selection");
            buttonFix.Put(languagelabel, 400, 40);
            buttonFix.Put(LanguageComboOptions, 400, 60);

            //Button PrepareSession = new Button("Load Session");
            Button NextQuestion = new Button("Next Question");
            Button PreviousQuestion = new Button("Previous Question");
            Button SubmitAnswer = new Button("Submit Answer");
            Button EndSession = new Button("End Session");

            //PrepareSession.SetSizeRequest(buttonWidth,buttonHeight);
            NextQuestion.SetSizeRequest(buttonWidth, buttonHeight);
            PreviousQuestion.SetSizeRequest(buttonWidth, buttonHeight);
            SubmitAnswer.SetSizeRequest(buttonWidth, buttonHeight);
            EndSession.SetSizeRequest(buttonWidth, buttonHeight);

            NextQuestion.Clicked += NextQuestion_Clicked;
            PreviousQuestion.Clicked += PreviousQuestion_Clicked;
            SubmitAnswer.Clicked += SubmitAnswer_Clicked;
            EndSession.Clicked += EndSession_Clicked;

            //currentLayout.Put(PrepareSession,0,60);
            buttonFix.Put(NextQuestion, ((0 * buttonWidth) + 20), 700);
            buttonFix.Put(PreviousQuestion, ((1 * buttonWidth) +20) , 700);
            buttonFix.Put(SubmitAnswer, ((2 * buttonWidth) + 20), 700);
            buttonFix.Put(EndSession, ((3 * buttonWidth) + 20), 700);
            
            //Add(currentLayout);
        }

        private void PreviousQuestion_Clicked(object sender, EventArgs e)
        {
            LTPhaseOneCore.GetQuestion(ref sessionDataList,QuestionIter--);
        }

        private void NextQuestion_Clicked(object sender, EventArgs e)
        {
            
            LTPhaseOneCore.GetQuestion(ref sessionDataList, QuestionIter++);
        }

        private void SubmitAnswer_Clicked(object sender, EventArgs e)
        {
            //UpdateMotherTongueView();
        }

        private void EndSession_Clicked(object sender, EventArgs e)
        {
            //save document before exiting the app
            Application.Quit();
        }

        /// <summary>
        /// this method sets up the screen dimensions and
        /// background color of the main window
        /// </summary>
        private void SetupScreenSize(Gdk.Color lBgColor)
        {
            SetDefaultSize(525, 800);
            SetPosition(WindowPosition.CenterOnParent);
            ModifyBg(StateType.Normal, lBgColor);
            bool GUIIconSet = SetIconFromFile("/Volumes/Secondary/Projects/PersonalGTK/LanTutor/LanTutor/GUIResources/LanTut.png");
            Console.WriteLine("GUI Icon Set:\t" + GUIIconSet);
            
        }

        private void LTGUIDesign_DeleteEvent(object o, DeleteEventArgs args)
        {
            Application.Quit();
        }

        private void SetupMenuBar( Gdk.Color cBabyBlue)
        {
            MenuBar LTmenu = new MenuBar();
            LTmenu.ModifyBg(StateType.Normal, cBabyBlue);

            Menu filemenu = new Menu();
            MenuItem file = new MenuItem("File");
            file.Submenu = filemenu;

            Menu SessionSubMenu = new Menu();
            MenuItem SessionMenu = new MenuItem("Session");
            SessionMenu.Submenu = SessionSubMenu;

            MenuItem LoadSession = new MenuItem("Load Session");
            LoadSession.Activated += LoadSession_Activated;
            MenuItem EndSession = new MenuItem("End Session");
            EndSession.Activated += EndSession_Activated;

            SessionSubMenu.Append(LoadSession);
            SessionSubMenu.Append(EndSession);

            MenuItem exit = new MenuItem("Exit");
            exit.Activated += Exit_Activated;

            MenuItem AboutMenuItem = new MenuItem("About");

            //filemenu.Append(LoadSession);
            //filemenu.Append(exit);

            LTmenu.Append(file);
            LTmenu.Append(SessionMenu);
            LTmenu.Append(exit);
            LTmenu.Append(AboutMenuItem);

            buttonFix.Put(LTmenu, 0, 0);
            //VBox menuBox = new VBox(false, 2);
            //menuBox.PackStart(LTmenu, false, false, 2);

            //Add(menuBox);

        }

        private void LoadSession_Activated(object sender, EventArgs e)
        {
            LTPhaseOneCore.ExecuteProgramBackend();
        }

        private void EndSession_Activated(object sender, EventArgs e)
        {
            Application.Quit();
        }

        private void Exit_Activated(object sender, EventArgs e)
        {
            Application.Quit();
        }
    }
}