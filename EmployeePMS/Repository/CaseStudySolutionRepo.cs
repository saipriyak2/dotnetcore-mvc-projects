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
    public class CaseStudySolutionRepo: ICaseStudySolutionRepo
    {
            PMS_Context _context = new PMS_Context();
            public List<CaseStudySolutionInfo> GetAllCaseStudyDetails()
            {
                List<CaseStudySolutionInfo> lst = new List<CaseStudySolutionInfo>();

                try
                {
                    lst = _context.casestudies.ToList();

                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }


                return lst;
            }

            public string InsertCaseStudyData(CaseStudySolutionInfo ci)
            {
                string msg = string.Empty;
                try
                {
                    _context.casestudies.Add(ci);

                    _context.SaveChanges();

                    msg = "success";

                }
                catch (Exception ex)
                {
                    msg = "failed";
                }

                return msg;
            }

            public CaseStudySolutionInfo GetCSOnCSID(int csid)
            {
            CaseStudySolutionInfo c = new CaseStudySolutionInfo();

                try
                {
                    c = _context.casestudies.FirstOrDefault(ep => ep.CSID == csid);
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }


                return c;
            }
            public string UpdateCSData(CaseStudySolutionInfo ci)
            {
                string msg = string.Empty;
                try
                {
                CaseStudySolutionInfo cExisting = _context.casestudies.FirstOrDefault(ep => ep.CSID == ci.CSID);
                    if (cExisting != null)
                    {
                        cExisting.CaseStudy = ci.CaseStudy;
                        cExisting.AssessmentID = ci.AssessmentID;
                        cExisting.soln1 = ci.soln1;
                        cExisting.Comp1 = ci.Comp1;
                        cExisting.soln2 = ci.soln2;
                        cExisting.Comp2 = ci.Comp2;
                        cExisting.soln3 = ci.soln3;
                        cExisting.Comp3 = ci.Comp3;
                        cExisting.soln4 = ci.soln4;
                        cExisting.Comp4 = ci.Comp4;
                        cExisting.CreatedBy = ci.CreatedBy;
                        cExisting.IsReviewed = ci.IsReviewed;

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

        public string DeleteCSData(int c)
        {
            string msg = string.Empty;
            try
            {
                CaseStudySolutionInfo csExisting = _context.casestudies.FirstOrDefault(ep => ep.CSID == c);
                if (csExisting != null)
                {
                    _context.casestudies.Remove(csExisting);
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

