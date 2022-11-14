using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class ProjectProfileModel : PageModel
    {
        public List<ProjectProfile> ProjectList { get; set; }
        [BindProperty]
        public List<IFormFile> files { get; set; }

        public ProjectProfileModel()
        {
            ProjectList = new List<ProjectProfile>();
        }

        public IActionResult OnGet(int ProjectID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/DBLogin");
            }


            //called when page first loads
            SqlDataReader ProjectReader = DBProjectClass.SingleProjectReader(ProjectID);

            while (ProjectReader.Read())
            {
                ProjectList.Add(new ProjectProfile
                {
                    ProjectID = Int32.Parse(ProjectReader["ProjectID"].ToString()),
                    ProjectDescription = ProjectReader["ProjectDescription"].ToString(),
                    ProjectType = ProjectReader["ProjectType"].ToString(),
                    ProjectName = ProjectReader["ProjectName"].ToString(),
                    ProjectOwnerEmail = ProjectReader["ProjectOwnerEmail"].ToString(),
                    ProjectBeginDate = ProjectReader["ProjectBeginDate"].ToString(),
                    ProjectMissionStatement = ProjectReader["ProjectMissionStatement"].ToString(),
                    ProfileID = Int32.Parse(ProjectReader["ProfileID"].ToString()),
                    DesiredSkills = ProjectReader["DesiredSkills"].ToString(),
                    Category = ProjectReader["Category"].ToString()
                });

                ProjectReader.Close();
            }
            return Page();
        }
    }
}
        
 



