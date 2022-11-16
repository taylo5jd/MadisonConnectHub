using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.User
{
    public class ViewUser : PageModel
    {
        public List<UserProfile> UserList { get; set; }
        public List<Skill> SkillList { get; set; }
        [BindProperty]
        public List<IFormFile> files { get; set; }
        
        public ViewUser()
        {
            UserList = new List<UserProfile>();
            SkillList = new List<Skill>();
        }

        public IActionResult OnGet(string username)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToPage("/DBLogin");
            }

            
            //called when page first loads
            SqlDataReader userReader = DBUserClass.UserReader(username);
            SqlDataReader skillReader = DBSkillClass.SingleUserSkillReader(username);

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
                    LinkedIn = userReader["LinkedIn"].ToString(),
                    VideoIntroduction = userReader["Video_Introduction"].ToString(),
                    Availability = userReader["Availability"].ToString(),
                    Bio = userReader["Bio"].ToString(),
                    Passions = userReader["Passions"].ToString(),
                    Personality = userReader["Personality"].ToString(),

                });

            } while (skillReader.Read())
            {
                SkillList.Add(new Skill
                {
                    SkillID = Int32.Parse(skillReader["SkillID"].ToString()),
                    SkillType = skillReader["Skill_Type"].ToString(),
                    SkillDescription = skillReader["Skill_Description"].ToString(),
                });

            }
            userReader.Close();
            skillReader.Close();
            return Page();
        }


    }
}
