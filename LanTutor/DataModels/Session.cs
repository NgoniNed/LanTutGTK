using System;

namespace LanTutor.DataModels
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string Language { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public User User { get; set; }
    }


}
