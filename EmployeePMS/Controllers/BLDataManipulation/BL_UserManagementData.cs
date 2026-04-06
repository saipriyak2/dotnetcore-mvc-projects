using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.Repository;
using DL_CompetencyPMS.ViewModel;
using DL_CompetencyPMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_CompetencyPMS.BLDataManipulation
{
    public class BL_UserManagementData
    {
        IUserManagementRepository _repo = new UserManagementRepository();

        public List<UserInfo> GetAllUsersFromDB()
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

        internal string InsertRoleData(RoleInfo r)
        {
            throw new NotImplementedException();
        }
    }
}
