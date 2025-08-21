using System.Collections.Generic;

namespace LanTutor.DataModels
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "Learner";

        public ICollection<Session> Sessions { get; set; }
        public ICollection<Score> Scores { get; set; }
    }


}
