using BuildPage.Interfaces;
using BuildPage.Models;

namespace BuildPage.Services
{
    public class UserMock : IUser
    {
        private readonly DbContextModel _context;
        public UserMock(DbContextModel context)
        {
            _context = context;
        }

        public void ChangeName(UserModel user)
        {
            var newUser = GetUser(user.Id);
            newUser.Name = user.Name;
            _context.Update(newUser);
            _context.SaveChanges();
        }

        public void ChangePassword(UserModel user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public UserModel? GetUser(int id)
        {
            var user = new UserModel();

            user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }

        }

        public UserModel? Login(UserModel loginUser)
        {
            var findUser = _context.Users.FirstOrDefault(x => x.Name == loginUser.Name && x.Password == loginUser.Password);

            UserModel newLoginUser = new UserModel();

            if (findUser != null)
            {
                return findUser;
            }
            else
            {
                return null;
            }
        }
    }
}
