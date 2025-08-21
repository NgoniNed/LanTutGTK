using LanTutor.DataModels;

namespace LanTutor.Services
{
    public interface IUserService
    {
        bool RegisterUser(string username, string password);
        User GetUser(string username);
        bool ValidateUser(string username, string password);
    }

}