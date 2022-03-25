using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChamThiDotnet5.Controllers
{
    public class StudentController : Controller
    {

        private StudentDAO daoStudent = new StudentDAO();
        private Exam_StudentDAO daoExamStudent = new Exam_StudentDAO();
        private ExamDAO daoExam = new ExamDAO();

        public IActionResult StudentExam()
        {
            string accountId = HttpContext.Session.GetString("accountid");
            int id = int.Parse(accountId);
            List<Exam_Student> listExamStudent = daoExamStudent.ReadAllExam_Student();
            List<Exam_Student> listExamExist = new List<Exam_Student>();
            foreach (Exam_Student student in listExamStudent)
            {
                    listExamExist.Add(student);
            }
            
            List<Exam> listExam = new List<Exam>();
            foreach (Exam_Student exams in listExamExist)
            {
                listExam.Add(daoExam.ReadAExam(exams.ExamId));
            }
            ViewBag.Exam_List = listExam;
            return View();
        }

        public IActionResult StudentAction(int id)
        {
            Exam exam = daoExam.ReadAExam(id);
            List<Exam_Student> listExamsStudent = daoExamStudent.ReadAllExam_Student();
            foreach (Exam_Student ExamStudent in listExamsStudent)
            {
                if (ExamStudent.ExamId == id)
                {
                    ViewBag.ExamStudent = ExamStudent;
                }
            }

            ViewBag.Exam_Test = exam;
            return View();
        }

        public IActionResult StudentTakeExam(int id)
        {
            Exam exam = daoExam.ReadAExam(id);
            List<Exam_Student> listExamStudent = daoExamStudent.ReadAllExam_Student();
            List<Exam_Student> listExamExist = new List<Exam_Student>();
            foreach (Exam_Student student in listExamStudent)
            {
                listExamExist.Add(student);
            }
            Exam_Student exam_Student_Check = new Exam_Student();
            foreach (Exam_Student student in listExamExist)
            {
                if (student.ExamId == id)
                {
                    exam_Student_Check = student;
                }
            }
            if (exam_Student_Check != null)
            {
                ViewBag.Exam_Student_Check = exam_Student_Check;
            }
            ViewBag.Exam_Test = exam;
            return View();
        }

        [HttpPost]
        public IActionResult StudentSubmit(int id, IFormFile[] files)
        {
            // Iterate each files
            foreach (var file in files)
            {
                // Get the file name from the browser
                var fileName = Path.GetFileName(file.FileName);

                // Get file path to be uploaded
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", fileName);

                // Check If file with same name exists and delete it
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Create a new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }

            // Get files from the server
            var model = new Exam_Student();
            foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Upload")))
            {
                model = new Exam_Student { SubmittedFolder = item.ToString() };
            }


            return View();
        }
    }
}
