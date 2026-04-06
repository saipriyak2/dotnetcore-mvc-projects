using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.ViewModel;
using DL_CompetencyPMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Abstract
{
    public interface IUserManagementRepository
    {
        List<UserInfo> GetAllUsersFromDB();

        string Login(LoginViewModel loginvm);
       string Register(RegisterViewModel registervm);
        //bool UpdateChangePassword(ChangePasswordVM cpvm);
         //bool VerifyPassword(string inputPassword, string storeHash);
        //string HashPassword(string password);
        UserInfo GetUserByID(string id);
        //bool UpdateChangePassword(ChangePasswordVM changepwdvm);

        UserInfo GetUserOnID(int id);
        bool uChangePassword(string username, string oldPassword, string newPassword);

        //string InsertUserData(UserInfo ur);
        public string UpdateUserData(UserInfo u);
        string DeleteUserData(int u);
    }

}
