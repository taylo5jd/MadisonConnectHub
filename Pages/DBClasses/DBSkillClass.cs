using lab_1_part_3.Pages.DataClasses;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DBSkillClass
    {
        //property with our connection string
        //private static readonly string Lab1ConnStr
        //= @"Server=Localhost;Database=Lab3;Trusted_Connection=True";

        private static readonly string Lab1ConnStr
            = @"Server=madisonconnecthubdb.cu2y6i9mdjnr.us-east-1.rds.amazonaws.com;
		    Database=Lab3;uid=MCH_Admin;password=MCH_Password";

        public static SqlDataReader SkillReader()
        {
            SqlCommand cmdSkillRead = new SqlCommand();
            cmdSkillRead.Connection = new SqlConnection();
            cmdSkillRead.Connection.ConnectionString = Lab1ConnStr;
            cmdSkillRead.CommandText = "SELECT * FROM Skill";
            cmdSkillRead.Connection.Open();
            SqlDataReader tempReader = cmdSkillRead.ExecuteReader();
            return tempReader;
        }

        public static void InsertSkill(Skill s)
        {
            String sqlQuery = "INSERT INTO Skill (Skill_Type) VALUES(";

           
            sqlQuery += "'" + s.SkillType + "')";
            SqlCommand cmdInsertSkill = new SqlCommand();
            cmdInsertSkill.Connection = new SqlConnection();
            cmdInsertSkill.Connection.ConnectionString = Lab1ConnStr;
            cmdInsertSkill.CommandText = sqlQuery;
            cmdInsertSkill.Connection.Open();
            cmdInsertSkill.ExecuteNonQuery();

        }
        public static SqlDataReader SingleSkillReader(int SkillID)
        {
            SqlCommand cmdSingleSkillRead = new SqlCommand();
            cmdSingleSkillRead.Connection = new SqlConnection();
            cmdSingleSkillRead.Connection.ConnectionString = Lab1ConnStr;
            cmdSingleSkillRead.CommandText = "SELECT * FROM Skill WHERE SkillID = " + SkillID;
            cmdSingleSkillRead.Connection.Open();
            SqlDataReader tempReader = cmdSingleSkillRead.ExecuteReader();

            return tempReader;
        }
        public static SqlDataReader SingleUserSkillReader(string Username)
        {
            SqlCommand cmdSingleSkillRead = new SqlCommand();
            cmdSingleSkillRead.Connection = new SqlConnection();
            cmdSingleSkillRead.Connection.ConnectionString = Lab1ConnStr;
            cmdSingleSkillRead.CommandText = "SELECT Skill.SkillID,UserProfile.FirstName, UserProfile.LastName, Skill.Skill_Type FROM   UserProfile INNER JOIN UserProfile_Skill ON UserProfile.ProfileID = UserProfile_Skill.ProfileID INNER JOIN Skill ON UserProfile_Skill.SkillID = Skill.SkillID WHERE UserProfile.Username = '" + Username + "'";
            cmdSingleSkillRead.Connection.Open();
            SqlDataReader tempReader = cmdSingleSkillRead.ExecuteReader();

            return tempReader;
        }


        public static void UpdateSkill(Skill s)
        {
            string sqlQuery = "UPDATE Skill SET ";
            sqlQuery += "Skill_Type ='" + s.SkillType + "',";
            sqlQuery +=  "' WHERE SkillID =" + s.SkillID;
            SqlCommand cmdUpdateSkill = new SqlCommand();
            cmdUpdateSkill.Connection = new SqlConnection();
            cmdUpdateSkill.Connection.ConnectionString = Lab1ConnStr;
            cmdUpdateSkill.CommandText = sqlQuery;
            cmdUpdateSkill.Connection.Open();
            cmdUpdateSkill.ExecuteNonQuery();



        }
    }
}
