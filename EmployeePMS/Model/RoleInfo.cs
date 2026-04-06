using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using MVCCore_Dropdown_ChkBxList.Models;
//using System.Diagnostics.Metrics;

namespace CompetencyPMS.Models
{
    [Table("RoleDetails")]
    public class RoleInfo
    {
       
        
            [Key]
            public int RoleID { get; set; }
            public string RoleName { get; set; }
            public string Description { get; set; }
            public DateTime CreatedDate { get; set; }
        //public List<SelectListItem> RoleList { get; set; }


    }
        
    }

