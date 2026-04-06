using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DL_CompetencyPMS.Model
{
    [Table("UserDetails")]
    public class UserInfo
    {
       
    
            [Key]
            public int ID { get; set; }
            public string UserID { get; set; }
            public string Password{ get; set; }
             public string Name { get; set; }
            public string EmpID { get; set; }
            public string Email { get; set; }
            public int RoleID { get; set; }
        public int DesigID { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime  CreatedDate { get; set; }
           public DateTime ModifiedBy { get; set; }
           public DateTime ModifiedDate { get; set; }
            public bool IsActive { get; set; }
            public DateTime PasswordChangeDate { get; set; }

    }
}

