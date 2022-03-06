using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class ClassDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public int AddNewClass(Class Class)
        {
            int n = 0;
            try
            {
                DbContext.Classes.Add(Class);
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }

        public DbSet<Class> ReadAllClass()
        {
            return DbContext.Classes;

        }

        public List<Class> ReadAllClases()
        {
            IQueryable<Class> Classes = from a in DbContext.Classes select a;
            foreach (Class Class in Classes)
            {
                var e = DbContext.Entry(Class);
                e.Reference(a => a.Teacher);
                e.Reference(a => a.Student);
            }
            return Classes.ToList();

        }

        public Class ReadAClass(int id)
        {
            Class Class = (from a in DbContext.Classes where a.Id == id select a).FirstOrDefault();
            if (Class != null)
            {
                var e = DbContext.Entry(Class);
                e.Reference(a => a.Teacher);
                e.Reference(a => a.Student);
            }
            return Class;
        }

        // return false co nghia id khong ton tai
        public int UpdateClass(int id, Class NewClass)

        {
            int n = 0;
            Class Class = ReadAClass(id);
            if (Class == null) return n;
            Class = NewClass;


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
        public int DeleteClass(int id)
        {
            int n = 0;
            Class Class = ReadAClass(id);
            if (Class != null)
                DbContext.Classes.Remove(Class);
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
