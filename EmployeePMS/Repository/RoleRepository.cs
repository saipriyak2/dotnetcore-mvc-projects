using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DL_CompetencyPMS.Repository.RoleRepository;

namespace DL_CompetencyPMS.Repository
{
    public class RoleRepository:IRoleRepository
    {
            PMS_Context _context = new PMS_Context();
        public List<RoleInfo> GetAllRoleDetails()
        {
            List<RoleInfo> lst = new List<RoleInfo>();

            try
            {
                lst = _context.roles.ToList();

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return lst;
        }

            public string InsertRoleData(RoleInfo rl)
            {
                string msg = string.Empty;
                try
                {
                    _context.roles.Add(rl);

                    _context.SaveChanges();

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
            RoleInfo r = new RoleInfo();

            try
            {
                r = _context.roles.FirstOrDefault(ep => ep.RoleID == roleid);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return r;
        }
        public string UpdateRoleData(RoleInfo r)
        {
            string msg = string.Empty;
            try
            {
                RoleInfo rExisting = _context.roles.FirstOrDefault(ep => ep.RoleID == r.RoleID);
                if (rExisting != null)
                {
                    rExisting.RoleName = r.RoleName;
                    rExisting.Description = r.Description;
                    rExisting.CreatedDate = r.CreatedDate;
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
    }
}
