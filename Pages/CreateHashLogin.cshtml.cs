using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab_1_part_3.Pages
{
    public class CreateHashLoginModel : PageModel
    {
        
            [BindProperty]
            public string Username { get; set; }
            [BindProperty]
            public string Password { get; set; }
            [BindProperty]
            public UserProfile NewUser { get; set; }
      
        public void OnGet()
            {
            }

            public IActionResult OnPost()
            {
                // Perform Validation First on Form
                // then...
                if(Username == null || Password == null)
            {
                ViewData["LoginMessage"] = "Please enter a Username and Password";
                return Page();
            }

                DBLoginClass.CreateHashedUser(Username, Password, NewUser);

                // Perform actual logic to check if user was successfully
                //  added in your projects but for demo purposes we can say:

                ViewData["UserCreate"] = "User Successfully Created!";

                return RedirectToPage("Index");
            }
        }
    }
