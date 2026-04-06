using DL_CompetencyPMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Abstract
{
    public interface ICompetencyRepo
    {
        List<CompetencyInfo> GetAllCompetensies();
        string InsertCompetencyData(CompetencyInfo c);
        CompetencyInfo GetCompetencyOnID(int cid);
        string UpdateCompetencyData(CompetencyInfo c);
    }
}
