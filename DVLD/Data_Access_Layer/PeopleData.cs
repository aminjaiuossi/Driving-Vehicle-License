using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data_Access_Layer
{
    public class PeopleData
    {
        public static DataTable GetAllPeople ()
        {
            DataTable dt = new DataTable ();
            SqlConnection connection = new SqlConnection (DataAccessSettings.ConnectionString);

            string query = "select * from people";

            SqlCommand command = new SqlCommand (query, connection);

            try
            {
                connection.Open ();
                SqlDataReader reader = command.ExecuteReader ();
                
                    dt.Load(reader);
                    reader.Close ();
            }
            catch 
            { 
            }
            finally
            {
                connection.Close ();
            }
            return dt;
        }

            

            public static DataTable GetAllCountries()
            {
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = "SELECT CountryID, CountryName FROM Countries";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    dt.Columns.Add("CountryID", typeof(int));
                    dt.Columns.Add("CountryName", typeof(string));

                    while (reader.Read())
                    {
                        dt.Rows.Add(reader["CountryID"], reader["CountryName"]);
                    }

                    reader.Close();
                }

                return dt;
            }

        public static bool IsNationalNumberExists(string nationalNo)
        {
            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM People WHERE NationalNo = @NationalNo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NationalNo", nationalNo);
                    int count = (int)cmd.ExecuteScalar();
                 
                    return count > 0;

                }
            }
        }

        public static int AddPerson(string nationalNo, string firstName, string secondName, string thirdName,
     string lastName, DateTime dateOfBirth, byte gendor, string address, string phone, string email,
     int nationalityCountryID, string imagePath)
        {
            int personId = -1;

            string query = @"INSERT INTO People
                    (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth,
                     Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)
                     VALUES
                    (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth,
                     @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NationalNo", nationalNo);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@SecondName", secondName);
                cmd.Parameters.AddWithValue("@ThirdName", thirdName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@Gendor", gendor);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);
                cmd.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                    personId = Convert.ToInt32(result);
            }

            return personId;
        }
        public static bool DeletePerson(int personId)
        {
            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PersonID", personId);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                        return false;

                    throw;
                }
            }
        }



        public static DataRow GetPersonByPersonID(int PersonID)
        {

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        public void UpdatePerson(int personId, string nationalNo, string firstName, string secondName,
                          string thirdName, string lastName, DateTime dateOfBirth, byte gendor,
                          string address, string phone, string email, int nationalityCountryID,
                          string imagePath)
        {
            string query = @"UPDATE People
                     SET NationalNo = @NationalNo,
                         FirstName = @FirstName,
                         SecondName = @SecondName,
                         ThirdName = @ThirdName,
                         LastName = @LastName,
                         DateOfBirth = @DateOfBirth,
                         Gendor = @Gendor,
                         Address = @Address,
                         Phone = @Phone,
                         Email = @Email,
                         NationalityCountryID = @NationalityCountryID,
                         ImagePath = @ImagePath
                     WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PersonID", personId);
                cmd.Parameters.AddWithValue("@NationalNo", nationalNo);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@SecondName", secondName);
                cmd.Parameters.AddWithValue("@ThirdName", thirdName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@Gendor", gendor);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);
                cmd.Parameters.AddWithValue("@ImagePath", imagePath ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static string GetCountryNameByID(int countryID)
        {
            string countryName = "";
            string query = "SELECT CountryName FROM Countries WHERE CountryID = @CountryID";

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                conn.Open();

                object result = cmd.ExecuteScalar();
                if (result != null)
                    countryName = result.ToString();
            }

            return countryName;
        }



    }
}
    

