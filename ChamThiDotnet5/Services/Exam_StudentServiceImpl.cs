using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.Services
{
    public class Exam_StudentServiceImpl : Exam_StudentService
    {
        Exam_StudentDAO exam_Studentsdao = new Exam_StudentDAO();
        TeacherDAO teacherDAO = new TeacherDAO();
        ClassDAO ClassDAO = new ClassDAO();
        StudentDAO studentDAO = new StudentDAO();
        //ham tra ve danh sach tat ca cac lop co bai kiem tra chua duoc cham cua 1 giao vien
        public List<Class_Exam> FindPending_ResultExamOfTeacher(int id)
        {
            List<Class_Exam> class_Exams = new List<Class_Exam>();
            Teacher t = teacherDAO.ReadATeacher(id);
            // lay danh sach cac lop cua giao vien
            List<Class> classList = t.Classes;

            if (classList != null)
            {
                // tim cac bai kiem tra cac lop nay duoc giao
                foreach (Class c in classList)
                {
                    //lay thong tin ve class nay
                    Class cInfo = ClassDAO.ReadAClass(c.Id);
                    if (cInfo.Student.Count > 0)
                    {
                        // vi cac hs deu duoc giao bai nhu nhau nen lay dien hinh 1 hs
                        Student student = cInfo.Student.ElementAt(0);
                        Student studentInfo = studentDAO.ReadAStudent(student.Id);
                        foreach (Exam_Student es in studentInfo.Exam_Students)
                        {
                            //chua co diem nghia la chua cham
                            Exam_Student esInfo = exam_Studentsdao.ReadAExam_Student(es.Id);
                            if (esInfo.Score == null) 
                            class_Exams.Add(new Class_Exam { Class = cInfo, Exam = esInfo.Exam });
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No Classes Found");
            }
            return class_Exams;
        }

        // lay thong tin danh sach hoc sinh theo class id va exam id 
        public List<Exam_Student> FindStudent_ExamByClassAndExamID(int ClassID, int ExamID)
        {

            List<Exam_Student> exam_Students = new List<Exam_Student>();

            exam_Studentsdao.ReadAllExam_Student().ForEach(x =>
            {

                if (x.Exam.Id == ExamID && x.Student.ClassId == ClassID)
                {
                    exam_Students.Add(x);
                }
            });

            return exam_Students;
        }

        //ham tra ve danh sach tat ca cac lop co bai kiem tra duoc cham cua 1 giao vien
        public List<Class_Exam> FindResultExamOfTeacher(int id)
        {
            List<Class_Exam> class_Exams = new List<Class_Exam>();
            Teacher t = teacherDAO.ReadATeacher(id);
            // lay danh sach cac lop cua giao vien
            List<Class> classList = t.Classes;

            if (classList != null)
            {
                // tim cac bai kiem tra cac lop nay duoc giao
                foreach (Class c in classList)
                {
                    //lay thong tin ve class nay
                    Class cInfo = ClassDAO.ReadAClass(c.Id);
                    if (cInfo.Student.Count > 0)
                    {
                        // vi cac hs deu duoc giao bai nhu nhau nen lay dien hinh 1 hs
                        Student student = cInfo.Student.ElementAt(0);
                        Student studentInfo = studentDAO.ReadAStudent(student.Id);
                        foreach (Exam_Student es in studentInfo.Exam_Students)
                        {
                            //chua co diem nghia la chua cham
                            Exam_Student esInfo = exam_Studentsdao.ReadAExam_Student(es.Id);
                            if (esInfo.Score != null) 
                            class_Exams.Add(new Class_Exam { Class = cInfo, Exam = esInfo.Exam });
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No Classes Found");
            }
            return class_Exams;
        }
    }
}
