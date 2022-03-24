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
        private Exam_StudentDAO exam_studentDAO=new Exam_StudentDAO(); 
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
            if (HttpContext.Session.GetString("accounttype")==null||!HttpContext.Session.GetString("accounttype").Equals("Teacher"))
                return RedirectToAction("Index", "Home");
            //Dictionary<string, List<Class_Exam>> examList = new Dictionary<string, List<Class_Exam>>();
            int teacherId = accountDAO.ReadAAccount( int.Parse(HttpContext.Session.GetString("accountid"))).Teacher.Id;
            List<Class_Exam> Class_Exams = new List<Class_Exam>();
            if (teacherDAO.ReadATeacher(teacherId) != null)
                Class_Exams = _exam_StudentService.FindPending_ResultExamOfTeacher(teacherId);
            ViewBag.Class_Exams = Class_Exams;

            ViewBag.ResultClass_Exams = _exam_StudentService.FindResultExamOfTeacher(teacherId);
            

            int classid = 0;
            if (ID != null) classid = int.Parse(ID);
            Class1(classid);

            var model = new FilesViewModel();
            model = load.Index();
            ViewData["StudentNum"] = StudentNum;
            ViewBag.StudentList = students;
            ViewBag.Exam = exams;
            ViewData["ClassId"] = classID;
            ViewData["ClassName"] = className;
            ViewBag.ClassList = classes;
            return View(model);
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
        int classID =0;
        string className;

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
            }

            var accid = HttpContext.Session.GetString("accountid");
            if (accid == null) accid = "1";
            var teacher = teacherDAO.GetATercherByAccId(Int32.Parse(accid));
            classes = _classService.GetClassesByTeacherId(teacher.Id);
          
 
        }



        // Select class
        public IActionResult Class(int id)
        {
            int classid = Convert.ToInt32(id);
            if (classid != 0)
            { 
                classID = classid;
                className = classDAO.ReadAClass(classid).Classname;
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
                StudentNum = student.Count; 
                students = student;
                exams = examDAO.ReadAllExam().ToList();
            }

            var accid = HttpContext.Session.GetString("accountid");
            if (accid == null) accid = "1";
            var teacher = teacherDAO.GetATercherByAccId(Int32.Parse(accid));
            classes = _classService.GetClassesByTeacherId(teacher.Id);
            ViewData["StudentNum"] = StudentNum;
            ViewBag.StudentList = students;
            ViewBag.Exam = exams;
            ViewData["ClassId"] = classID;
            ViewData["ClassName"] = className;
            ViewBag.ClassList = classes;
            return View();
        }
        
        // Add exam for all student in selected class
        [HttpPost]
        public IActionResult Class(string id, string exam, string start, string end)
        {
            //int classid = Convert.ToInt32(id);
            //if (classid != 0)
            //{
            //    ViewData["ClassId"] = classid;
            //    ViewData["ClassName"] = classDAO.ReadAClass(classid).Classname;
            //    var student = studentDAO.ReadAllStudent().ToList();
            //    int i = 0;
            //    while (i < student.Count)
            //    {
            //        if (student[i].ClassId != classid)
            //        {
            //            student.Remove(student[i]);
            //        }
            //        else i++;
            //    }
            //    //ViewData["StudentNum"] = student.Count;
            //    ViewBag.StudentList = student;
            //    if (start != null) {
            //        foreach(var s in student)
            //        {
            //            var exSt = new Exam_Student();
            //            exSt.StudentId = s.Id;
            //            exSt.ExamId = examDAO.ReadAExam(int.Parse(exam)).Id;
            //            exSt.Start = DateTime.Parse(start);
            //            exSt.End = DateTime.Parse(end);
            //            //return View();
            //            exam_studentDAO.AddNewExam_Student(exSt);
                        
            //        }
            //    }
            //    ViewData["AddExam"] = ViewData["ClassName"] + ": Add exam success! View detail in Exam Pending.";
            //    ViewBag.Exam = examDAO.ReadAllExam().ToList();
            //}

            //var accid = HttpContext.Session.GetString("accountid");
            //var teacher = teacherDAO.GetATercherByAccId(Int32.Parse(accid));
            //List<Class> classes = _classService.GetClassesByTeacherId(teacher.Id);
            //ViewBag.ClassList = classes;

            return View("~/Teacher/ExamPending.cshtml");
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
        public IActionResult updateResultStudentList(int ClassId,int ExamID)
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
