using DL_CompetencyPMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Abstract
{
    public interface ICaseStudySolutionRepo
    {
        List<CaseStudySolutionInfo> GetAllCaseStudyDetails();
        string InsertCaseStudyData(CaseStudySolutionInfo ci);
        CaseStudySolutionInfo GetCSOnCSID(int csid);
        string UpdateCSData(CaseStudySolutionInfo c);
        string DeleteCSData(int c);
    }
}
