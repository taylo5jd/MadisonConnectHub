
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab_1_part_3.Pages.User
{
    public class AddUserProfileModel : PageModel
    {
        [BindProperty]
        public UserProfile NewUserProfile { get; set; }
        
        


        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
        }

        public IActionResult OnPost()
        {
            DBUserClass.InsertUserProfile(NewUserProfile);

            return RedirectToPage("Index");
        }
       
        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                NewUserProfile.Username = "je123";
                NewUserProfile.FirstName = "Jeremy";
                NewUserProfile.LastName = "Ezell";
                NewUserProfile.Email = "jz@gmail.com";
                NewUserProfile.UserType = "Professional";
                NewUserProfile.PhoneNumber = "5711231234";
                NewUserProfile.ProfessionalEmail = "jz@jmu.edu";
                NewUserProfile.ProfessionalCompany = "JMU";
                NewUserProfile.FacultyAssociation = null;
            }
            return Page();
        }
        
    }

}

