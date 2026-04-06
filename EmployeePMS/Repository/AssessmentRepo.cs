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
    public class AssessmentRepo:IAssessmentRepo
    {
        PMS_Context _context = new PMS_Context();

        public List<AssessmentInfo> GetAllAssessments()
        {
            List<AssessmentInfo> lst = new List<AssessmentInfo>();

            try
            {
                lst = _context.assessments.ToList();

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return lst;
        }

        public string InsertAssessmentData(AssessmentInfo a)
        {
            string msg = string.Empty;
            try
            {
                _context.assessments.Add(a);

                _context.SaveChanges();

                msg = "success";

            }
            catch (Exception ex)
            {
                msg = "failed";
            }

            return msg;
        }

        public AssessmentInfo GetAssessmentOnID(int id)
        
        {
            AssessmentInfo a = new AssessmentInfo();

            try
            {
                a = _context.assessments.FirstOrDefault(ep => ep.AssessmentID == id);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }


            return a;
        }


        public string UpdateAssessmentData(AssessmentInfo a)
        {
            string msg = string.Empty;
            try
            {
                AssessmentInfo aExisting = _context.assessments.FirstOrDefault(ep => ep.AssessmentID == a.AssessmentID);
                if (aExisting != null)
                {
                    aExisting.AssessmentName = a.AssessmentName;
                    aExisting.Description = a.Description;
                    aExisting.AssmtDate = a.AssmtDate;
                    aExisting.IsActive = a.IsActive;

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
       
                  
        public string DeleteAssessmentData(int a)
        {
            string msg = string.Empty;
            try
            {
                AssessmentInfo aExisting = _context.assessments.FirstOrDefault(ep => ep.AssessmentID == a);
                if (aExisting != null)
                {
                    _context.assessments.Remove(aExisting);
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
