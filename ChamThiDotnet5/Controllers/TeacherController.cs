using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private Exam_StudentDAO exam_studentDAO = new Exam_StudentDAO();
        private AccountDAO accountDAO = new AccountDAO();
        private LoadfileController load = new LoadfileController();
        public TeacherController(ClassService classService, Exam_StudentService exam_StudentService)
        {
            _classService = classService;
            _exam_StudentService = exam_StudentService;
        }

        [HttpGet]
        public IActionResult Index(string ID)
        {
            //id mac dinh dung trong test
            if (HttpContext.Session.GetString("accounttype") == null || !HttpContext.Session.GetString("accounttype").Equals("Teacher"))
                return RedirectToAction("Index", "Home");
            //Dictionary<string, List<Class_Exam>> examList = new Dictionary<string, List<Class_Exam>>();
            int teacherId = accountDAO.ReadAAccount(int.Parse(HttpContext.Session.GetString("accountid"))).Teacher.Id;
            List<Class_Exam> Class_Exams = new List<Class_Exam>();
            if (teacherDAO.ReadATeacher(teacherId) != null)
                Class_Exams = _exam_StudentService.FindPending_ResultExamOfTeacher(teacherId);
            ViewBag.Class_Exams = Class_Exams;

            ViewBag.ResultClass_Exams = _exam_StudentService.FindResultExamOfTeacher(teacherId);

            // err validate
            if (HttpContext.Session.GetString("start") != null)
            {
                ViewData["start"] = HttpContext.Session.GetString("start");
                HttpContext.Session.Remove("start");
            }
            if (HttpContext.Session.GetString("end") != null)
            {
                ViewData["end"] = HttpContext.Session.GetString("end");
                HttpContext.Session.Remove("end");
            }
            if (HttpContext.Session.GetString("ID") != null)
            {
                ID = HttpContext.Session.GetString("ID");
                HttpContext.Session.Remove("ID");
            }

            int classid = 0;
            if (ID != null) classid = int.Parse(ID);
            else classid = int.Parse(idClass);
            Class1(classid);
            var model = new FilesViewModel();
            model = load.Index();

            string addtest = HttpContext.Session.GetString("addtest");
            if (addtest != null) if (!addtest.Equals("false"))
                {
                    ViewData["AddExam"] = addtest + ": Add exam success! View detail in Exam Pending.";
                    HttpContext.Session.SetString("addtest", "false");
                }
            ViewData["StudentNum"] = StudentNum;
            ViewBag.StudentList = students;
            ViewBag.Exam = exams;
            ViewData["ClassId"] = classID;
            ViewData["ClassName"] = className;
            ViewBag.ClassList = classes;

            return View("~/Views/Teacher/Index.cshtml",model);
        }
        [HttpPost]
        public IActionResult getPendingExam_Student(int examid, int classid)
        {
            List<Exam_Student> exam_Students = _exam_StudentService.FindStudent_ExamByClassAndExamID(classid, examid);

            ViewBag.Exam_StudentList = exam_Students;

            return View("PendingStudentList");
        }
        public IActionResult getResultExam_Student(int examid, int classid)
        {
            List<Exam_Student> exam_Students = _exam_StudentService.FindStudent_ExamByClassAndExamID(classid, examid);

            ViewBag.Exam_StudentList = exam_Students;

            return View("StudentList");
        }

        int StudentNum = 0;
        List<Student> students;
        List<Exam> exams;
        List<Class> classes;
        int classID = 0;
        string className;
        string idClass = "0";
        public void Class1(int id)
        {
            int classid = Convert.ToInt32(id);
            if (classid != 0)
            {
                classID = classid;
                className = classDAO.ReadAClass(classid).Classname;
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
                StudentNum = student.Count;
                students = student;
                exams = examDAO.ReadAllExam().ToList();
                i = 0;
                while (i < exams.Count)
                {
                    if (exams[i].Detail == null || exams[i].Testcase == null)
                    {
                        exams.Remove(exams[i]);
                    }
                    else i++;
                }
            }

            var accid = HttpContext.Session.GetString("accountid");
            if (accid == null) accid = "1";
            var teacher = teacherDAO.GetATercherByAccId(Int32.Parse(accid));
            classes = _classService.GetClassesByTeacherId(teacher.Id);
        }


        [HttpPost]
        public IActionResult AddTest(string classid, string exam, string start, string end)
        {
            DateTime now = DateTime.Now;
            DateTime st = DateTime.Parse(start);
            DateTime en = DateTime.Parse(end);
            bool check = true;
            if (st < now)
            {
                check = false;
                HttpContext.Session.SetString("start", "Start time must be greater than current time!");
            }
            if (en < st)
            {
                check = false;
                HttpContext.Session.SetString("end", "End time must be greater than the start time!");
            }
            if (check == false)
            {
                HttpContext.Session.SetString("ID", classid);
                return RedirectToAction("Index", "Teacher", classid);
            }
            int Classid = Convert.ToInt32(classid);
            if (Classid != 0)
            {
                className = classDAO.ReadAClass(Classid).Classname;
                var student = studentDAO.ReadAllStudent().ToList();
                int i = 0;
                while (i < student.Count)
                {
                    if (student[i].ClassId != Classid)
                    {
                        student.Remove(student[i]);
                    }
                    else i++;
                }
                if (start != null)
                {
                    foreach (var s in student)
                    {
                        var exSt = new Exam_Student();
                        exSt.StudentId = s.Id;
                        exSt.ExamId = examDAO.ReadAExam(int.Parse(exam)).Id;
                        exSt.Start = DateTime.Parse(start);
                        exSt.End = DateTime.Parse(end);
                        exam_studentDAO.AddNewExam_Student(exSt);
                    }
                }
                HttpContext.Session.SetString("addtest", className);
            }
            idClass = classid;

            return RedirectToAction("Index");
        }


        public IActionResult updateExamResultTable()
        {
            int teacherId = accountDAO.ReadAAccount(int.Parse(HttpContext.Session.GetString("accountid"))).Teacher.Id;
            ViewBag.ResultClass_Exams = _exam_StudentService.FindResultExamOfTeacher(teacherId);
            return View("ResultExamTable");
        }
        public IActionResult updatePendingExamTable()
        {

            int teacherId = accountDAO.ReadAAccount(int.Parse(HttpContext.Session.GetString("accountid"))).Teacher.Id;
            ViewBag.Class_Exams = _exam_StudentService.FindPending_ResultExamOfTeacher(teacherId);

            return View("PendingExamTable");
        }
        [HttpPost]
        public IActionResult updateResultStudentList(int ClassId, int ExamID)
        {
            //danh sach diem + hoc sinh cua 1 lop da duoc cham
            List<Exam_Student> exam_Students = _exam_StudentService.FindStudent_ExamByClassAndExamID(ClassId, ExamID);

            ViewBag.Exam_StudentList = exam_Students;
            return View("ResultStudentList");
        }
        [HttpGet]
        public IActionResult getPendingExam_StudentList()
        {
            List<Exam_Student> exam_Students = new List<Exam_Student>();

            ViewBag.Exam_StudentList = exam_Students;

            return View("PendingStudentList");
        }
    }
}
