using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CintaUang.ViewModels.AuthViewModels;
using Service.Modules;
using Model.Domain;
using Model.Domain.DB;
using Model.Domain.DB.UserDB;

namespace CintaUang.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IndexViewModel viewModels)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModels);
            }

            User user = await userService.Login(new User
            {
                UserEmail = viewModels.UserEmail,
                UserPassword = viewModels.UserPassword
            });

            if (user == null)
            {
                AddNotification(ViewNotification.Make("User Tidak Ditemukan", ViewNotification.ERROR));
                return View("Index", viewModels);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModels)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", viewModels);
            }

            ExecuteResult result = await userService.Register(new User
            {
                UserName = viewModels.UserName,
                UserEmail = viewModels.UserEmail,
                UserPassword = viewModels.UserPassword
            }, 1);

            if (result == null)
            {
                return View(viewModels);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}