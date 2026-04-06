using CompetencyPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Abstract
{
    public interface IRoleRepository
    {
        List<RoleInfo> GetAllRoleDetails();
        string InsertRoleData(RoleInfo rl);

        RoleInfo GetRoleOnRoleID(int roleid);
        string UpdateRoleData(RoleInfo r);
    }
}
