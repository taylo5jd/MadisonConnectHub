using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.User
{
    public class AddToTeamModel : PageModel
    {
    
       
        public int Uuid { get; set; }
       

        [BindProperty]
        public Team TeamToJoin  { get; set; }
        public List<Team> TeamList { get; set; }
        

        public AddToTeamModel()
        {
            TeamToJoin = new Team();
            TeamList = new List<Team>();

        }




        public void OnGet(int prf)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
            Uuid = prf;

            int profileIDTest = Int32.Parse(HttpContext.Session.GetString("profileid"));


            SqlDataReader teamReader = DBTeamClass.TeamReader();

            while (teamReader.Read())
            {
                TeamList.Add(new Team
                {
                    TeamID = Int32.Parse(teamReader["TeamID"].ToString()),
                    TeamEmail = teamReader["Team_Email"].ToString(),
                    TeamDescription = teamReader["Team_Description"].ToString(),
                    TeamName = teamReader["Team_Name"].ToString(),
                    TeamMeetingDate = teamReader["Team_Meeting_Date"].ToString(),
                    ProjectID = Int32.Parse(teamReader["ProjectID"].ToString()),






                });
            }
            teamReader.Close();
        }

        public IActionResult OnPost()
        {

            DBAddToTeamClass.InsertUserTeamComposition(Uuid,TeamToJoin.TeamID);

            return RedirectToPage("Index");
        }
    }
}

