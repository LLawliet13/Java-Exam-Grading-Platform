using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using System.Collections.Generic;

namespace ChamThiDotnet5.Services
{

    public class ClassServiceImpl : ClassService
    {
        private ClassDAO classDAO = new ClassDAO();
        public List<Class> GetClassesByTeacherId(int id)
        {
            List<Class> classes = new List<Class>();
            classDAO.ReadAllClases().ForEach(cl =>
            {
                if (cl.TeacherId == id)classes.Add(cl);
            });
            return classes;
        }

    }
}
