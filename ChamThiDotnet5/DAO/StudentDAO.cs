using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class StudentDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public int AddNewStudent(Student Student)
        {
            int n = 0;
            try
            {
                DbContext.Students.Add(Student);
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }

        public List<Student> ReadAllStudent()
        {
            IQueryable<Student> Students = from a in DbContext.Students select a;
            foreach (Student Student in Students)
            {
                var e = DbContext.Entry(Student);
                e.Reference(a => a.Class).Load();
                e.Reference(a => a.Account).Load();
                e.Collection(a => a.Exam_Students).Load();

            }
            return Students.ToList();

        }

        public Student ReadAStudent(int id)
        {
            Student Student = (from a in DbContext.Students where a.Id == id select a).FirstOrDefault();
            if (Student != null)
            {
                var e = DbContext.Entry(Student);
                e.Reference(a => a.Class).Load();
                e.Reference(a => a.Account).Load();
                e.Collection(a => a.Exam_Students).Load();
            }
            return Student;
        }

        // return false co nghia id khong ton tai
        public int UpdateStudent(int id, Student NewStudent)

        {
            int n = 0;
            Student Student = ReadAStudent(id);
            if (Student == null) return n;
            Student = NewStudent;


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
        public int DeleteStudent(int id)
        {
            int n = 0;
            Student Student = ReadAStudent(id);
            if (Student != null)
                DbContext.Students.Remove(Student);
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
