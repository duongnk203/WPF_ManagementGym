using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.Identity.Client;

namespace DataAccessLayer
{
    public class UserDAO
    {
        public static List<User> GetUserByRole(string role)
        {
            var listUsers = new List<User>();
            try
            {
                using var context = new GymManagementContext();
                listUsers = context.Users.Where(u => u.Role.Equals(role)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return listUsers;
        }

        public static User GetUserByUserId(int userId)
        {
            using var context = new GymManagementContext();
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            return user;
        }

        public static void AddUser(User user)
        {
            using var context = new GymManagementContext();
            context.Users.Add(user);
            context.SaveChanges();
        }
        
        public static List<User> GetUsers()
        {
            using var context = new GymManagementContext();
            var list = context.Users.ToList();
            return list;
        }

        public static bool IsExistUserName(string username)
        {
            using var context = new GymManagementContext();
            var user = context.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public static void UpdateUser(User user)
        {
            using var context = new GymManagementContext();
            context.Users.Update(user);
            context.SaveChanges();
        }

        public static bool IsExistEmail(string email)
        {
            using var context = new GymManagementContext();
            var user = context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                return true;
            }
            return false;
        }

    }

}

