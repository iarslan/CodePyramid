﻿using CodePyramidv1.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ProgressAndAssessmentViewModel FetchProgressResults()
        {
            string uname = "dummy"; //for now, this is a hardcoded username. eventually, this will be replaced with the username stored in the cookie.
            ProgressAndAssessmentViewModel paavm = new ProgressAndAssessmentViewModel();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
            "SELECT s.sectionName as Lesson "
        +   "FROM `User` u "
        +   "INNER JOIN `Completion` c ON u.userId = c.userId "
        +   "INNER JOIN `Section` s ON s.sectionId = c.sectionId "
        +   "WHERE u.username = '" + uname + "' "
        +   "ORDER BY u.username;", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        paavm.CompletedLessons.Add(reader.GetString("Lesson"));
                    }
                }
            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
            "SELECT g.score as Score, s.sectionName as Assessment "
        + "FROM `User` u "
        + "INNER JOIN `Grade` g ON u.userId = g.userId "
        + "INNER JOIN `Section` s ON s.sectionId = g.sectionId "
        + "WHERE u.username = '" + uname + "' "
        + "ORDER BY u.username;", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        paavm.AssessmentScores.Add(reader.GetString("Assessment"), reader.GetInt16("Score"));
                    }
                }
            }

            return paavm;
        }

        public string GetLogonInfo(LoginViewModel model)
        {
            string ph = Encoding.UTF8.GetString(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
            string uname = string.Empty;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT username FROM User where username = '" + model.Username + "' and passwordHash = '" + ph + "'", conn);
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

            string ph = Encoding.UTF8.GetString(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
            int newId = 0;
            string insertSQL =
                "Insert into User (username, passwordHash) VALUES (@username, @passwordHash); ";
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(insertSQL, conn);
//                cmd.Parameters.Add("@userId", MySqlDbType.Int32);
//                cmd.Parameters["@userId"].Value = DateTime.Now.GetHashCode();

                cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                cmd.Parameters["@username"].Value = model.Username;

                cmd.Parameters.Add("@passwordHash", MySqlDbType.VarChar);
                cmd.Parameters["@passwordHash"].Value = ph;
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
