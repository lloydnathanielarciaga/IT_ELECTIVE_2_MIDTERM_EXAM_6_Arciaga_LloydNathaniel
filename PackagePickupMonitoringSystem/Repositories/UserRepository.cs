using PackagePickupMonitoringSystem.Models;

namespace PackagePickupMonitoringSystem.Repositories
{
    public static class UserRepository
    {
        private static List<User> _users = new List<User>(); 
        private static int _nextId = 1;

        public static void AddUser(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public static User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
    }
}