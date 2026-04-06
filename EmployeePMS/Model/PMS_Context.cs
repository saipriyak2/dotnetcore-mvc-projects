using CompetencyPMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DL_CompetencyPMS.Model
{
    public class PMS_Context:DbContext
    {
        public DbSet<RoleInfo> roles { get; set; }
        public DbSet<UserInfo> users { get; set; }
        public DbSet<AssessmentInfo> assessments { get; set; }
        public DbSet<DesignationInfo> designations { get; set; }
        public DbSet<CompetencyInfo> competencies { get; set; }
        public DbSet<CaseStudySolutionInfo> casestudies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SAIPRIYA_K\\PRACTICEPRIYA;Database=Competency_PMS;Integrated Security=true;TrustServerCertificate=True;");
            }
            base.OnConfiguring(optionsBuilder);


        }
    }
}
