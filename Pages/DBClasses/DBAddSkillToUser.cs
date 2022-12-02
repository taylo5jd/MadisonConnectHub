using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DBAddSkillToUser
    { //property with our connection string
       // private static readonly string Lab1ConnStr = @"Server=Localhost;Database=Lab3;Trusted_Connection=True;Pooling=False";
          private static readonly string Lab1ConnStr
              = @"Server=madisonconnecthubdb.cu2y6i9mdjnr.us-east-1.rds.amazonaws.com;
        Database=Lab3;uid=MCH_Admin;password=MCH_Password;Pooling=False";

        public static void InsertUserProfile_Skills(int ProfileID, int SkillID)
        {//how to incorporate team ID through parameters?
            String sqlQuery = "INSERT INTO UserProfile_Skill (ProfileID,SkillID) VALUES(";
            sqlQuery += "'" + ProfileID + "',";
            sqlQuery += "'" + SkillID + "')";
            SqlCommand cmdUserInsert = new SqlCommand();
            cmdUserInsert.Connection = new SqlConnection();
            
            cmdUserInsert.Connection.ConnectionString = Lab1ConnStr;
          
            cmdUserInsert.CommandText = sqlQuery;
            cmdUserInsert.Connection.Open();
            cmdUserInsert.ExecuteNonQuery();

        }

        public static SqlDataReader GeneralReaderQuery(string sqlQuery)
        {

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            //cmdProductRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProductRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

    }
}