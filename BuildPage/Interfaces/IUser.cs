using BuildPage.Models;

namespace BuildPage.Interfaces
{
    public interface IUser
    {
        UserModel? Login(UserModel loginUser);
        UserModel? GetUser(int id);
        void ChangePassword(UserModel user);
        void ChangeName(UserModel user);
    }
}
