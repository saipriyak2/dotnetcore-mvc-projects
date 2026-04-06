using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Model
{
    [Table("CompetencyDetails")]
    public class CompetencyInfo
    {
        [Key]
        public int CompID { get; set; }
        public string CompName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
