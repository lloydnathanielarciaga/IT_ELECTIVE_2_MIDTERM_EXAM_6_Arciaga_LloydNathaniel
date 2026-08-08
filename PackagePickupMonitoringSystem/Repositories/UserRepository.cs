using PackagePickupMonitoringSystem.Models;
using System.Collections.Generic;

namespace PackagePickupMonitoringSystem.Repositories
{
    public static class UserRepository
    {
        private static List<User> userList = new List<User>();
        private static int nextUserId = 1;

        public static void AddUser(User u)
        {
            u.Id = nextUserId;
            nextUserId++;
            userList.Add(u);
        }

        public static User GetUserByUsername(string uname)
        {
            foreach (User u in userList)
            {
                if (u.Username == uname)
                {
                    return u;
                }
            }
            return null;
        }
    }
}