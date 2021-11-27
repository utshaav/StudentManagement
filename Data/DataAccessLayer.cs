using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    public class DataAccessLayer
    {
        private string connectionString = "data source=.;Initial Catalog=StudentManagement;Integrated Security=True";

        public string CreateStudent(Student student)
        {
            SqlConnection connection = null;
            string result = "";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand sql = new SqlCommand("AddStudent", connection);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@StudentName", student.StudentName);
                sql.Parameters.AddWithValue("@Email", student.Email);
                sql.Parameters.AddWithValue("@mobile", student.Mobile);
                sql.Parameters.AddWithValue("@address", student.Address);
                sql.Parameters.AddWithValue("@gender", student.Gender);


                connection.Open();
                result = sql.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                connection.Close();
            }
        }


        public string UpdateStudent(Student student)
        {
            SqlConnection connection = null;
            string result = "";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand sql = new SqlCommand("UpdateStudent", connection);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@StudentId", student.StudentId);
                sql.Parameters.AddWithValue("@StudentName", student.StudentName);
                sql.Parameters.AddWithValue("@Email", student.Email);
                sql.Parameters.AddWithValue("@mobile", student.Mobile);
                sql.Parameters.AddWithValue("@address", student.Address);
                sql.Parameters.AddWithValue("@gender", student.Gender);
                connection.Open();
                sql.ExecuteScalar();
                return "Done";
            }
            catch
            {
                return result = "";
            }
            finally
            {
                connection.Close();
            }
        }

        public int DeleteStudent(int id)
        {
            SqlConnection connection = null;
            int result;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand sql = new SqlCommand("DeleteStudent", connection);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@StudentId", id);
                connection.Open();
                result = sql.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }



        public Student GetStudent(int id)
        {
            SqlConnection connection = null;
            DataSet ds = null;
            Student student = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand sql = new SqlCommand("GetStudent", connection);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@StudentId", id);
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sql;
                ds = new DataSet();
                da.Fill(ds);
                student = new Student
                {
                    StudentId = Convert.ToInt32(ds.Tables[0].Rows[0]["StudentId"].ToString()),
                    StudentName = ds.Tables[0].Rows[0]["StudentName"].ToString(),
                    Address = ds.Tables[0].Rows[0]["Address"].ToString(),
                    Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString(),
                    Gender = ds.Tables[0].Rows[0]["Gender"].ToString(),
                    Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                };
                return student;
            }
            catch
            {
                return student;
            }
            finally
            {
                connection.Close();
            }
        }

        public int CountStudents()
        {
            SqlConnection connection = null;
            DataSet ds = null;
            Student student = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand sql = new SqlCommand("CountTotal", connection);
                sql.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sql;
                ds = new DataSet();
                da.Fill(ds);
                
                return Convert.ToInt32(ds.Tables[0].Rows[0]["Total"].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Student> GetAllStudent()
        {
            SqlConnection connection = null;
            DataSet ds = null;
            List<Student> students = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand sql = new SqlCommand("GetStudents", connection);
                sql.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sql;
                ds = new DataSet();
                da.Fill(ds);
                students = new List<Student>();
                for (int i = 0;i < ds.Tables[0].Rows.Count; i++)
                {
                    Student student = new Student
                    {
                        StudentId = Convert.ToInt32(ds.Tables[0].Rows[i]["StudentId"].ToString()),
                        StudentName = ds.Tables[0].Rows[i]["StudentName"].ToString(),
                        Address = ds.Tables[0].Rows[i]["Address"].ToString(),
                        Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString(),
                        Gender = ds.Tables[0].Rows[i]["Gender"].ToString(),
                        Email = ds.Tables[0].Rows[i]["Email"].ToString(),
                    };
                    students.Add(student);
                }

                return students;
            }
            catch
            {
                return students;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}