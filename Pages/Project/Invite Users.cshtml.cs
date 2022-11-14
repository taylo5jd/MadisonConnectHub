using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class Invite_UsersModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public List<UserProfile> UserList { get; set; }
        public List<Skill> SkillList { get; set; }
        
        public int ProjID = 0;
        public Invite_UsersModel()
        {
            UserList = new List<UserProfile>();
            SkillList = new List<Skill>();
        }

        public IActionResult OnGet(int ProjectID, int ProfileID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/DBLogin");
            }
            ProjID = ProjectID;
            int Requester = DBUserClass.UserIDReader(HttpContext.Session.GetString("username"));

            if(ProjectID != 0 && ProfileID != 0)
            {

                DBJoinRequest.CreateJoinRequest(Requester, ProjID, ProfileID, "n", "Invite");
            }



            //called when page first loads
            SqlDataReader userReader = DBUserClass.MultipleUserReader();



            while (userReader.Read())
            {
                UserList.Add(new UserProfile
                {
                    Username = userReader["Username"].ToString(),
                    ProfileID = Int32.Parse(userReader["ProfileID"].ToString()),
                    FirstName = userReader["FirstName"].ToString(),
                    LastName = userReader["LastName"].ToString(),
                    Email = userReader["Email"].ToString(),
                    UserType = userReader["UserType"].ToString(),
                    PhoneNumber = userReader["PhoneNumber"].ToString(),
                    ProfessionalEmail = userReader["Professional_Email"].ToString(),
                    ProfessionalCompany = userReader["Professional_Company"].ToString(),
                    FacultyAssociation = userReader["Faculty_Association"].ToString(),
                    VideoIntroduction = userReader["Video_Introduction"].ToString(),
                    LinkedIn = userReader["LinkedIn"].ToString()


                });

            }

            userReader.Close();

            return Page();
        }
    }
}
