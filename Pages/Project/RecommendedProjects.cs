using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class RecommendedProjectsModel : PageModel
    {
        public List<ProjectProfile> ProjectList { get; set; }
        public List<ProjectProfile> ProjectList2 { get; set; }
        [BindProperty]
        public int projID { get; set; }
        public RecommendedProjectsModel()
        {
            ProjectList = new List<ProjectProfile>();
            ProjectList2 = new List<ProjectProfile>();

        }
        public void OnGet()
        {

            SqlDataReader tempReader = DBClasses.DBDesiredSkills.ProjectReader(DBUserClass.UserIDReader(HttpContext.Session.GetString("username")));
            while (tempReader.Read())
            {
                projID = Int32.Parse(tempReader["ProjectID"].ToString());
                SqlDataReader projectReader2 = DBProjectClass.SingleProjectReader(projID);



                while (projectReader2.Read())
                {
                    ProjectList.Add(new ProjectProfile
                    {
                        ProjectID = Int32.Parse(projectReader2["ProjectID"].ToString()),
                        ProfileID = Int32.Parse(projectReader2["ProfileID"].ToString()),
                        ProjectDescription = projectReader2["Project_Description"].ToString(),
                        ProjectType = projectReader2["Project_Type"].ToString(),
                        ProjectName = projectReader2["Project_Name"].ToString(),
                        ProjectOwnerEmail = projectReader2["Project_Owner_Email"].ToString(),
                        ProjectBeginDate = (projectReader2["Project_Begin_Date"].ToString()),
                        ProjectMissionStatement = projectReader2["Project_Mission_Statement"].ToString(),


                    });


                }



                projectReader2.Close();


            }

            int test = 0;
            bool flag = false;

            for (int i = 0; i < ProjectList.Count; i++)
            {
                if (ProjectList2.Count == 0)
                {
                    ProjectList2.Add(ProjectList[i]);
                    continue;
                }

                for (int k = 0; k < ProjectList2.Count; k++)
                {
                    if ((ProjectList[i].ProjectName.Equals(ProjectList2[k].ProjectName)))
                    {
                        flag = true;
                        break;

                    }

                }
                if (flag == false)
                {
                    ProjectList2.Add(ProjectList[i]);
                }
                else
                {
                    flag = false;
                }
            }

        }
    }
}