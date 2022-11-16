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
        public List<UserProfile> UserList2 { get; set; }
        [BindProperty]
        public int projID { get; set; }
        public RecommendedUsersModel()
        {
            UserList = new List<UserProfile>();
            UserList2 = new List<UserProfile>();
            
        }
        public void OnGet(int ProjectID)
        {
             projID = ProjectID;
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

            int test = 0;
            bool flag = false;

            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList2.Count == 0)
                {
                    UserList2.Add(UserList[i]);
                    continue;
                }

                for (int k = 0; k < UserList2.Count; k++)
                {
                    if ((UserList[i].Username.Equals(UserList2[k].Username)) )
                    {
                        flag = true;
                        break;
                        
                    }

                }
                if(flag == false)
                {
                    UserList2.Add(UserList[i]);
                }
                else
                {
                    flag = false;
                }
            }

        }
    }
}
