using Microsoft.AspNetCore.Mvc;
using Proj.BLL.Services.Contracts;
using Proj.DAL.DataContext;
using Proj.PLL.Models;

namespace Proj.PLL.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly VshopContext _context;
        private readonly IDeveloperService _developerService;
        public LoginController(IAdminService adminService, IUserService userService, VshopContext context, IDeveloperService developerService)
        {
            _adminService = adminService;
            _userService = userService;
            _context = context;
            _developerService = developerService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role == "User")
                {
                    Console.WriteLine($"Username is {model.Email}");
                    Console.WriteLine($"Password is {model.Password}");
                    var user = _userService.ReadUserbyMail(model.Email);
                    Console.WriteLine(user.Email);
                    Console.WriteLine(user.Password);
                    Console.WriteLine(_userService.LoginUser(model.Email, model.Password.ToLower()));

                    if (user != null && _userService.LoginUser(model.Email,model.Password.ToLower()))
                    {
                        Console.WriteLine("Login Successful!");
                        HttpContext.Session.SetInt32("UserId", user.Id);
                        return RedirectToAction("IndexUsers", "Login");
                        

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email or password");
                    }
                }
                else if (model.Role == "Admin")
                {
                    var admin = _adminService.ReadUserbyMail(model.Email);
                    if (admin != null && _adminService.LoginAdmin(model.Email, model.Password))
                    {
                        return RedirectToAction("IndexAdmin", "Admin");
                    } 
                }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email or password");
                    }
                }
            return View(model);

        }
        public IActionResult IndexUsers()
        {
            return View();
        }

        public IActionResult Create(string devName)
        {
            var dev = _developerService.GetDevByName(devName);
            Console.WriteLine(dev.Name);
                return RedirectToAction("IndexAdmin", "Login");
         
        }

            
        }


    }

