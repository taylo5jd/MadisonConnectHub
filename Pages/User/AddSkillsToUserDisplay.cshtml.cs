using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab_1_part_3.Pages.User
{
    public class AddSkillsToUserDisplayModel : PageModel
    {
        public void OnGet(string skillid)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }

            if (skillid != null)
            {

                int teamIDInt = Int32.Parse(skillid);

                DBAddSkillToUser.InsertUserProfile_Skills(Int32.Parse(HttpContext.Session.GetString("profileid").ToString()), teamIDInt);
            }
        }
    }
}