using DL_CompetencyPMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Abstract
{
    public interface IDesignationRepo
    {
        List<DesignationInfo> GetAllDesignations();
        string InsertDesignationData(DesignationInfo d);
        DesignationInfo GetDesignationOnID(int did);
        string UpdateDesignationData(DesignationInfo d);
    }
}
