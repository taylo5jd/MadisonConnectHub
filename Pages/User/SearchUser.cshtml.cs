using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.User
{
    public class SearchUserModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty]
        public List<ProjectProfile> ProjectList { get; set; }
        [BindProperty]
        public List<ProjectProfile> SingleProject { get; set; }
        [BindProperty]
        public List<UserProfile> UserList { get; set; }

        public int ProjID { get; set; }

        public int ProfID { get; set; }

        public int ApproverID { get; set; }
        public string StatusFlag { get; set; }

        public SearchUserModel()
        {
            ProjectList = new List<ProjectProfile>();
            SingleProject = new List<ProjectProfile>();
            UserList = new List<UserProfile>();
            
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }


            //called when page first loads
            SqlDataReader userReader = DBUserClass.UserSearch(SearchText);

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

                });

                

                
            }
            return Page();
        }
        }
    }
