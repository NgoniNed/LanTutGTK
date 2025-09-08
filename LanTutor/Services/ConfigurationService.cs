using System;

namespace LanTutor.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private static int currentUserId = 1;

        public UserSettings GetUserSettings()
        {
            return LTReadFile.getUserSettings();
        }

        public int GetCurrentUserId()
        {
            return currentUserId;
        }

        internal static void SetCurrentUserId(int userId)
        {
            currentUserId = userId;
        }
    }
}