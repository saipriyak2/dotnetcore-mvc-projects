using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Repository
{
    public class CompetencyRepo:ICompetencyRepo
    {
        PMS_Context _context = new PMS_Context();

        public List<CompetencyInfo> GetAllCompetensies()
        {
            List<CompetencyInfo> lst = new List<CompetencyInfo>();

            try
            {
                lst = _context.competencies.ToList();

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return lst;
        }

        public string InsertCompetencyData(CompetencyInfo c)
        {
            string msg = string.Empty;
            try
            {
                _context.competencies.Add(c);

                _context.SaveChanges();

                msg = "success";

            }
            catch (Exception ex)
            {
                msg = "failed";
            }

            return msg;
        }

        public CompetencyInfo GetCompetencyOnID(int cid)
        {
            CompetencyInfo c = new CompetencyInfo();

            try
            {
                c = _context.competencies.FirstOrDefault(ep => ep.CompID == cid);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return c;
        }


        public string UpdateCompetencyData(CompetencyInfo c)
        {
            string msg = string.Empty;
            try
            {
                CompetencyInfo cExisting = _context.competencies.FirstOrDefault(ep => ep.CompID == c.CompID);
                if (cExisting != null)
                {
                    cExisting.CompName = c.CompName;
                    cExisting.Description = c.Description;
                    cExisting.CreatedDate = c.CreatedDate;
                    

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
