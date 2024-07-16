using BusinessObjects.Models;
using Repositories;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;
        public UserService(){
            iUserRepository = new UserRepository();
        }

        public List<User> GetUserByRole(string role)
            => iUserRepository.GetUserByRole(role);
        public User GetUserByUserId(int userId)
            => iUserRepository.GetUserByUserId(userId);
        public void AddUser(User user)
            => iUserRepository.AddUser(user);
        public void UpdateUser(User user)
            => iUserRepository.UpdateUser(user);
        public void DeleteUser(int userId)
            => iUserRepository.DeleteUser(userId);

        public List<User> GetUsers()
            => iUserRepository.GetUsers();

        public bool IsExistUserName(string username)
            => iUserRepository.IsExistUserName(username);

        public bool IsExistEmail(string email)
            => iUserRepository.IsExistEmail(email);
    }
}
