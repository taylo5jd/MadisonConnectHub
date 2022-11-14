using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DBJoinRequest
    {
        private static readonly string Lab1ConnStr
            = @"Server=Localhost;Database=Lab3;Trusted_Connection=True";
        private static readonly string AuthConnStr
            = @"Server=Localhost;Database=Auth;Trusted_Connection=True";
        public static int JoinRequestCheck (string Username)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "select count(*) from Join_Request left outer join UserProfile on Join_Request.ApproverID = UserProfile.ProfileID where Join_Request.StatusFlag = 'n' and " + "UserProfile.Username = '"+  Username + "'";
            cmdUserRead.Connection.Open();
            int rowCount = (int)cmdUserRead.ExecuteScalar();
            return rowCount;
        }
        public static SqlDataReader JoinRequestReader(string Username)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = @"select UserProfile.FirstName+ ' ' + UserProfile.LastName as Requester, Join_Request.RequestID,
            Join_Request.ProfileID,Join_Request.ApproverID, (select Project.Project_Name
            from Project where Join_Request.ProjectID = Project.ProjectID) as 'Project Name',
            Join_Request.StatusFlag, Join_Request._Type, Join_Request.ProjectID,TeamComposition.TeamID
            from Join_Request inner join UserProfile on Join_Request.ProfileID = UserProfile.ProfileID
            inner join TeamComposition on Join_Request.ApproverID = TeamComposition.ProfileID
            WHERE (select UserProfile.Username from UserProfile where UserProfile.ProfileID = Join_Request.ApproverID)  =  '" + Username + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();
            return tempReader;
        }
        //need to insert two profile ID's
        public static SqlDataReader CreateJoinRequest(int ProfileID, int ProjectID,int ApproverID,string StatusFlag,string Type)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "INSERT INTO Join_Request(ProfileID, ProjectID,ApproverID,StatusFlag,_Type) VALUES( " + ProfileID +"," + ProjectID +"," +ApproverID+",'"+ StatusFlag +"','"+ Type + "')";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();
            return tempReader;
        }
        public static void UpdateJoinRequest(string s, int RequestID)
        {
            string sqlQuery = "UPDATE Join_Request SET ";
            sqlQuery += "StatusFlag ='" + s + "' WHERE Join_Request.RequestID = " + RequestID;
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = sqlQuery;
            cmdProjectRead.Connection.Open();
            cmdProjectRead.ExecuteNonQuery();
        }
    }
}
