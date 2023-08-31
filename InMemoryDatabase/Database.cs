using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public static class Database
    {
        public static List<User> Users = new()
        {
            new User()
            {
                Email ="testuser@gmail.com",
                Password = "testuser123@!"
            }
        };
    }
}
