using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class SearchProjectModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        [BindProperty]
        public List<ProjectProfile> ProjectList { get; set; }
        [BindProperty]
        public List<ProjectProfile> SingleProject { get; set; }
        [BindProperty]
        public List<UserProfile> UserList { get; set; }

        public int ProjID { get; set; }
       
        public int ProfID { get; set; }
        
        public int ApproverID { get; set; }
        public string StatusFlag { get; set; }

        public SearchProjectModel()
        {
            ProjectList = new List<ProjectProfile>();
            SingleProject = new List<ProjectProfile>(); 
            UserList = new List<UserProfile>();
            ProjID = 0;
            StatusFlag = "n";
        }
        public void OnGet(int ProjID)
        {
            //need to insert two profile id's
            if (ProjID != 0)
            {
                SqlDataReader projectReader = DBProjectClass.SingleProjectReader(ProjID); 
                SqlDataReader userReader = DBUserClass.UserReader(HttpContext.Session.GetString("username"));

                while (projectReader.Read())
                {
                    SingleProject.Add(new ProjectProfile
                    {
                        ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                        ProfileID = Int32.Parse(projectReader["ProfileID"].ToString()),
                        ProjectDescription = projectReader["Project_Description"].ToString(),
                        ProjectType = projectReader["Project_Type"].ToString(),
                        ProjectName = projectReader["Project_Name"].ToString(),
                        ProjectOwnerEmail = projectReader["Project_Owner_Email"].ToString(),
                        ProjectBeginDate = (projectReader["Project_Begin_Date"].ToString()),
                        ProjectMissionStatement = projectReader["Project_Mission_Statement"].ToString(),
                        



                    });
                   

                        foreach (var proj in SingleProject)
                    {
                        ApproverID = proj.ProfileID;
                    }
                        

                }
                projectReader.Close();
                while (userReader.Read())
                {
                    UserList.Add(new UserProfile
                    {
                        Username = userReader["Username"].ToString(),
                        ProfileID = Int32.Parse(userReader["ProfileID"].ToString()),
                        FirstName = userReader["FirstName"].ToString(),
                        LastName = userReader["LastName"].ToString(),
                        Email = userReader["Email"].ToString(),
                        UserType = userReader["UserType"].ToString(),
                        PhoneNumber = userReader["PhoneNumber"].ToString(),
                        ProfessionalEmail = userReader["Professional_Email"].ToString(),
                        ProfessionalCompany = userReader["Professional_Company"].ToString(),
                        FacultyAssociation = userReader["Faculty_Association"].ToString(),

                    });



                    foreach (var usr in UserList)
                    {
                        ProfID = usr.ProfileID;
                    }
                   
                }


                DBJoinRequest.CreateJoinRequest(ProfID, ProjID, ApproverID,StatusFlag,"Join Request");
            }
        }
        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }


            //called when page first loads
            SqlDataReader projectReader = DBProjectClass.ProjectSearch(SearchText);

            while (projectReader.Read())
            {
                ProjectList.Add(new ProjectProfile
                {
                    ProjectBeginDate = projectReader["Project_Begin_Date"].ToString(),
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProfileID = Int32.Parse(projectReader["ProfileID"].ToString()),
                    ProjectDescription = projectReader["Project_Description"].ToString(),
                    ProjectType = projectReader["Project_Type"].ToString(),
                    ProjectName = projectReader["Project_Name"].ToString(),
                    ProjectOwnerEmail = projectReader["Project_Owner_Email"].ToString(),
                    ProjectMissionStatement = projectReader["Project_Mission_Statement"].ToString(),

                    //DesiredSkills = projectReader["Desired_Skills"].ToString(),


                });

            }
           
            projectReader.Close();
    
            return Page();
        }
    }
}
