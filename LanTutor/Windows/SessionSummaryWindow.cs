using Gtk;
using LanTutor.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanTutor.Windows
{
    public class SessionSummaryWindow : Window
    {
        public SessionSummaryWindow(List<WordTransDef> sessionWords) : base("Session Summary")
        {
            SetDefaultSize(600, 400);
            SetPosition(WindowPosition.Center);

            VBox layout = new VBox(false, 5);
            ScrolledWindow scroll = new ScrolledWindow();
            TextView summaryText = new TextView
            {
                Editable = false,
                CursorVisible = false
            };
            Button saveButton = new Button("Save Summary");
            saveButton.Clicked += (sender, e) =>
            {
                SaveSummaryToXml(sessionWords);
                SaveSummaryToJson(sessionWords);
                MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Session saved to XML and JSON.");
                dialog.Run();
                dialog.Destroy();
            };

            layout.PackStart(saveButton, false, false, 5);

            StringBuilder builder = new StringBuilder();
            foreach (var word in sessionWords)
            {
                var wordScore = word.lWordScore ?? new ScoreParameters();
                var descrScore = word.lDescriptionScore ?? new ScoreParameters();

                builder.AppendLine($" {word.lword} → {word.lTrans}");
                builder.AppendLine($"   Word Score: {wordScore.Score}, Attempts: {wordScore.Attempts}, Time: {wordScore.TimeSpent}");
                builder.AppendLine($"   Description Score: {descrScore.Score}, Attempts: {descrScore.Attempts}, Time: {descrScore.TimeSpent}");
                builder.AppendLine();
            }


            summaryText.Buffer.Text = builder.ToString();
            scroll.Add(summaryText);
            layout.PackStart(scroll, true, true, 0);
            Add(layout);
            ShowAll();
        }

        private void SaveSummaryToXml(List<WordTransDef> sessionWords)
        {
            var scoreCard = new LTSessionScoreCard
            {
                SessionLibrary = sessionWords.ConvertAll(w => new WordTransDefDict
                {
                    lword = w.lword,
                    lTrans = w.lTrans,
                    ldef = w.ldef,
                    lWordScore = w.lWordScore ?? new ScoreParameters(),
                    lDescriptionScore = w.lDescriptionScore ?? new ScoreParameters()
                })
            };

            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "SessionSummary.xml");
            LTWriteFile.WriteSchemeToxml(scoreCard, "SessionSummary", Environment.CurrentDirectory + "/");
        }

        private void SaveSummaryToJson(List<WordTransDef> sessionWords)
        {
            var scoreCard = new LTSessionScoreCard
            {
                SessionLibrary = sessionWords.ConvertAll(w => new WordTransDefDict
                {
                    lword = w.lword,
                    lTrans = w.lTrans,
                    ldef = w.ldef,
                    lWordScore = w.lWordScore ?? new ScoreParameters(),
                    lDescriptionScore = w.lDescriptionScore ?? new ScoreParameters()
                })
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(scoreCard, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText("SessionSummary.json", json);
        }

    }
}
