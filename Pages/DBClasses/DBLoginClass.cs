using lab_1_part_3.Pages.DataClasses;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.DBClasses
{
    public class DBLoginClass
    {
        private static readonly string Lab1ConnStr
            = @"Server=Localhost;Database=Lab3;Trusted_Connection=True";
        private static readonly string AuthConnStr
           = @"Server=Localhost;Database=Auth;Trusted_Connection=True";
        

        public static bool StoredProcedure(string Username, string Password)
        {
            SqlCommand cmdSP = new SqlCommand();
            cmdSP.Connection = new SqlConnection();
            cmdSP.Connection.ConnectionString = AuthConnStr;
            cmdSP.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSP.Parameters.AddWithValue("@Username", Username);
            
            cmdSP.CommandText = "sp_login";
            cmdSP.Connection.Open();
           SqlDataReader tempReader = cmdSP.ExecuteReader();
            string correctHash = "";
            while (tempReader.Read())
            {
                correctHash += tempReader["Password"].ToString();
            }
            if(PasswordHash.ValidatePassword(Password, correctHash))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public static int LoginQuery(string loginQuery)
        {
            // This method expects to receive an SQL SELECT
            // query that uses the COUNT command.
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = Lab1ConnStr;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Connection.Open();
            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();
            return rowCount;
        }
        

        public static bool HashedParameterLogin(string Username, string Password)
        {
            string loginQuery =
            "SELECT Password FROM HashedCredentials WHERE Username = @Username";
        SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = AuthConnStr;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Connection.Open();
           // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["Password"].ToString();
                if (PasswordHash.ValidatePassword(Password, correctHash))
                {
                    return true;
                }
            }
            return false;
        }
        public static void CreateHashedUser(string Username, string Password, UserProfile p)
        {
            string loginQuery =
            "INSERT INTO HashedCredentials (Username,Password) values (@Username, @Password)";
            String sqlQuery = "INSERT INTO UserProfile (Username, FirstName,LastName,Email,UserType,PhoneNumber,Professional_Email,Professional_Company,Faculty_Association) VALUES(";
            sqlQuery += "'" + Username + "',";
            sqlQuery += "'" + p.FirstName + "',";
            sqlQuery += "'" + p.LastName + "'" + ",";
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
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = new SqlConnection();
            cmdLogin.Connection.ConnectionString = AuthConnStr;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", PasswordHash.HashPassword(Password));
            cmdLogin.Connection.Open();
            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();
        }


    }
}
