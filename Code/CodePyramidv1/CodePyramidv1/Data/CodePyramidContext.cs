﻿using CodePyramidv1.ViewModels;
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

        /*  QUICK CHEAT SHEET FOR THE ID'S ON LESSONS/QUIZZES
         *   
         *   COURSES
         *   -------
         *   (courseId)     (courseName)
         *   1              "HTML/CSS"
         *   2              "Javascript"
         *   
         *   
         *   
         *  SECTIONS
         *  --------
         *  (sectionId)     (sectionName)               (courseId)
         *  1               "HTML/CSS Quiz One"         1 
         *  2               "HTML/CSS Quiz Two"         1 
         *  3               "HTML/CSS Quiz Three"       1 
         *  4               "Javascript Quiz One"       2
         *  5               "Javascript Quiz Two"       2
         *  6               "Javascript Quiz Three"     2
         *  7               "HTML/CSS Lesson 1"         1
         *  8               "HTML/CSS Lesson 2"         1
         *  9               "HTML/CSS Lesson 3"         1
         *  10              "HTML/CSS Lesson 4"         1
         *  11              "HTML/CSS Lesson 5"         1
         *  12              "Javascript Lesson 1"       2
         *  13              "Javascript Lesson 2"       2
         *  14              "Javascript Lesson 3"       2
         *  15              "Javascript Lesson 4"       2
         *  16              "Javascript Lesson 5"       2
         * 
         */


        public bool InsertAssessmentScore(String sectionName, String username, double p)
        {
            return false;
        }

        public bool InsertLessonCompletion(String sectionName, String username)
        {
            return false;
        }





        public ProgressAndAssessmentViewModel FetchProgressResults(String uname)
        {
            ProgressAndAssessmentViewModel paavm = new ProgressAndAssessmentViewModel();

            paavm.CompletedLessons = new List<string>();
            paavm.AssessmentScores = new Dictionary<String, Int16>();

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
