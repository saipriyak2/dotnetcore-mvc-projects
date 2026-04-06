using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.Repository;
using DL_CompetencyPMS.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_CompetencyPMS.BLDataManipulation
{
    public class BL_UserManagementData
    {
        
        private readonly IUserManagementRepository _repo;
        private readonly IRoleRepository _rolerepo;

        public BL_UserManagementData(IUserManagementRepository repo, IRoleRepository rolerepo)
        {
            _repo = repo;
            _rolerepo = rolerepo;
        }


        public List<UserInfo> GetAllUsers()
        {
            try
            {
                List<UserInfo> lst = _repo.GetAllUsersFromDB();
                return lst;
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        public List<RoleInfo> GetAllRoles()
        {
            try
            {
                List<RoleInfo> lst = _rolerepo.GetAllRoleDetails();
                return lst;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }

        public string Login(LoginViewModel loginVM)
        {
            string msg = string.Empty;
            try
            {
                _repo.Login(loginVM);
                msg = "success";
            }
            catch(Exception ex)
            {
                msg = "failed";
            }
            return msg;
        }

        public string Register(RegisterViewModel registerVM)
        {
            string msg = string.Empty;
            try
            {
                _repo.Register(registerVM);
                msg = "success";
            }
            catch (Exception ex)
            {
                msg = "failed";
            }
            return msg;
        }

        public string InsertRoleData(RoleInfo rl)
        {
            string msg = string.Empty;
            try
            {

                _rolerepo.InsertRoleData(rl);

                msg = "success";

            }
            catch (Exception ex)
            {
                msg = "failed";
            }

            return msg;
        }

        public RoleInfo GetRoleOnRoleID(int roleid)
        {
            try
            {
                return _rolerepo.GetRoleOnRoleID(roleid);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return null; // or handle gracefully
            }
    
        }
        public string UpdateRoleData(RoleInfo r)
        {
            try
            {
                _rolerepo.UpdateRoleData(r);
                return "success";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }

        public bool UChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                return _repo.uChangePassword(username, oldPassword, newPassword);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return false;
            }
        }


    }

}

