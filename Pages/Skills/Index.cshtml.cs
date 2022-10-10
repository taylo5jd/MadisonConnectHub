using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Skills
{
    public class IndexModel : PageModel
    {
        public List<Skill> SkillList { get; set; }
        public IndexModel()
        {
            SkillList = new List<Skill>();
        }

        public IActionResult OnGet()
        {


            if (HttpContext.Session.GetString("username") == null)
            {
               return RedirectToPage("/DBLogin");
            }



            //called when page first loads
            SqlDataReader skillReader = DBSkillClass.SkillReader();

            while (skillReader.Read())
            {
                SkillList.Add(new Skill
                {
                    SkillID = Int32.Parse(skillReader["SkillID"].ToString()),
                    SkillType = skillReader["Skill_Type"].ToString(),
                    SkillDescription = skillReader["Skill_Description"].ToString(),
                   


                });

            }
            skillReader.Close();
            return Page();
        }
    }
}




