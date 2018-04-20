using CodePyramidv1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

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

        public int InsertAssessmentScore(String sectionName, String username, Int16 score)
        {
            int sectionId = 0;
            int userId = 0;

            //The sectionId's of the various quizzes are known, so we can just use a switch statement. No query needed. 
            switch (sectionName)
            {
                case "HTML/CSS Quiz 1":
                    sectionId = 11;
                    break;
                case "HTML/CSS Quiz 2":
                    sectionId = 12;
                    break;
                case "HTML/CSS Quiz 3":
                    sectionId = 13;
                    break;
                case "HTML/CSS Quiz 4":
                    sectionId = 14;
                    break;
                case "HTML/CSS Quiz 5":
                    sectionId = 15;
                    break;
                case "Javascript Quiz 1":
                    sectionId = 16;
                    break;
                case "Javascript Quiz 2":
                    sectionId = 17;
                    break;
                case "Javascript Quiz 3":
                    sectionId = 18;
                    break;
                case "Javascript Quiz 4":
                    sectionId = 19;
                    break;
                case "Javascript Quiz 5":
                    sectionId = 20;
                    break;
                default:
                    break;
            }

            if( sectionId == 0 )
            {
                //This means you didn't enter a valid quiz name. 
                return 1;
            }

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select userId from `User` where username = '" + username + "';", conn); 
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //Grab the relevant userId to use in our insert command. 
                        userId = reader.GetInt32("userId");
//                        userId = reader.GetInt64("userId");
                    }
                    else
                    {
                        //This means the username we searched for isn't in the database.
                        //We can't insert for a user that doesn't exist, so we must quit. 
                        return 2;
                    }
                }
            }

            using (MySqlConnection conn = GetConnection())
            {
//                MySqlCommand cmd = new MySqlCommand("insert into `Grade` (score, userId, sectionId) "
 //                   + "values (" + score + ", " + userId + ", " + sectionId + ");", conn);
                MySqlCommand cmd = new MySqlCommand("insert into `Grade` (score, userId, sectionId) "
                    + "values (" + score + ", " + userId + ", " + sectionId + ") "
                    + "on duplicate key update score = " + score + ";", conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //This means the insertion failed. We'll return false to inform. 
                    return 3;
                }
            }

            return 0;
        }

        public int InsertLessonCompletion(String sectionName, String username)
        {
            int sectionId = 0;
            int userId = 0;

            //The sectionId's of the various lessons are known, so we can just use a switch statement. No query needed. 
            switch (sectionName)
            {
                case "HTML/CSS Lesson 1":
                    sectionId = 1;
                    break;
                case "HTML/CSS Lesson 2":
                    sectionId = 2;
                    break;
                case "HTML/CSS Lesson 3":
                    sectionId = 3;
                    break;
                case "HTML/CSS Lesson 4":
                    sectionId = 4;
                    break;
                case "HTML/CSS Lesson 5":
                    sectionId = 5;
                    break;
                case "Javascript Lesson 1":
                    sectionId = 6;
                    break;
                case "Javascript Lesson 2":
                    sectionId = 7;
                    break;
                case "Javascript Lesson 3":
                    sectionId = 8;
                    break;
                case "Javascript Lesson 4":
                    sectionId = 9;
                    break;
                case "Javascript Lesson 5":
                    sectionId = 10;
                    break;
                default:
                    break;
            }

            if( sectionId == 0)
            {
                //This means you didn't enter a valid lesson name
                return 1;
            }

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select userId from `User` where username = '" + username + "';", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //Grab the relevant userId to use in our insert command. 
                        userId = reader.GetInt16("userId");
                    }
                    else
                    {
                        //This means the username we searched for isn't in the database.
                        //We can't insert for a user that doesn't exist, so we must quit. 
                        return 2;
                    }
                }
            }

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("insert into `Completion` (userId, sectionId) "
                    + "values (" + userId + ", " + sectionId + ");", conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //This means the insertion failed. We'll return false to inform. 
                    return 3;
                }
            }

            return 0;
        }

        public ProgressAndAssessmentViewModel FetchProgressResults(String uname)
        {
            ProgressAndAssessmentViewModel paavm = new ProgressAndAssessmentViewModel
            {
                CompletedLessons = new List<string>(),
                AssessmentScores = new Dictionary<String, Int16>()
            };

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
                    while (reader.Read())
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
                    while (reader.Read())
                    {
                        try
                        {
                            paavm.AssessmentScores.Add(reader.GetString("Assessment"), reader.GetInt16("Score"));
                        }
                        catch (Exception)
                        {
                            //this means there's a duplicate entry. just go to next.
                        }
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
            if( ! model.Password.Equals(model.ConfirmPassword) )
            {
                return 0;
            }
            if( model.Password.Length < 6 || model.Password.Length > 100 )
            {
                return 0;
            }

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
