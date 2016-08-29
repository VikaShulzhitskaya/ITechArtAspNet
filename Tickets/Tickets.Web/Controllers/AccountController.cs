using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Tickets.BLL.Core.Identity;
using Tickets.DAL.Models.Entities;
using Tickets.Web.Models;

namespace Tickets.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(UserManager userManager, RoleManager roleManager, IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationManager = authManager;
        }
        //Get
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            SetInitialData();
            if (ModelState.IsValid)
            {
                User user = _userManager.Find(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    ClaimsIdentity claims = _userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    _authenticationManager.SignOut();
                    _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claims);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            SetInitialData();
            if (ModelState.IsValid)
            {
                var user = new User {Email = model.Email, FirstName = model.FirstName, LastName = model.LastName,UserName = model.Email};
                var result = _userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "user");
                    return View("SuccessRegister");
                }
                ModelState.AddModelError("",result.Errors.FirstOrDefault());
            }
            return View(model);
        }
        [Authorize]
        public ActionResult UserAccount()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            var userModel = new UserViewModel {Email = user.Email,FirstName = user.FirstName,LastName = user.LastName,UserId = user.Id};
            return View("UserArea", userModel);
        }

        private void SetInitialData()
        {
            _roleManager.Create(new Role { Name = "admin" });
            _roleManager.Create(new Role {Name = "user"});
            var admin = new User {Email = "admin@gmail.com",FirstName = "Ivan",LastName = "Ivanov",UserName = "admin@gmail.com" };
            var result = _userManager.Create(admin, "1234567");
            if (result.Succeeded)
            {
                _userManager.AddToRole(admin.Id, "admin");
                _userManager.AddToRole(admin.Id, "user");
            }
        }
    }
}