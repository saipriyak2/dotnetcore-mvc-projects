using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.ViewModel;
using DL_CompetencyPMS.ViewModel;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static DL_CompetencyPMS.Repository.UserManagementRepository;

namespace DL_CompetencyPMS.Repository
{
    // [Authorize(Roles = "Admin")]
    public class UserManagementRepository : IUserManagementRepository
    {
        PMS_Context _context = new PMS_Context();

        public List<UserInfo> GetAllUsersFromDB()
        {
            List<UserInfo> lst = new List<UserInfo>();

            try
            {
                lst = _context.users.ToList();

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return lst;
        }


        public string Login(LoginViewModel loginVM)
        {
            string msg = string.Empty;

            try
            {
                var user = _context.users
                    .FirstOrDefault(u => u.UserID == loginVM.UserID && u.Password == loginVM.Password);

                if (user != null)
                {
                    msg = "success";
                }
                else
                {
                    msg = "invalid credentials";
                }

                return msg;

            }
            catch (Exception ex)
            {
                msg = "error";
            }

            return msg;
        }
        public string Register(RegisterViewModel registerVM)
        {
            string msg = string.Empty;

            try
            {
                var user = new UserInfo
                {
                    UserID = registerVM.UserID,
                    Password = registerVM.Password,
                    Name = registerVM.Name,
                    EmpID = registerVM.EmpID,
                    Email = registerVM.Email,
                    RoleID = registerVM.RoleID,
                    IsActive = registerVM.IsActive,
                };
                _context.users.Add(user);
                _context.SaveChanges();
                msg = "success";
            }
            catch (Exception ex)
            {
                msg = "failed";
            }

            return msg;
        }

        public UserInfo GetUserOnID(int id)
        {
            UserInfo u = new UserInfo();

            try
            {
                u = _context.users.FirstOrDefault(ep => ep.ID == id);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return u;
        }

        public string UpdateUserData(UserInfo u)
        {
            string msg = string.Empty;
            try
            {
                UserInfo uExisting = _context.users.FirstOrDefault(ep => ep.ID == u.ID);
                if (uExisting != null)
                {
                    uExisting.UserID = u.UserID;
                    uExisting.Name = u.Name;
                    uExisting.Password = u.Password;
                    uExisting.EmpID = u.EmpID;
                    uExisting.Email = u.Email;
                    uExisting.RoleID = u.RoleID;
                    uExisting.DesigID = u.DesigID;
                    uExisting.IsActive = u.IsActive;
                   
                }

                _context.SaveChanges();

                msg = "success";

            }
            catch (Exception ex)
            {
                msg = "failed";
            }

            return msg;
        }
                


        public string DeleteUserData(int ui)
        {
            string msg = string.Empty;
            try
            {
                UserInfo userExisting = _context.users.FirstOrDefault(ep => ep.ID == ui);
                if (userExisting != null)
                {
                    _context.users.Remove(userExisting);
                }

                _context.SaveChanges();

                msg = "success";

            }
            catch (Exception ex)
            {
                msg = "failed";
            }

            return msg;
        }

        public bool uChangePassword(string username, string oldPassword, string newPassword)
        {
            // Find user by username
            var user = _context.users.FirstOrDefault(u => u.UserID == username && u.Password == oldPassword);
            if (user == null)
                return false;

            // Check if old password matches
            if (user.Password != oldPassword)
                return false;

            // Update password
            user.Password = newPassword;

            _context.SaveChanges(); // Commit to DB
            return true;
        }










        private string HashPassword(string password)
        {
            //Replace this with secure password hashing
            return password;
        }
        public UserInfo GetUserByID(string id)
        {
            return _context.users.Find(id);
        }

        //List<string> GetUserRoles(string userId)
        //{
        //    return "";
        //}

        











    }


}



    



