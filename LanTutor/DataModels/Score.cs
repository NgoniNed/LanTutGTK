namespace LanTutor.DataModels
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public int WordId { get; set; }
        public int Attempts { get; set; } = 0;
        public double ScoreValue { get; set; } = 0;
        public string TimeSpent { get; set; }

        public User User { get; set; }
        public WordTransDef Word { get; set; }
    }


}
