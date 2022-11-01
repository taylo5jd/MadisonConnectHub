using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;


namespace lab_1_part_3.Pages.User
{
    public class FileUploadModel : PageModel
    {
        [BindProperty]
        public List<IFormFile> files { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Methods.renamer(formFile.FileName, HttpContext.Session.GetString("username"));
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
            }
            return RedirectToPage("Index");
        }
    }
}
