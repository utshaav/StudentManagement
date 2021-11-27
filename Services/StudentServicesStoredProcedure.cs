using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentServicesStoredProcedure
    {
        DataAccessLayer dataAccess = new DataAccessLayer();

        public List<Student> GetAllStudents(){
            return dataAccess.GetAllStudent();
        }

        public Student GetStudent(int id){
            return dataAccess.GetStudent(id);
        }

        public void UpdateStudent(Student student){
            dataAccess.UpdateStudent(student);
        }

        public int CountStudents(){
            return dataAccess.CountStudents();
        }

        public void AddStudent(Student student){
            dataAccess.CreateStudent(student);
            
        }

        public void DeleteStudent(int id){
            dataAccess.DeleteStudent(id);
        }

    }
}