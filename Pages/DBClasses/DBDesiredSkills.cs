using lab_1_part_3.Pages.DataClasses;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DBDesiredSkills
    {
        private static readonly string Lab1ConnStr
            = @"Server=Localhost;Database=Lab3;Trusted_Connection=True";
        private static readonly string AuthConnStr
            = @"Server=Localhost;Database=Auth;Trusted_Connection=True";

        public static void InsertDesiredSkills(int ProjectID, int SkillID)
        {//how to incorporate team ID through parameters?
            String sqlQuery = "INSERT INTO Desired_SKills (ProjectID,SkillID) VALUES(";
            sqlQuery += "'" + ProjectID + "',";
            sqlQuery += "'" + SkillID + "')";
            SqlCommand cmdUserInsert = new SqlCommand();
            cmdUserInsert.Connection = new SqlConnection();
            cmdUserInsert.Connection.ConnectionString = Lab1ConnStr;
            cmdUserInsert.CommandText = sqlQuery;
            cmdUserInsert.Connection.Open();
            cmdUserInsert.ExecuteNonQuery();

        }

        public static int[] DesiredSkills(int ProjectID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = @"select desired_skills.skillid from desired_skills
            inner join project on Desired_Skills.projectid = project.projectid
            where project.ProjectID =" + ProjectID;
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            int[] desired = new int[50];
            int count = 0;
            while (tempReader.Read())
            {
                desired[count] = Int32.Parse(tempReader["skillid"].ToString());
                count++;
            }
            return desired;
        }
        public static int[] skillReader(int ProfileID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = @"select UserProfile_Skill.SkillID,skill_type,UserProfile_Skill.ProfileID from UserProfile_Skill
            inner join skill on skill.SkillID = UserProfile_Skill.SkillID
            where UserProfile_Skill.ProfileID = " + ProfileID;
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();

            int[] skill = new int[50];
            int count = 0;

            while(tempReader.Read())
            {
                skill[count] = Int32.Parse(tempReader["skillid"].ToString());
                count++;
            }
            return skill;
        }
        public static SqlDataReader UserReader(int ProjectID)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = @"select UserProfile_Skill.ProfileID, Desired_Skills.ProjectID, Desired_skills.skillid from UserProfile_Skill
                                        inner join  Desired_Skills on UserProfile_Skill.SkillID = Desired_Skills.SkillID
                                        where UserProfile_Skill.SkillID = desired_skills.skillid and desired_skills.projectid =" +ProjectID;
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();
            return tempReader;
        }
    }
}