using DL_CompetencyPMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Abstract
{
    public interface IAssessmentRepo
    {
        List<AssessmentInfo> GetAllAssessments();
        string InsertAssessmentData(AssessmentInfo a);
        AssessmentInfo GetAssessmentOnID(int aid);
        string UpdateAssessmentData(AssessmentInfo a);
        string DeleteAssessmentData(int a);
    }
}
