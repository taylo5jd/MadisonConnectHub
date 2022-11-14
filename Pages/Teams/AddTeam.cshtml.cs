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
            NewTeam = new Team();
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
            NewTeam.TeamName = HttpContext.Session.GetString("username");
            NewTeam.ProjectID = (int)HttpContext.Session.GetInt32("projid");

            int prfid = DBUserClass.UserIDReader(HttpContext.Session.GetString("username"));
            int tid = DBTeamClass.TeamIDReader();

            DBTeamClass.InsertTeam(NewTeam);

            DBAddToTeamClass.InsertUserTeamComposition(prfid, tid);
            RedirectToPage("/Project/Index");

        }


    }
}

