using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace lab_1_part_3.Pages.Project
{
    public class IndexModel : PageModel
    {
        public List<ProjectProfile> ProjectList { get; set; }
        public IndexModel()
        {
            ProjectList = new List<ProjectProfile>();
        }

        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/DBLogin");
            }




            //called when page first loads
            SqlDataReader projectReader = DBProjectClass.SingleUserProjectReader(HttpContext.Session.GetString("username"));

            while (projectReader.Read())
            {
                ProjectList.Add(new ProjectProfile
                {
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProfileID = Int32.Parse(projectReader["ProfileID"].ToString()),
                    ProjectDescription = projectReader["Project_Description"].ToString(),
                    ProjectType = projectReader["Project_Type"].ToString(),
                    ProjectName = projectReader["Project_Name"].ToString(),
                    ProjectOwnerEmail = projectReader["Project_Owner_Email"].ToString(),
                    ProjectBeginDate = (projectReader["Project_Begin_Date"].ToString()),
                    ProjectMissionStatement = projectReader["Project_Mission_Statement"].ToString(),
                    
                });

            }
            projectReader.Close();
            return Page();
        }
    }
}




