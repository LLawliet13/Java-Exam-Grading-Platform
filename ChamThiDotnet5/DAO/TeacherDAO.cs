using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class TeacherDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public void AddNewTeacher(Teacher Teacher)
        {
            DbContext.Teachers.Add(Teacher);
            DbContext.SaveChanges();
        }

        public DbSet<Teacher> ReadAllTeacher()
        {
            return DbContext.Teachers;

        }

        public Teacher ReadATeacher(int id)
        {
            var Teacher = from a in DbContext.Teachers where a.Id == id select a;
            return (Teacher)Teacher;
        }

        // return false co nghia id khong ton tai
        public bool UpdateTeacher(int id, Teacher NewTeacher)

        {
            Teacher Teacher = ReadATeacher(id);
            if (Teacher == null) return false;
            Teacher = NewTeacher;
            DbContext.SaveChanges();
            return true;
        }
        public void DeleteTeacher(int id)
        {
            Teacher Teacher = ReadATeacher(id);
            if (Teacher != null)
                DbContext.Teachers.Remove(Teacher);
            DbContext.SaveChanges();
        }
    }
}
