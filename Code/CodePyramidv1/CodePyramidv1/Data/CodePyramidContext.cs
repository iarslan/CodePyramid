using CodePyramidv1.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePyramidv1.Data
{
    public class CodePyramidContext
    {
        public string ConnectionString { get; set; }

        public CodePyramidContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public List<CourseViewModel> GetAllCourses()
        {

            var list = new List<CourseViewModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT courseId, courseName FROM Course", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CourseViewModel()
                        {
                            Id = reader.GetInt32("courseId"),
                            Name = reader.GetString("courseName")
                        });
                    }
                }
            }

            return list;
        }
        public string GetLogonInfo(string username)
        {
            string uname = string.Empty;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT username FROM User where username = '" + username + "'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        uname = reader.GetString("username");
                    }
                }
            }

            return uname;
        }
        public int RegisterUser(RegisterViewModel model)
        {

            int newId = 0;
            string insertSQL =
                "Insert into User (userId, username, passwordHash) VALUES (@userId, @username, @passwordHash); ";
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(insertSQL, conn);
                cmd.Parameters.Add("@userId", MySqlDbType.Int32);
                cmd.Parameters["@userId"].Value = DateTime.Now.GetHashCode();

                cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                cmd.Parameters["@username"].Value = model.Username;

                cmd.Parameters.Add("@passwordHash", MySqlDbType.VarChar);
                cmd.Parameters["@passwordHash"].Value = model.Password;
                try
                {
                    conn.Open();
                    newId = (Int32)cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    string err;
                    err = ex.Message;
                }
            }

            return newId;
        }
    }
}
