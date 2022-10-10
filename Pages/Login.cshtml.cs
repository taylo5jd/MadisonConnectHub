using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages
{
    
    public class LoginPageModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        //[BindProperty]

        //public List<string> UsernameList { get; set; }
        [BindProperty]
        public int userCounter { get; set; }
        [BindProperty]
        public List<string> UsernameList { get; set; }


        public void OnGet(string logout)
        {

            if (logout != null)
            {
                HttpContext.Session.Clear();
                ViewData["ErrorMessage"] = "Logged Out Successfully!";
            }
        }

        public IActionResult OnPost()
        {
            userCounter = 0;
            // string[] UsernameList = new string[100];
            UsernameList = new List<string>();
            SqlDataReader usernameReader = DBUserClass.UsernameReader();

            while (usernameReader.Read())
            {
                UsernameList.Add(usernameReader["Username"].ToString());
                userCounter++;

            }

            if (Username != null)
            {

                foreach (string username in UsernameList)
                {

                    if (Username == username)
                    {
                        HttpContext.Session.SetString("username", Username);
                    }



                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Incorrect Username and/or Password";
                return Page();
            }
            return RedirectToPage("Index");
        }


    }
}