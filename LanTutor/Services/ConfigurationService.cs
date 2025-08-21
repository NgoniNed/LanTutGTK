namespace LanTutor.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public UserSettings GetUserSettings()
        {
            return LTReadFile.getUserSettings();
        }

        public int GetCurrentUserId()
        {
            return 1;
        }
    }
}