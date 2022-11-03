using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace lab_1_part_3.Pages.User
{
    public class EditUserModel : PageModel
    {
        [BindProperty]
        public UserProfile UserToUpdate { get; set; }
        [BindProperty]
        public string PRFID { get; private set; }
        public object Public { get; private set; }
     

        public EditUserModel()
        {
            UserToUpdate = new UserProfile();
        }

        public void OnGet(int ProfileID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
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

            }

            
            singleUser.Close();

            HttpContext.Session.SetString("profileid",ProfileID.ToString());

        }

        public IActionResult OnPost()
        {
            DBUserClass.UpdateUserProfile(UserToUpdate);

            return RedirectToPage("Index");

           
           
        }
    }
}