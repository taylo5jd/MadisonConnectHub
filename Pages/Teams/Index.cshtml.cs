using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Teams
{
    public class IndexModel : PageModel
    {
        public List<Team> TeamList { get; set; }
        public IndexModel()
        {
            TeamList = new List<Team>();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
              return  RedirectToPage("/DBLogin");
            }


            //called when page first loads
            SqlDataReader teamReader = DBTeamClass.SingleUserTeamReader(HttpContext.Session.GetString("username"));

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
            return Page();
        }
    }
}

