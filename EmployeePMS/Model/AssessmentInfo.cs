using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Model
{
    [Table("AssessmentDetails")]
    public class AssessmentInfo
    {


        [Key]
        public int AssessmentID { get; set; }
        public string AssessmentName { get; set; }
        public DateTime AssmtDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        //public List<SelectListItem> RoleList { get; set; }


    }
}
