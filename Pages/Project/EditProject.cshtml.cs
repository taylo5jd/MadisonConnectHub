using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.Project
{
    public class EditProjectModel : PageModel
    {
        [BindProperty]
        public ProjectProfile ProjectToUpdate { get; set; }
        [BindProperty]
        public List<UserProfile> UserList { get; set; }
   

        public EditProjectModel()
        {
            ProjectToUpdate = new ProjectProfile();
            UserList = new List<UserProfile>();

        }
            public void OnGet(int ProjectID)
            {
                if (HttpContext.Session.GetString("username") == null)
                {
                    RedirectToPage("/DBLogin");
                }
                SqlDataReader singleProject = DBProjectClass.SingleProjectReader(ProjectID);

                while (singleProject.Read())
                {
                    ProjectToUpdate.ProjectID = ProjectID;
                    ProjectToUpdate.ProfileID = Int32.Parse(singleProject["ProfileID"].ToString());
                    ProjectToUpdate.ProjectDescription = singleProject["Project_Description"].ToString();
                    ProjectToUpdate.ProjectType = singleProject["Project_Type"].ToString();
                    ProjectToUpdate.ProjectName = singleProject["Project_Name"].ToString();
                    ProjectToUpdate.ProjectOwnerEmail = singleProject["Project_Owner_Email"].ToString();
                ProjectToUpdate.ProjectMissionStatement = singleProject["Project_Mission_Statement"].ToString();


            }
                SqlDataReader userReader = DBUserClass.MultipleUserReader();

                while (userReader.Read())
                {
                    UserList.Add(new UserProfile
                    {
                        ProfileID = Int32.Parse(userReader["ProfileID"].ToString()),
                        Username = userReader["Username"].ToString(),
                        FirstName = userReader["FirstName"].ToString(),
                        LastName = userReader["LastName"].ToString(),
                        Email = userReader["Email"].ToString(),
                        UserType = userReader["UserType"].ToString(),
                        PhoneNumber = userReader["PhoneNumber"].ToString(),
                        ProfessionalEmail = userReader["Professional_Email"].ToString(),
                        ProfessionalCompany = userReader["Professional_Company"].ToString(),
                        FacultyAssociation = userReader["Faculty_Association"].ToString(),

                    });


                }
                singleProject.Close();
                userReader.Close();
            }

            public IActionResult OnPost()
            {
                DBProjectClass.UpdateProject(ProjectToUpdate);

                return RedirectToPage("Index");
            }
        }
    } 