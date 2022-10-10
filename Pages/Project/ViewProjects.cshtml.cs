using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class ViewProjectsModel : PageModel
    {
        public List<ProjectProfile> ProjectList { get; set; }
        public ViewProjectsModel()
        {
            ProjectList = new List<ProjectProfile>();
        }

        public void OnGet()
        {



            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }


            //called when page first loads
            SqlDataReader projectReader = DBProjectClass.ProjectReader();

            while (projectReader.Read())
            {
                ProjectList.Add(new ProjectProfile
                {
                    ProjectBeginDate = projectReader["Project_Begin_Date"].ToString(),
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProfileID = Int32.Parse(projectReader["ProfileID"].ToString()),
                    ProjectDescription = projectReader["Project_Description"].ToString(),
                    ProjectType = projectReader["Project_Type"].ToString(),
                    ProjectName = projectReader["Project_Name"].ToString(),
                    ProjectOwnerEmail = projectReader["Project_Owner_Email"].ToString(),
                    ProjectMissionStatement = projectReader["Project_Mission_Statement"].ToString(),


                });

            }
            projectReader.Close();

        }
    }
}


