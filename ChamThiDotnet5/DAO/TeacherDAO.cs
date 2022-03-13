using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class TeacherDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public int AddNewTeacher(Teacher Teacher)
        {
            int n = 0;
            try
            {
                DbContext.Teachers.Add(Teacher);
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }

        public List<Teacher> ReadAllTeacher()
        {
            IQueryable<Teacher> Teachers = from a in DbContext.Teachers select a;
            foreach (Teacher Teacher in Teachers)
            {
                var e = DbContext.Entry(Teacher);
                e.Collection(a => a.Classes).Load();
                e.Reference(a => a.Account).Load();
                

            }
            return Teachers.ToList();
            return Teachers.ToList();

        }

        public Teacher ReadATeacher(int id)
        {
            Teacher Teacher = (from a in DbContext.Teachers where a.Id == id select a).FirstOrDefault();
            if(Teacher != null) { 
            var e = DbContext.Entry(Teacher);
            e.Collection(a => a.Classes).Load();
            e.Reference(a => a.Account).Load();
            }
            return Teacher;
        }

        // return false co nghia id khong ton tai
        public int UpdateTeacher(int id, Teacher NewTeacher)

        {
            int n = 0;
            Teacher Teacher = ReadATeacher(id);
            if (Teacher == null) return n;
            Teacher = NewTeacher;


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
        public int DeleteTeacher(int id)
        {
            int n = 0;
            Teacher Teacher = ReadATeacher(id);
            if (Teacher != null)
                DbContext.Teachers.Remove(Teacher);
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
