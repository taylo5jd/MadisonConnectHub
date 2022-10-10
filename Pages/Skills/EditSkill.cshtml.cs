using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Skills
{
    public class EditSkillModel : PageModel
    {
        [BindProperty]
        public Skill SkillToUpdate { get; set; }

        public EditSkillModel()
        {
            SkillToUpdate = new Skill();
        }

        public void OnGet(int SkillID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
            SqlDataReader singleSkill = DBSkillClass.SingleSkillReader(SkillID);

            while (singleSkill.Read())
            {
                SkillToUpdate.SkillID = SkillID;
                SkillToUpdate.SkillType = singleSkill["Skill_Type"].ToString();
                SkillToUpdate.SkillDescription = singleSkill["Skill_Description"].ToString();


            }

            singleSkill.Close();
        }

        public IActionResult OnPost()
        {
            DBSkillClass.UpdateSkill(SkillToUpdate);

            return RedirectToPage("Index");
        }
    }
}