using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab_1_part_3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

      
            public IActionResult OnGet()
            {
               if (HttpContext.Session.GetString("username") == null)
                {
               
                }

                return Page();

            }
    }
}