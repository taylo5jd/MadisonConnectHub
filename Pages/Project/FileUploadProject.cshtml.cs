using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab_1_part_3.Pages.Project
{
    public class FileUploadProjectModel : PageModel
    {
        [BindProperty]
        public List<IFormFile> files { get; set; }
        public string projName = "";
        public void OnGet(string projname)
        {
            projName += projname;
        }

           
          
       
            public IActionResult OnPost(string projname)
            {
                var filePaths = new List<string>();
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = Methods.renamer(formFile.FileName, projname);
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
