using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.User
{
    public class EditUserModel : PageModel
    {
        [BindProperty]
        public UserProfile UserToUpdate { get; set; }

        



        public EditUserModel()
        {
            UserToUpdate = new UserProfile();
        }

        public void OnGet(int ProfileID)
        {
            //HttpContext.Session.SetString("flag", "false");
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
           // if (HttpContext.Session.GetString("flag").Equals("false"))
            {
                SqlDataReader singleUser = DBUserClass.SingleUserReader(ProfileID);

                while (singleUser.Read())
                {
                    UserToUpdate.Username = singleUser["Username"].ToString();
                    UserToUpdate.ProfileID = ProfileID;
                    UserToUpdate.FirstName = singleUser["FirstName"].ToString();
                    UserToUpdate.LastName = singleUser["LastName"].ToString();
                    UserToUpdate.Email = singleUser["Email"].ToString();
                    UserToUpdate.UserType = singleUser["UserType"].ToString();
                    UserToUpdate.PhoneNumber = singleUser["PhoneNumber"].ToString();
                    UserToUpdate.ProfessionalEmail = singleUser["Professional_Email"].ToString();
                    UserToUpdate.ProfessionalCompany = singleUser["Professional_Company"].ToString();
                    UserToUpdate.FacultyAssociation = singleUser["Faculty_Association"].ToString();
                    UserToUpdate.LinkedIn = singleUser["LinkedIn"].ToString();
                    UserToUpdate.VideoIntroduction = singleUser["Video_Introduction"].ToString();
                    UserToUpdate.Availability = singleUser["Availability"].ToString();
                    UserToUpdate.Passions = singleUser["Passions"].ToString();
                    UserToUpdate.Personality = singleUser["Personality"].ToString();
                    UserToUpdate.Bio = singleUser["Bio"].ToString();

                }


                singleUser.Close();
            }
            HttpContext.Session.SetString("profileid", ProfileID.ToString());

        }

        public IActionResult OnPost()
        {
            DBUserClass.UpdateUserProfile(UserToUpdate);

            return RedirectToPage("Index");



        }
        //public IActionResult OnPostPopulateHandler()
        //{
        //    HttpContext.Session.SetString("flag", "true");
        //    if (!ModelState.IsValid)
        //    {




        //        UserToUpdate.FirstName = "Jeremy";
        //        UserToUpdate.LastName = "Ezell";
        //        UserToUpdate.Email = "jz@gmail.com";
        //        UserToUpdate.LinkedIn = "https://www.linkedin.com/in/chrisobrienux/";
        //        UserToUpdate.VideoIntroduction = "https://www.youtube.com/watch?v=flKc89EE2bw";
        //        UserToUpdate.Availability = "I am available Tuesdays and Thursdays in the afternoon.";
        //        UserToUpdate.Passions = "I am passionate about the environment and the Valley";
        //        UserToUpdate.Personality = "INFP";
        //        UserToUpdate.Bio = "I am a Professor at JMU who cares for the environment";
                


        //    }
        //    return Page();
        //}
    }
}