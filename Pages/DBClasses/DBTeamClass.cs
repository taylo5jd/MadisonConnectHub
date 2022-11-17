using lab_1_part_3.Pages.DataClasses;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DBTeamClass
    {
        //property with our connection string
        //private static readonly string Lab1ConnStr
        //= @"Server=Localhost;Database=Lab3;Trusted_Connection=True";

        private static readonly string Lab1ConnStr
            = @"Server=madisonconnecthubdb.cu2y6i9mdjnr.us-east-1.rds.amazonaws.com;
		    Database=Lab3;uid=MCH_Admin;password=MCH_Password";

        public static SqlDataReader TeamReader()
        {
            SqlCommand cmdTeamRead = new SqlCommand();
            cmdTeamRead.Connection = new SqlConnection();
            cmdTeamRead.Connection.ConnectionString = Lab1ConnStr;
            cmdTeamRead.CommandText = "SELECT * FROM Team";
            cmdTeamRead.Connection.Open();
            SqlDataReader tempReader = cmdTeamRead.ExecuteReader();
            return tempReader;
        }

        public static int TeamIDReader()
        {
            SqlCommand cmdUserRead = new SqlCommand();
                cmdUserRead.Connection = new SqlConnection();
                cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
                cmdUserRead.CommandText = "select MAX(Team.TeamID) from Team";
                cmdUserRead.Connection.Open();
                int rowCount = (int)cmdUserRead.ExecuteScalar();
                return rowCount;
            
        }


        public static SqlDataReader TeamOwner (string Username, int TeamID)
        {
            SqlCommand cmdTeamRead = new SqlCommand();
            cmdTeamRead.Connection = new SqlConnection();
            cmdTeamRead.Connection.ConnectionString = Lab1ConnStr;
            cmdTeamRead.CommandText = "select UserProfile.FirstName+UserProfile.LastName as Requester, Join_Request.ProfileID,Join_Request.ApproverID, (select Project.Project_Name from Project where Join_Request.ProjectID = Project.ProjectID) as 'Project Name', Join_Request.StatusFlag, Join_Request.ProjectID,TeamComposition.TeamID from Join_Request inner join UserProfile on Join_Request.ProfileID = UserProfile.ProfileID inner join TeamComposition on Join_Request.ApproverID = TeamComposition.ProfileID WHERE (select UserProfile.Username from UserProfile where UserProfile.ProfileID = Join_Request.ApproverID)  =  '" + Username + "'" + "and TeamComposition.TeamID =" + TeamID;
            cmdTeamRead.Connection.Open();
            SqlDataReader tempReader = cmdTeamRead.ExecuteReader();
            return tempReader;
        }
        public static void InsertTeam(Team t)
        {
            String sqlQuery = "INSERT INTO Team (Team_Email, Team_Description, Team_Name, Team_Meeting_Date,ProjectID) VALUES(";

            sqlQuery += "'" + t.TeamEmail + "'" + ",";
            sqlQuery += "'" + t.TeamDescription + "'" + ",";
            sqlQuery += "'" + t.TeamName + "'" + ",";
            sqlQuery += "'" + t.TeamMeetingDate + "'" + ",";
            sqlQuery += "'" + t.ProjectID + "')";
            SqlCommand cmdInsertSkill = new SqlCommand();
            cmdInsertSkill.Connection = new SqlConnection();
            cmdInsertSkill.Connection.ConnectionString = Lab1ConnStr;
            cmdInsertSkill.CommandText = sqlQuery;
            cmdInsertSkill.Connection.Open();
            cmdInsertSkill.ExecuteNonQuery();

        }
        public static SqlDataReader SingleTeamReader(int TeamID)
        {
            SqlCommand cmdSingleTeamRead = new SqlCommand();
            cmdSingleTeamRead.Connection = new SqlConnection();
            cmdSingleTeamRead.Connection.ConnectionString = Lab1ConnStr;
            cmdSingleTeamRead.CommandText = "SELECT * FROM Team WHERE TeamID = " + TeamID;
            cmdSingleTeamRead.Connection.Open();
            SqlDataReader tempReader = cmdSingleTeamRead.ExecuteReader();

            return tempReader;
        }
        public static SqlDataReader SingleUserTeamReader(string Username)
        {
            SqlCommand cmdSingleTeamRead = new SqlCommand();
            cmdSingleTeamRead.Connection = new SqlConnection();
            cmdSingleTeamRead.Connection.ConnectionString = Lab1ConnStr;
            cmdSingleTeamRead.CommandText = "SELECT Team.TeamID,Team.ProjectID,Team.Team_Email, Team.Team_Name, Team.Team_Description, Team.Team_Meeting_Date FROM  UserProfile INNER JOIN TeamComposition ON UserProfile.ProfileID = TeamComposition.ProfileID INNER JOIN Team ON TeamComposition.TeamID = Team.TeamID WHERE UserProfile.Username = '" + Username + "'";

            cmdSingleTeamRead.Connection.Open();
            SqlDataReader tempReader = cmdSingleTeamRead.ExecuteReader();

            return tempReader;
        }

        public static void UpdateTeam(Team t)
        {
            string sqlQuery = "UPDATE Team SET ";
            sqlQuery += "Team_Email ='" + t.TeamEmail + "',";
            sqlQuery += "Team_Description ='" + t.TeamDescription + "',";
            sqlQuery += "Team_Name ='" + t.TeamName + "',";
            sqlQuery += "Team_Meeting_Date ='" + t.TeamMeetingDate + "',";
            sqlQuery += "ProjectID='" + t.ProjectID + "' WHERE TeamID =" + t.TeamID;
            SqlCommand cmdUpdateSkill = new SqlCommand();
            cmdUpdateSkill.Connection = new SqlConnection();
            cmdUpdateSkill.Connection.ConnectionString = Lab1ConnStr;
            cmdUpdateSkill.CommandText = sqlQuery;
            cmdUpdateSkill.Connection.Open();
            cmdUpdateSkill.ExecuteNonQuery();



        }
    }
}
