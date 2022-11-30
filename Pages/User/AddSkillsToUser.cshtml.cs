using lab_1_part_3.Pages.DataClasses;
using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.User
{
    public class AddSkillsToUserModel : PageModel
    {
        public int Uuid { get; set; }


        [BindProperty]
        public Skill SkillToAdd { get; set; }
        public List<Skill> UserSkillList { get; set; }


        public AddSkillsToUserModel()
        {
            SkillToAdd = new Skill();
            UserSkillList = new List<Skill>();

        }




        public void OnGet(int prf, int skillid)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }
            Uuid = prf;
            if (skillid != 0)
            {

                int teamIDInt = (skillid);

                DBAddSkillToUser.InsertUserProfile_Skills(Int32.Parse(HttpContext.Session.GetString("profileid").ToString()), teamIDInt);
            }

            int profileIDTest = Int32.Parse(HttpContext.Session.GetString("profileid"));


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

        public IActionResult OnPost()
        {

            DBAddSkillToUser.InsertUserProfile_Skills(Uuid, SkillToAdd.SkillID);

            return RedirectToPage("Index");
        }

    }
}