using LanTutor.DataModels;
using LanTutor.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanTutor.Services
{
    public class SessionService : ISessionService
    {
        private readonly LanTutorContext _context;

        public SessionService(LanTutorContext context)
        {
            _context = context;
        }

        public Session StartSession(int userId, string language)
        {
            var newSession = new Session
            {
                UserId = userId,
                Language = language,
                StartTime = DateTime.Now
            };

            _context.Sessions.Add(newSession);
            _context.SaveChanges();

            return newSession;
        }

        public void EndSession(int sessionId)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session != null && session.EndTime == null)
            {
                session.EndTime = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public List<Session> GetSessionsByUser(int userId)
        {
            return _context.Sessions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartTime)
                .ToList();
        }

        public Session GetActiveSession(int userId)
        {
            return _context.Sessions
                .FirstOrDefault(s => s.UserId == userId && s.EndTime == null);
        }
    }

}
