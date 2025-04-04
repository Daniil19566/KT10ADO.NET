using Microsoft.EntityFrameworkCore;

namespace KT10ADO.NET
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Добавление нового пользователя с профилем
        public void AddUser(string name, string bio)
        {
            var user = new User { Name = name, Profile = new UserProfile { Bio = bio } };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Получение всех пользователей с профилями
        public IQueryable<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.Profile).AsQueryable();
        }

        // Получение пользователя по Id
        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Profile).FirstOrDefault(u => u.UserId == id);
        }

        // Обновление пользователя и его профиля
        public void UpdateUser(int id, string name, string bio)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.Name = name;
                user.Profile.Bio = bio;
                _context.SaveChanges();
            }
        }

        // Удаление пользователя и его профиля
        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
