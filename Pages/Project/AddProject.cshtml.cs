using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Globalization;

namespace lab_1_part_3.Pages.Project
{
    public class AddProjectModel : PageModel
    {
        
            [BindProperty]
            public ProjectProfile NewProjectProfile { get; set; }
            [BindProperty]
        public List<UserProfile> UserList { get; set; }
        public AddProjectModel()
        {
            UserList = new List<UserProfile>();
        }
        public void OnGet()
            {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }

            SqlDataReader userReader = DBUserClass.MultipleUserReader();

            while (userReader.Read())
            {
                UserList.Add(new UserProfile
                {
                    ProfileID = Int32.Parse(userReader["ProfileID"].ToString()),
                    Username = userReader["Username"].ToString(),
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

        }

        public IActionResult OnPost()
            {
            //var dt = NewProjectProfile.ProjectBeginDate.Date;
            //  NewProjectProfile.ProjectBeginDate = NewProjectProfile.ProjectBeginDate.Date;

           // 2022-09-27
            //var dt = NewProjectProfile.ProjectBeginDate.ToString().Substring(0, 10);
            //Convert.ToDateTime(dt);
           // NewProjectProfile.ProjectBeginDate = dt;
            DBProjectClass.InsertProject(NewProjectProfile);

                return RedirectToPage("/Teams/AddTeam");
            }
        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                SqlDataReader userReader = DBUserClass.MultipleUserReader();

                while (userReader.Read())
                {
                    UserList.Add(new UserProfile
                    {
                        ProfileID = Int32.Parse(userReader["ProfileID"].ToString()),
                        Username = userReader["Username"].ToString(),
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

                ModelState.Clear();
                NewProjectProfile.ProfileID = 5;
                NewProjectProfile.ProjectDescription = "An effort to clean air";
                NewProjectProfile.ProjectType = "Environmental";
                NewProjectProfile.ProjectName = "Air Sustainability";
                NewProjectProfile.ProjectOwnerEmail = "js@dukes.jmu";
                NewProjectProfile.ProjectBeginDate = "2022-12-04";
                NewProjectProfile.ProjectMissionStatement = "We need clean air for the valley.";


            }
            return Page();
        }
    }
    }

