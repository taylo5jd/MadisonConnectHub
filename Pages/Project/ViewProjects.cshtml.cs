using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class ViewProjectsModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public List<ProjectProfile> ProjectList { get; set; }
        public List<ProjectProfile> SingleProject { get; set; }
        public int ProjID { get; set; }
        [BindProperty]
        public List<UserProfile> UserList { get; set; }

        public int ProfID { get; set; }

        public int ApproverID { get; set; }
        public string StatusFlag { get; set; }
        public string Message { get; set; }
        public ViewProjectsModel()
           
        {
            ProjectList = new List<ProjectProfile>();
            SingleProject = new List<ProjectProfile>();
            UserList = new List<UserProfile>();
            ProjID = 0;
            StatusFlag = "n";

        }

        public void OnGet(int ProjID)
        {
            
            if (ProjID != 0)
            {
                Message = "Request Sent!";
                SqlDataReader projectReader2 = DBProjectClass.SingleProjectReader(ProjID);
                SqlDataReader userReader = DBUserClass.UserReader(HttpContext.Session.GetString("username"));

                while (projectReader2.Read())
                {
                    SingleProject.Add(new ProjectProfile
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


                    foreach (var proj in SingleProject)
                    {
                        ApproverID = proj.ProfileID;
                    }


                }
                projectReader2.Close();
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


                DBJoinRequest.CreateJoinRequest(ProfID, ProjID, ApproverID, StatusFlag, "Join Request");
            }


            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToPage("/DBLogin");
            }


            //called when page first loads
            SqlDataReader projectReader = DBProjectClass.ProjectReader();

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
                    


                });

            }
            projectReader.Close();

        }

        public IActionResult OnPost() {
            SqlDataReader projectReader = DBProjectClass.ProjectSearch(SearchText);
            return Page();
        }
    }
}


