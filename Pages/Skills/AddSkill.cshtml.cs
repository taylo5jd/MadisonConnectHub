using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab_1_part_3.Pages.Skills
{
    public class AddSkillModel : PageModel
    {
        [BindProperty]
        public Skill NewSkill { get; set; }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
        }

        public IActionResult OnPost()
        {
            DBSkillClass.InsertSkill(NewSkill);

            return RedirectToPage("Index");
        }
        public IActionResult OnPostPopulateHandler()
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                NewSkill.SkillType = "Charisma";
                NewSkill.SkillDescription = "Being a cool guy/gal";
               
            }
            return Page();
        }

    }
}

