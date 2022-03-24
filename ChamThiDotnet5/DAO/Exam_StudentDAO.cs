using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class Exam_StudentDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public int AddNewExam_Student(Exam_Student Exam_Student)
        {
            int n = 0;
            try
            {
                DbContext.Exam_Students.Add(Exam_Student);
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }

        public List<Exam_Student> ReadAllExam_Student()
        {
            IQueryable<Exam_Student> Exam_Students = from a in DbContext.Exam_Students select a;
            foreach (Exam_Student Exam_Student in Exam_Students)
            {
                var e = DbContext.Entry(Exam_Student);
                e.Reference(a => a.Exam).Load();
                e.Reference(a => a.Student).Load();
            }
            return Exam_Students.ToList();

        }

        public Exam_Student ReadAExam_Student(int id)
        {
            Exam_Student Exam_Student = (from a in DbContext.Exam_Students where a.Id == id select a).FirstOrDefault();
            if (Exam_Student != null)
            {
                var e = DbContext.Entry(Exam_Student);
                e.Reference(a => a.Exam).Load();
                e.Reference(a => a.Student).Load();
            }
            return Exam_Student;
        }

        // return false co nghia id khong ton tai
        public int UpdateExam_Student(int id, Exam_Student NewExam_Student)

        {
            int n = 0;
            Exam_Student Exam_Student = ReadAExam_Student(id);
            if (Exam_Student == null) return n;
            Exam_Student.ExamId = NewExam_Student.ExamId;
            Exam_Student.Score = NewExam_Student.Score;
            Exam_Student.StudentId = NewExam_Student.StudentId;
            Exam_Student.SubmittedFolder = NewExam_Student.SubmittedFolder;
            Exam_Student.Start = NewExam_Student.Start;
            Exam_Student.End = NewExam_Student.End;
            try
            {
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;


        }
        public int DeleteExam_Student(int id)
        {
            int n = 0;
            Exam_Student Exam_Student = ReadAExam_Student(id);
            if (Exam_Student != null)
                DbContext.Exam_Students.Remove(Exam_Student);
            try
            {
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }
    }
}
