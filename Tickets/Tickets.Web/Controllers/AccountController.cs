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
        private UserManager userManager;
        private RoleManager roleManager;
        private IAuthenticationManager authenticationManager;

        public AccountController(UserManager userManager, RoleManager roleManager, IAuthenticationManager authManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            authenticationManager = authManager;
        }
        //Get
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            SetInitialData();
            if (ModelState.IsValid)
            {
                User user = userManager.Find(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    ClaimsIdentity claims = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignOut();
                    authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claims);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            SetInitialData();
            if (ModelState.IsValid)
            {
                var user = new User {Email = model.Email, FirstName = model.FirstName, LastName = model.LastName,UserName = model.Email};
                var result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "user");
                    return View("SuccessRegister");
                }
                ModelState.AddModelError("",result.Errors.FirstOrDefault());
            }
            return View(model);
        }
        [Authorize]
        public ActionResult UserAccount()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            var userModel = new UserModel {Email = user.Email,FirstName = user.FirstName,LastName = user.LastName,UserId = user.Id};
            return View("UserArea", userModel);
        }

        private void SetInitialData()
        {
            roleManager.Create(new Role { Name = "admin" });
            roleManager.Create(new Role {Name = "user"});
            var admin = new User {Email = "admin@gmail.com",FirstName = "Ivan",LastName = "Ivanov",UserName = "admin@gmail.com" };
            var result = userManager.Create(admin, "1234567");
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, "admin");
                userManager.AddToRole(admin.Id, "user");
            }
        }
    }
}