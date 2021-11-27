using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Data;
using StudentManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace StudentManagement.Services
{
    public class StudentServices
    {
        private readonly StudentContext _db;
        public StudentServices(StudentContext db)
        {
            _db = db;
            
        }
        public List<Student> GetAllStudents(){
            string sql = "EXEC GetStudents";
            return _db.Students.FromSqlRaw(sql).ToList(); 
        }

        public Student GetStudent(int id){
            string sql = $"EXEC GetStudent {id}";
            List<Student> stds = _db.Students.FromSqlRaw(sql).ToList();
            return stds[0]; 
        }

        public void UpdateStudent(Student student){
            string sql = $"EXEC UpdateStudent @StudentId={student.StudentId}," +
                $"@StudentName='{student.StudentName}', @Email='{student.Email}'," +
                $"@mobile='{student.Mobile}', @gender='{student.Gender}', @address='{student.Address}'";
            // return ExecuteQuery(sql);
             _db.Students.Attach(student);
             _db.Students.Update(student);
             _db.SaveChanges();
        }

        public int CountTotal(){
            // var total = _db.Students.FromSqlRaw("EXEC CountTotal");
            int total = _db.Students.Count();
            Console.WriteLine(total);
            return total;
        }

        public void AddStudent(Student student){
            string sql = $"EXEC AddStudent " +
                $"@StudentName='{student.StudentName}', @Email='{student.Email}'," +
                $"@mobile='{student.Mobile}', @gender='{student.Gender}', @address='{student.Address}'";

            // return ExecuteQuery(sql);
            _db.Students.Add(student);
            _db.SaveChanges();
            
        }

        public void DeleteStudent(int id){
            string sql = $"EXEC DeleteStudent {id}";
            ExecuteQuery(sql);
            //Student student = GetStudent(id);
            //_db.Students.Remove(student);
            //_db.SaveChanges();
        }

        public void DeleteStudent(Student student){
            _db.Students.Attach(student);
            _db.Students.Remove(student);
            _db.SaveChanges();
        }

        private string ExecuteQuery(string sql){
            try{
                _db.Students.FromSqlRaw(sql);
                _db.SaveChangesAsync();
                return "Success";
            } catch{
                return "Fail";
            }
        }

    }
}