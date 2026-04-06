using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_CompetencyPMS.Model
{
    [Table("CaseStudySolutionDetails")]
    public class CaseStudySolutionInfo
    {
        [Key]

        public int CSID { get; set; }
        public string CaseStudy { get; set; }

        public int AssessmentID { get; set; }
        public string soln1 { get; set; }
        public int Comp1 { get; set; }
        public string soln2 { get; set; }
        public int Comp2 { get; set; }
        public string soln3 { get; set; }
        public int Comp3 { get; set; }
        public string soln4 { get; set; }
        public int Comp4 { get; set; }
        public DateTime CreatedBy { get; set; }
        public bool IsReviewed { get; set; }


    }
}
