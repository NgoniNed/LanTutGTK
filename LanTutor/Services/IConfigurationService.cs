namespace LanTutor.Services
{
    public interface IConfigurationService
    {
        UserSettings GetUserSettings();
        int GetCurrentUserId();
    }
}