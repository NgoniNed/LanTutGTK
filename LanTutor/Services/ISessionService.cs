using LanTutor.DataModels;
using System.Collections.Generic;

namespace LanTutor.Services
{
    public interface ISessionService
    {
        Session StartSession(int userId, string language);
        void EndSession(int sessionId);
        List<Session> GetSessionsByUser(int userId);
        Session GetActiveSession(int userId);
    }
}
