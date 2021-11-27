using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    // [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        StudentServicesStoredProcedure studentServices;
        public StudentController(ILogger<StudentController> logger, StudentContext db)
        {
            studentServices = new StudentServicesStoredProcedure();
            _logger = logger;
        }
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public IActionResult Index()
        {
            ViewBag.Count = studentServices.CountTotal();
            List<Student> students = studentServices.GetAllStudents();
            return View(students);
        }

        public IActionResult Detail(int id)
        {
            Student student = studentServices.GetStudent(id);
            return View(student);
        }
        public IActionResult Add()
        {
            // Console.WriteLine("here yeah");
            ViewBag.GenderDD = getGenderDD();
            return PartialView(new Student());
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            Console.WriteLine(student.StudentName);
            if (ModelState.IsValid)
            {
                studentServices.AddStudent(student);
                return RedirectToActionPermanent("Index");    
            }
            ViewBag.GenderDD = getGenderDD();
            return View();
        }

        public IActionResult Update(int id)
        {
            ViewBag.GenderDD = getGenderDD();
            Student student = studentServices.GetStudent(id);
            return PartialView(student);
        }

        [HttpPost]
        public IActionResult Update(Student student){
            studentServices.UpdateStudent(student);
            // Console.WriteLine(result);
            return RedirectPermanent("Index");
        }

        public IActionResult Delete(int id)
        {
            Student student = studentServices.GetStudent(id);
            return PartialView(student);
        }

        [HttpPost]
        public IActionResult Delete(Student student) {
            studentServices.DeleteStudent(student.StudentId);
            return RedirectToActionPermanent("Index");
        }

        public IActionResult ConfirmDelete(int id)
        {
            studentServices.DeleteStudent(id);
            return RedirectToActionPermanent("Index");
        }

        private List<SelectListItem> getGenderDD()
        {
            return new List<SelectListItem>{
               new SelectListItem{Value="Male", Text="Male"},
               new SelectListItem{Value="Female", Text="Female"},
               new SelectListItem{Value="other", Text="Other"}
            };
        }
    }
}