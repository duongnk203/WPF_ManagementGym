using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
            => UserDAO.AddUser(user);

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUserByRole(string role)
            => UserDAO.GetUserByRole(role);

        public User GetUserByUserId(int userId)
            => UserDAO.GetUserByUserId(userId);

        public void UpdateUser(User user)
            => UserDAO.UpdateUser(user);

        public List<User> GetUsers()
            => UserDAO.GetUsers();

        public bool IsExistUserName(string username)
            => UserDAO.IsExistUserName(username);

        public bool IsExistEmail(string email)
            => UserDAO.IsExistEmail(email);
    }
}
