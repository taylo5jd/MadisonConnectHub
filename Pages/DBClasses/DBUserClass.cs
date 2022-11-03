using System.Data.SqlClient;


namespace lab_1_part_3.Pages.DBClasses
{
    public class DBUserClass
    {
        //property with our connection string
        private static readonly string Lab1ConnStr
            = @"Server=Localhost;Database=Lab3;Trusted_Connection=True";
        private static readonly string AuthConnStr
            = @"Server=Localhost;Database=Auth;Trusted_Connection=True";
        public static SqlDataReader UserReader(string username)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "SELECT * FROM UserProfile WHERE Username ='"+ username + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();
            return tempReader;
        }
        public static int UserIDReader(string username)
        {
        

            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "SELECT ProfileID FROM UserProfile WHERE Username ='" + username + "'";
            cmdUserRead.Connection.Open();
            int rowCount = (int)cmdUserRead.ExecuteScalar();
            return rowCount;
        }


        public static SqlDataReader UserSearch(string search)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = new SqlConnection();
            cmdProjectRead.Connection.ConnectionString = Lab1ConnStr;
            cmdProjectRead.CommandText = "SELECT * FROM  UserProfile WHERE FirstName LIKE '%" + search + "%' or LastName LIKE '%" + search + "%'";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader UsernameReader()
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "SELECT Username FROM UserProfile";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();
            return tempReader;
        }
       
        public static SqlDataReader FirstNameReader(string Username)
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "SELECT FirstName FROM UserProfile where Username = '" + Username + "'";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader MultipleUserReader()
        {
            SqlCommand cmdUserRead = new SqlCommand();
            cmdUserRead.Connection = new SqlConnection();
            cmdUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdUserRead.CommandText = "SELECT * FROM UserProfile";
            cmdUserRead.Connection.Open();
            SqlDataReader tempReader = cmdUserRead.ExecuteReader();
            return tempReader;
        }


        public static void InsertUserProfile(UserProfile p)
        {
            String sqlQuery = "INSERT INTO UserProfile (Username, Password,FirstName,LastName,Email,UserType,PhoneNumber,Professional_Email,Professional_Company,Faculty_Association) VALUES(";
            sqlQuery += "'" + p.Username + "',";
         
            sqlQuery += "'" + p.FirstName + "',";
            sqlQuery += "'" + p.LastName + "'"  + ",";
            sqlQuery += "'" + p.Email + "'" + ",";
            sqlQuery += "'" + p.UserType + "'" + ",";
            sqlQuery += "'" + p.PhoneNumber + "'" + ",";
            sqlQuery += "'" + p.ProfessionalEmail + "'" + ",";
            sqlQuery += "'" + p.ProfessionalCompany + "'" + ",";
            sqlQuery += "'" + p.FacultyAssociation + "')";
            SqlCommand cmdUserInsert = new SqlCommand();
            cmdUserInsert.Connection = new SqlConnection();
            cmdUserInsert.Connection.ConnectionString = Lab1ConnStr;
            cmdUserInsert.CommandText = sqlQuery;
            cmdUserInsert.Connection.Open();
            cmdUserInsert.ExecuteNonQuery();

        }
        public static SqlDataReader SingleUserReader(int ProfileID)
        {
            SqlCommand cmdSingleUserRead = new SqlCommand();
            cmdSingleUserRead.Connection = new SqlConnection();
            cmdSingleUserRead.Connection.ConnectionString = Lab1ConnStr;
            cmdSingleUserRead.CommandText = "SELECT * FROM UserProfile WHERE ProfileID = " + ProfileID;
            cmdSingleUserRead.Connection.Open();
            SqlDataReader tempReader = cmdSingleUserRead.ExecuteReader();

            return tempReader;
        }

        public static void UpdateUserProfile(UserProfile u)
        {
            string sqlQuery = "UPDATE UserProfile SET ";
            sqlQuery += "Username='" + u.Username + "',";
            sqlQuery += "FirstName='" + u.FirstName + "',";
            sqlQuery += "LastName='" + u.LastName + "',";
            sqlQuery += "Email='" + u.Email + "',";
            sqlQuery += "UserType='" + u.UserType + "',";
            sqlQuery += "PhoneNumber='" + u.PhoneNumber + "',";
            sqlQuery += "Professional_Email='" + u.ProfessionalEmail + "',";
            sqlQuery += "Professional_Company='" + u.ProfessionalCompany + "',";
            sqlQuery += "LinkedIn='" + u.LinkedIn + "',";
            sqlQuery += "Video_Introduction='" + u.VideoIntroduction + "',";
            sqlQuery += "Faculty_Association='" + u.FacultyAssociation + "' WHERE ProfileID =" + u.ProfileID;
            SqlCommand cmdUpdateUser = new SqlCommand();
            cmdUpdateUser.Connection = new SqlConnection();
            cmdUpdateUser.Connection.ConnectionString = Lab1ConnStr;
            cmdUpdateUser.CommandText = sqlQuery;
            cmdUpdateUser.Connection.Open();
            cmdUpdateUser.ExecuteNonQuery();
        }
    }
}
