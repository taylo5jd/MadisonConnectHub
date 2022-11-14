using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DesiredSkill
    {
        private static readonly string Lab1ConnStr
          = @"Server=Localhost;Database=Lab3;Trusted_Connection=True";
        public static SqlDataReader DesiredSkillReader(int ProjectID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = "SELECT * FROM  Desired_SKills WHERE ProjectID =" + ProjectID;
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }
    }
}
