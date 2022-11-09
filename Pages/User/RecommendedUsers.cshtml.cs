using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.User
{
    public class RecommendedUsersModel : PageModel
    {
        public List<UserProfile> UserList { get; set; }
        public RecommendedUsersModel()
        {
            UserList = new List<UserProfile>();
            
        }
        public void OnGet(int ProjectID)
        {
            
            SqlDataReader tempReader = DBClasses.DBDesiredSkills.UserReader(ProjectID);
            while (tempReader.Read())
            {
                SqlDataReader userReader = DBUserClass.SingleUserReader(Int32.Parse(tempReader["ProfileID"].ToString()));



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

              
            }
            
        }
    }
}
