using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using lab_1_part_3.Pages.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Teams
{
    public class EditTeamModel : PageModel
    {
        
            [BindProperty]
            public Team TeamToUpdate { get; set; }
            [BindProperty]
             public List<ProjectProfile> ProjectList { get; set; }
        public EditTeamModel()
            {
                TeamToUpdate = new Team();
            ProjectList = new List<ProjectProfile>();
            }

            public void OnGet(int TeamID)
            {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
            SqlDataReader singleTeam = DBTeamClass.SingleTeamReader(TeamID);

                while (singleTeam.Read())
                {
                    TeamToUpdate.TeamID = TeamID;
                TeamToUpdate.ProjectID = Int32.Parse(singleTeam["ProjectID"].ToString());
                TeamToUpdate.TeamEmail = singleTeam["Team_Email"].ToString();
                TeamToUpdate.TeamDescription = singleTeam["Team_Description"].ToString();
                TeamToUpdate.TeamName = singleTeam["Team_Name"].ToString();
                   TeamToUpdate.TeamMeetingDate = singleTeam["Team_Meeting_Date"].ToString();


                }

                singleTeam.Close();
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
                DBTeamClass.UpdateTeam(TeamToUpdate);

                return RedirectToPage("Index");
            }
        }
    }