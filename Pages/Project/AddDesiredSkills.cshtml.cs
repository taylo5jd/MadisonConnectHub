using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class AddDesiredSkillsModel : PageModel
    {
        public List<Skill> UserSkillList { get; set; }
        public List<int> SkillIDList { get; set; }

        public AddDesiredSkillsModel()
        {
            
            UserSkillList = new List<Skill>();
            SkillIDList = new List<int>();
        }



        public void OnGet(int skillid)
        {

            if(skillid != 0)
            {
                DBDesiredSkills.InsertDesiredSkills((int)HttpContext.Session.GetInt32("projid"), skillid);
            }
            SqlDataReader skillReader = DBSkillClass.SkillReader();

            while (skillReader.Read())
            {
                UserSkillList.Add(new Skill
                {
                    SkillType = skillReader["Skill_Type"].ToString(),
                    SkillID = Int32.Parse(skillReader["SkillID"].ToString()),
                    







                });
            }
            skillReader.Close();
        }
    }
}

