using System.Diagnostics;
using KT10ADO.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KT10ADO.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Главная страница: вывод всех пользователей и профилей
        public IActionResult Index()
        {
            var viewModel = new DemoViewModel
            {
                Users = _context.Users.Include(u => u.Profile).ToList(),
                UserProfiles = _context.UserProfiles.ToList()
            };

            return View(viewModel);
        }

        // Метод для добавления нового пользователя и профиля
        [HttpPost]
        public IActionResult Create(string UserName, string UserEmail, string Bio, string UserProfileEmail)
        {
            var user = new User
            {
                UserName = UserName,
                UserEmail = UserEmail
            };
            var profile = new UserProfile
            {
                Bio = Bio,
                UserProfileEmail = UserProfileEmail,
                User = user // связываем напрямую
            };
            user.Profile = profile;

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Message"] = "Профиль успешно создан!";

            return View("Index", new DemoViewModel
            {
                Users = _context.Users.Include(u => u.Profile).ToList(),
                UserProfiles = _context.UserProfiles.ToList()
            });
        }
        [HttpPost]
        public IActionResult Delete(int UserID)
        {
            var user = _context.Users.Include(u => u.Profile).FirstOrDefault(u => u.UserID == UserID);
            if (user != null)
            {
                _context.Users.Remove(user); // каскад удалит и профиль
                _context.SaveChanges();
            }
            TempData["Message"] = "Пользователь и профиль удалены!";
            return View("Index", new DemoViewModel
            {
                Users = _context.Users.Include(u => u.Profile).ToList(),
                UserProfiles = _context.UserProfiles.ToList()
            });
        }

        // GET: /Home/Edit?id=1
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users
                .Include(u => u.Profile)
                .FirstOrDefault(u => u.UserID == id);

            if (user == null)
                return NotFound();
            TempData["Message"] = "Профиль успешно обновлён!";
            return View(user);
        }

        // POST: /Home/Edit
        [HttpPost]
        public IActionResult Edit(int UserID, string UserName, string UserEmail, string Bio, string UserProfileEmail)
        {
            var user = _context.Users.Include(u => u.Profile).FirstOrDefault(u => u.UserID == UserID);
            if (user == null) return NotFound();

            user.UserName = UserName;
            user.UserEmail = UserEmail;

            if (user.Profile != null)
            {
                user.Profile.Bio = Bio;
                user.Profile.UserProfileEmail = UserProfileEmail;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}