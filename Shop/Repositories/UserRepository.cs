using System.Collections.Generic;
using System.Linq;
using Shop.Models;

namespace Shop.Repositories
{
    public static class UserRepository
    {
        public static User GetUser (string username, string password) {
            var users = new List<User>();
            users.Add(new User{
                Id = 1, 
                Username = "messzsoarz",
                Password = "2307",
                Role = "manager"
            });
            users.Add(new User{
                Id = 2, 
                Username = "soaresmessiasg130",
                Password = "2307",
                Role = "employee"
            });
            users.Add(new User{
                Id = 3, 
                Username = "admin",
                Password = "2307",
                Role = "dev"
            });

            return users.Where(x => 
                x.Username.ToLower() == username.ToLower() && 
                x.Password == password
            ).FirstOrDefault<User>();
        }
    }
}