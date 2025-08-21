using Gtk;
using LanTutor.Services;
using System.Text;

namespace LanTutor.Windows
{
    public class ProgressDashboardWindow : Window
    {
        public ProgressDashboardWindow(int userId) : base("Progress Dashboard")
        {
            SetDefaultSize(600, 400);
            SetPosition(WindowPosition.Center);

            VBox layout = new VBox(false, 5);
            ScrolledWindow scroll = new ScrolledWindow();
            TextView dashboardText = new TextView
            {
                Editable = false,
                CursorVisible = false
            };

            var scoreService = new ScoreService(new LanTutor.Database.LanTutorContext());
            var sessionService = new SessionService(new LanTutor.Database.LanTutorContext());

            var scores = scoreService.GetScoresByUser(userId);
            var sessions = sessionService.GetSessionsByUser(userId);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" User Progress Overview\n");

            foreach (var session in sessions)
            {
                builder.AppendLine($"Session: {session.Language} | Started: {session.StartTime} | Ended: {session.EndTime}");
            }

            builder.AppendLine("\n Score Breakdown:");
            foreach (DataModels.WordScore score in scores)
            {
                builder.AppendLine($"Word ID: {score.WordScoreId} | Score: {score.Score} | Attempts: {score.Attempts} | Time: {score.TimeSpent}");
            }

            dashboardText.Buffer.Text = builder.ToString();
            scroll.Add(dashboardText);
            layout.PackStart(scroll, true, true, 0);
            Add(layout);
            ShowAll();
        }
    }
}
