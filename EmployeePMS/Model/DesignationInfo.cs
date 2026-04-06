using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Model
{
    [Table("DesignationDetails")]
    public class DesignationInfo
    {
        [Key]
        public int DesgID { get; set; }
        public string DesgName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
