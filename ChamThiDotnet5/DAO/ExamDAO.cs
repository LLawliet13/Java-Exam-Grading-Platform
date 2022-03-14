using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class ExamDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public int AddNewExam(Exam Exam)
        {
            int n = 0;
            try
            {
                DbContext.Exams.Add(Exam);
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }

        public List<Exam> ReadAllExam()
        {
            IQueryable<Exam> Exams = from a in DbContext.Exams select a;
            foreach (Exam Exam in Exams)
            {
                var e = DbContext.Entry(Exam);
                e.Collection(a => a.Exam_Students).Load();
               
            }
            return Exams.ToList();

        }

        public Exam ReadAExam(int id)
        {
            Exam Exam = (from a in DbContext.Exams where a.Id == id select a).FirstOrDefault();
            if (Exam != null)
            {
                var e = DbContext.Entry(Exam);
                e.Collection(a => a.Exam_Students).Load();
            }
            return Exam;
        }

        // return false co nghia id khong ton tai
        public int UpdateExam(int id, Exam NewExam)

        {
            int n = 0;
            Exam Exam = ReadAExam(id);
            if (Exam == null) return n;
            Exam = NewExam;


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
        public int DeleteExam(int id)
        {
            int n = 0;
            Exam Exam = ReadAExam(id);
            if (Exam != null)
                DbContext.Exams.Remove(Exam);
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
