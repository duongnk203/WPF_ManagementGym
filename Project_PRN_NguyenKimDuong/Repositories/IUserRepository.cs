using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        List<User> GetUserByRole(string role);
        User GetUserByUserId(int userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        bool IsExistUserName(string username);
        List<User> GetUsers();

        bool IsExistEmail(string email);
    }
}
