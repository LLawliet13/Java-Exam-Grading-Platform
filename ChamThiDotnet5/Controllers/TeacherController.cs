using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ChamThiDotnet5.Controllers
{
    public class TeacherController : Controller
    {
        private ClassService _classService;
        private Exam_StudentService _exam_StudentService;
        private TeacherDAO teacherDAO = new TeacherDAO();
        public TeacherController(ClassService classService, Exam_StudentService exam_StudentService)
        {
            _classService = classService;
            _exam_StudentService = exam_StudentService;
        }
        [HttpGet]
        public IActionResult Teacher()
        {
            //id mac dinh dung trong test
            int id = 2;
            Dictionary<string, List<Class_Exam>> examList = new Dictionary<string, List<Class_Exam>>();
            List<Class_Exam> Class_Exams = _exam_StudentService.FindPending_ResultExamOfTeacher(id);
            ViewBag.Class_Exams = Class_Exams;
            return View();
        }
        [HttpPost]
        public IActionResult getPendingExam_Student(int examid , int classid)
        {
            List<Exam_Student> exam_Students = _exam_StudentService.FindStudent_ExamByClassAndExamID(classid,examid);
            
            ViewBag.Exam_StudentList = exam_Students;

            return View("StudentList");
        }
        public IActionResult getResultExam_Student(int examid, int classid)
        {
            List<Exam_Student> exam_Students = _exam_StudentService.FindStudent_ExamByClassAndExamID(classid, examid);

            ViewBag.Exam_StudentList = exam_Students;

            return View("StudentList");
        }
    }
}
