using lab_1_part_3.Pages.DataClasses;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DBProjectClass
    {
        //property with our connection string
       // private static readonly string Lab1ConnStr
       // = @"Server=Localhost;Database=Lab3;Trusted_Connection=True;Pooling=False";

          private static readonly string Lab1ConnStr
             = @"Server=madisonconnecthubdb.cu2y6i9mdjnr.us-east-1.rds.amazonaws.com;
       Database=Lab3;uid=MCH_Admin;password=MCH_Password;Pooling=False";
        public static SqlDataReader ProjectSearch(string search)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = "SELECT * FROM  Project WHERE Project_Name LIKE '%" + search + "%'";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader TeamMember(int ProjectID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = @"select UserProfile.FirstName + ' ' + UserProfile.LastName as Name from UserProfile
                                        inner join TeamComposition on UserProfile.ProfileID = TeamComposition.ProfileID
                                        inner join team on team.TeamID = TeamComposition.TeamID
                                        inner join project on team.ProjectID = project.ProjectID
                                        where Project.ProjectID = " + ProjectID;
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }


        public static SqlDataReader ProjectReader()
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = "SELECT * FROM Project";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }
        public static int ProjectIDReader()
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "select MAX(Project.ProjectID) from Project";
            cmdUserRead.Connection.Open();
            int rowCount = (int)cmdUserRead.ExecuteScalar();
            return rowCount;

        }

        public static SqlDataReader UserProjectReader(string username)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = @"select Project.ProjectID,Project.Project_Name, project.Project_Type, Project.Project_Description from
            Project inner join Team on Team.ProjectID = Project.ProjectID
            inner join TeamComposition on team.TeamID = TeamComposition.TeamID
            inner join UserProfile on TeamComposition.ProfileID = UserProfile.ProfileID
            where Username = '" + username + "'";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }


        public static void InsertProject(ProjectProfile p)
        {
            String sqlQuery = "INSERT INTO Project (ProfileID,Project_Description,Project_Type,Project_Name,Project_Owner_Email,Project_Begin_Date,Project_Mission_Statement) VALUES(";

            sqlQuery += "'" + p.ProfileID + "'" + ",";
            sqlQuery += "'" + p.ProjectDescription + "'" + ",";
            sqlQuery += "'" + p.ProjectType + "'" + ",";
            sqlQuery += "'" + p.ProjectName + "'" + ",";
            sqlQuery += "'" + p.ProjectOwnerEmail + "',";
            sqlQuery += "'" + p.ProjectBeginDate + "',";
            sqlQuery += "'" + p.ProjectMissionStatement + "')";
            SqlCommand cmdInsertProject = new SqlCommand();
            cmdInsertProject.Connection = new SqlConnection();
            cmdInsertProject.Connection.ConnectionString = Lab1ConnStr;
            cmdInsertProject.CommandText = sqlQuery;
            cmdInsertProject.Connection.Open();
            cmdInsertProject.ExecuteNonQuery();

        }
        public static SqlDataReader SingleUserProjectReader(string Username)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = "SELECT Project.ProjectID,Project_Begin_Date,Project_Mission_Statement, Project.ProfileID,Project.Project_Name, Project.Project_Description, Project.Project_Type,Project.Project_Owner_Email FROM Project INNER JOIN UserProfile ON Project.ProfileID = UserProfile.ProfileID WHERE UserProfile.Username ='" + Username + "'";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();

            return tempReader;
        }


        public static SqlDataReader SingleProjectReader(int ProjectID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = "SELECT * FROM Project WHERE ProjectID = " + ProjectID;
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();

            return tempReader;
        }
        public static string ProjOwner(int ProjID)
        {
            string projown="";
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = @"select UserProfile.Username from UserProfile inner join Project on UserProfile.ProfileID = Project.ProfileID where Project.ProjectID =" + ProjID;
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            while (tempReader.Read())
            {
                projown = tempReader["Username"].ToString();
            }

            return projown;
        }
        public static void UpdateProject(ProjectProfile p)
        {
            string sqlQuery = "UPDATE Project SET ";
            sqlQuery += "Project_Description='" + p.ProjectDescription + "',";
            sqlQuery += "Project_Type='" + p.ProjectType + "',";
            sqlQuery += "Project_Name='" + p.ProjectName + "',";
            sqlQuery += "Project_Owner_Email='" + p.ProjectOwnerEmail + ",Project_Begin_Date='" + p.ProjectBeginDate +
                ",Project_Mission_Statement ='" + p.ProjectMissionStatement + "' WHERE ProjectID =" + p.ProjectID;
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = sqlQuery;
            cmdProjectRead.Connection.Open();
            cmdProjectRead.ExecuteNonQuery();
        }
    }
}
