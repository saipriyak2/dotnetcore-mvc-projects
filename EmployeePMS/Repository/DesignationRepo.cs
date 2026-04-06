using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Repository
{
    public class DesignationRepo:IDesignationRepo
    {
        PMS_Context _context = new PMS_Context();

        public List<DesignationInfo> GetAllDesignations()
        {
            List<DesignationInfo> lst = new List<DesignationInfo>();

            try
            {
                lst = _context.designations.ToList();

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return lst;
        }

        public string InsertDesignationData(DesignationInfo d)
        {
            string msg = string.Empty;
            try
            {
                _context.designations.Add(d);

                _context.SaveChanges();

                msg = "success";

            }
            catch (Exception ex)
            {
                msg = "failed";
            }

            return msg;
        }

        public DesignationInfo GetDesignationOnID(int did)
        {
            DesignationInfo d = new DesignationInfo();

            try
            {
                d = _context.designations.FirstOrDefault(ep => ep.DesgID == did);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return d;
        }


        public string UpdateDesignationData(DesignationInfo d)
        {
            string msg = string.Empty;
            try
            {
                DesignationInfo dExisting = _context.designations.FirstOrDefault(ep => ep.DesgID == d.DesgID);
                if (dExisting != null)
                {
                    dExisting.DesgName = d.DesgName;
                    dExisting.Description = d.Description;
                    dExisting.CreatedDate = d.CreatedDate;


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
    






