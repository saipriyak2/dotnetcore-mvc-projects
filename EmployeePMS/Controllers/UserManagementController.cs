
using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.Repository;
using DL_CompetencyPMS.ViewModel;
using DL_CompetencyPMS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CompetencyPMS.Controllers
{
    //[Authorize]
    public class UserManagementController : Controller
    {
        IUserManagementRepository _repo = new UserManagementRepository();
        IRoleRepository _rolerepo = new RoleRepository();
        IDesignationRepo _desigrepo = new DesignationRepo();

        [HttpGet]
        public IActionResult Index()
        {
            List<UserInfo> lst = _repo.GetAllUsersFromDB();

            return View(lst);


        }

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginvm)
        {
            //if (!ModelState.IsValid)
            //    return View(loginvm);

            //var user = _blUser.Login(loginvm);

            //if(user == null)
            //{
            //    ModelState.AddModelError("", "Invalid login credentials");
            //    return View(loginvm);
            //}

            //var role = _blUser.GetAllRole().FirstOrDefault(r => r.RoleID == user.RoleID);
            //string roleName = role?.RoleName ?? "Unknown";

            //HttpContext.Session.SetString("LoginUserID", user.UserID);
            //HttpContext.Session.SetString("LoginRoleID", user.RoleID);
            //HttpContext.Session.SetString("LoginRoleName", roleName);

            //var claims = new List<Claim>
            //{
            //new Claim(ClaimTypes.Name,user.UserID),
            //new Claim(ClaimTypes.NameIdentifier,user.UserID),
            //new Claim(ClaimTypes.Role,roleName)

            // };

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(identity);

            // await HttpContext.SignInAsync(principal);
            //return RedirectToAction("Index", "Home");

            string msg = _repo.Login(loginvm);
            if (msg == "success")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Data not saved, try again latter";
            }

            return View(loginvm);

        }

        // [Authorize(Roles = "HRM")]
        [HttpGet]
        public IActionResult CreateRegister()
        {
            var roles = _rolerepo.GetAllRoleDetails()
               .Select(r => new
               {
                   Id = r.RoleID,
                   DisplayText = $"{r.RoleID} - {r.RoleName}"
               })
               .ToList();
            ViewBag.RoleId = new SelectList(roles, "Id", "DisplayText");

            var designations = _desigrepo.GetAllDesignations()
                .Select(d => new
                {
                    Id = d.DesgID,
                    DisplayText = $"{d.DesgName}"
                })
                .ToList();

            ViewBag.DesignationId = new SelectList(designations, "Id", "DisplayText");


            return View();
        }
        //[Authorize(Roles = "HRM")]
        [HttpPost]
        public IActionResult CreateRegister(RegisterViewModel registervm)
        {
            string msg = _repo.Register(registervm);
            if (msg == "success")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Data not saved, try again latter";
            }

            return View(registervm);

        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            UserInfo u = _repo.GetUserOnID(Convert.ToInt32(id));
            return View(u);
        }


        [HttpPost]
        public IActionResult Edit(UserInfo u)
        {
            string msg = _repo.UpdateUserData(u);
            if (msg == "success")
            {
                return RedirectToAction("Index");
            }
            else
            {
                string info = "Data not saved, try again latter";
            }

            return View();
        }





        [HttpGet]
        public IActionResult Delete(int id)
        {
            string msg = _repo.DeleteUserData(id);
            if (msg == "success")
            {
                return RedirectToAction("Index");
            }
            else
            {
                string info = "Data not saved, try again latter";
            }
            return View();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,PM,Candidate,HRM,SME" )]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,PM,Candidate,HRM,SME")]
        public IActionResult ChangePassword(ChangePasswordVM model)
        {
            //if (!ModelState.IsVa
            //    return View(model);

            bool success = _repo.uChangePassword(model.UserID, model.OldPassword, model.NewPassword);

            if (success)
            {
                ViewBag.Message = "Password changed successfully.";
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Username or old password is incorrect.");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            UserInfo u_ = _repo.GetUserOnID(Convert.ToInt32(id));
            return View(u_);
        }



        //public IActionResult ChangePassword(string username,string oldPassword,string newPassword,string confirmPassword)
        //{
        //    if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
        //    {
        //        ModelState.AddModelError("", "All fields are required");
        //        return View();
        //    }

        //    if(newPassword != oldPassword)
        //    {
        //        ModelState.AddModelError("", "new password and confirm password do not match");
        //        return View();
        //    }

        //    bool success = _repo.uChangePassword(username, oldPassword, newPassword);
        //    if(success)
        //    {
        //        ViewBag.Message = "Password changed successfully";
        //        return View();
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("","oldpassword is incorrect");
        //        return View();
        //    }















    }
}
