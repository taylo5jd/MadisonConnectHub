using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab_1_part_3.Pages
{
    public class DBLoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet(string logout)
        {
            if(logout != null)
            {
                HttpContext.Session.Clear();
                ViewData["ErrorMessage"] = "Logged Out Successfully";
            }

        }

        public IActionResult OnPost()
        {
           
          if (DBLoginClass.StoredProcedure(Username, Password))
       
            {
                HttpContext.Session.SetString("username", Username);
                ViewData["LoginMessage"] = "Login Successful!";
                return RedirectToPage("Index");
    }
            else
            {
                ViewData["LoginMessage"] = "Username and/or Password Incorrect";
                return Page();
}

        }
    }
}
