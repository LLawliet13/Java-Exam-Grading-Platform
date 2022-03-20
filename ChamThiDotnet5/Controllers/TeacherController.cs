﻿using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.Controllers
{
    public class TeacherController : Controller
    {
        private ClassService _classService;
        private Exam_StudentService _exam_StudentService;
        private TeacherDAO teacherDAO = new TeacherDAO();
        private ClassDAO classDAO = new ClassDAO();
        private StudentDAO studentDAO = new StudentDAO();
        private ExamDAO examDAO = new ExamDAO();
        private Exam_StudentDAO exam_studentDAO=new Exam_StudentDAO(); 
        public TeacherController(ClassService classService, Exam_StudentService exam_StudentService)
        {
            _classService = classService;
            _exam_StudentService = exam_StudentService;
        }
        [HttpGet]
        public IActionResult Teacher()
        {
            //id mac dinh dung trong test
            int id = 10;
            //Dictionary<string, List<Class_Exam>> examList = new Dictionary<string, List<Class_Exam>>();
            List<Class_Exam> Class_Exams = new List<Class_Exam>();
            if (teacherDAO.ReadATeacher(id) != null)
                Class_Exams = _exam_StudentService.FindPending_ResultExamOfTeacher(id);
            ViewBag.Class_Exams = Class_Exams;
            return View();
        }
        [HttpPost]
        public IActionResult getPendingExam_Student(int examid, int classid)
        {
            List<Exam_Student> exam_Students = _exam_StudentService.FindStudent_ExamByClassAndExamID(classid, examid);

            ViewBag.Exam_StudentList = exam_Students;

            return View("StudentList");
        }
        public IActionResult getResultExam_Student(int examid, int classid)
        {
            List<Exam_Student> exam_Students = _exam_StudentService.FindStudent_ExamByClassAndExamID(classid, examid);

            ViewBag.Exam_StudentList = exam_Students;

            return View("StudentList");
        }

        // Select class
        [HttpGet]
        public IActionResult Class(int id)
        {
            int classid = Convert.ToInt32(id);
            if (classid != 0)
            {

                ViewData["ClassId"] = classid;
                ViewData["ClassName"] = classDAO.ReadAClass(classid).Classname;
                var student = studentDAO.ReadAllStudent().ToList();
                int i=0;
                while(i<student.Count)
                {
                    if (student[i].ClassId != classid)
                    {
                        student.Remove(student[i]);
                    }
                    else i++;
                }
                ViewData["StudentNum"] = student.Count; 
                ViewBag.StudentList = student;
                ViewBag.Exam = examDAO.ReadAllExam().ToList();
            }

            var accid = HttpContext.Session.GetString("accountid");
            var teacher = teacherDAO.GetATercherByAccId(Int32.Parse(accid));
            List<Class> classes = _classService.GetClassesByTeacherId(teacher.Id);
            ViewBag.ClassList = classes;

            return View();
        }
        
        // Add exam for all student in selected class
        [HttpPost]
        public IActionResult Class(string id, string exam, string start, string end)
        {
            int classid = Convert.ToInt32(id);
            if (classid != 0)
            {
                ViewData["ClassId"] = classid;
                ViewData["ClassName"] = classDAO.ReadAClass(classid).Classname;
                var student = studentDAO.ReadAllStudent().ToList();
                int i = 0;
                while (i < student.Count)
                {
                    if (student[i].ClassId != classid)
                    {
                        student.Remove(student[i]);
                    }
                    else i++;
                }
                ViewData["StudentNum"] = student.Count;
                ViewBag.StudentList = student;
                if (start != null) {
                    foreach(var s in student)
                    {
                        var exSt = new Exam_Student();
                        exSt.StudentId = s.Id;
                        exSt.ExamId = examDAO.ReadAExam(int.Parse(exam)).Id;
                        exSt.Start = DateTime.Parse(start);
                        exSt.End = DateTime.Parse(end);
                        //return View();
                        exam_studentDAO.AddNewExam_Student(exSt);
                        
                    }
                    ViewData["AddExam"] = "Add exam success!";
                }
                ViewBag.Exam = examDAO.ReadAllExam().ToList();
            }

            var accid = HttpContext.Session.GetString("accountid");
            var teacher = teacherDAO.GetATercherByAccId(Int32.Parse(accid));
            List<Class> classes = _classService.GetClassesByTeacherId(teacher.Id);
            ViewBag.ClassList = classes;

            return View();
        }

    }
}
