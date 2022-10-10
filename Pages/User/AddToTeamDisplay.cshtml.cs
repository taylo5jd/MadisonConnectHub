using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.User
{
    public class AddToTeamDisplayModel : PageModel
    {
        [BindProperty]
        public int teamidint { get; set; }

        public void OnGet(string teamid)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }

            if (teamid != null)
                {

                    int teamIDInt = Int32.Parse(teamid);

                    DBAddToTeamClass.InsertUserTeamComposition(Int32.Parse(HttpContext.Session.GetString("profileid").ToString()), teamIDInt);
                }
            }
        }
    }
