using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Teams
{
    public class AddTeamModel : PageModel
    {
        [BindProperty]
        public Team NewTeam { get; set; }
        [BindProperty]
        public List<ProjectProfile> ProjectList { get; set; }
        public AddTeamModel()
        {
            ProjectList = new List<ProjectProfile>();   
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
            SqlDataReader projectReader = DBProjectClass.ProjectReader();

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


                });

            }
            projectReader.Close();
        }

        public IActionResult OnPost()
        {
            DBTeamClass.InsertTeam(NewTeam);

            int prfid = DBUserClass.UserIDReader(HttpContext.Session.GetString("username"));
            int tid = DBTeamClass.TeamIDReader();

            DBAddToTeamClass.InsertUserTeamComposition(prfid,tid );

            return RedirectToPage("Index");
        }

        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                SqlDataReader projectReader = DBProjectClass.ProjectReader();

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


                    });

                }
                projectReader.Close();

                ModelState.Clear();
                NewTeam.TeamEmail= "TestTeamEmail@gmail.com";
                NewTeam.TeamDescription = "A decent test team";
                NewTeam.TeamName = "TeamViolet";
                NewTeam.TeamMeetingDate = "2023-01-15";
                NewTeam.ProjectID = 2;
            }
            return Page();
        }

    }
}

