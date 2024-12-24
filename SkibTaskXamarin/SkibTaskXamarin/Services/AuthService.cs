using SkibTaskXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkibTaskXamarin.Services
{
    public class AuthService
    {
        private readonly List<User> _users = new List<User>();

        public bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username)) return false;
            _users.Add(new User { Username = username, Password = password });
            return true;
        }

        public bool Login(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }
    }
}
