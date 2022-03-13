using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ChamThiDotnet5.Controllers
{
    public class TeacherController : Controller
    {
        private ClassService _classService;
        private TeacherDAO teacherDAO = new TeacherDAO();
        public TeacherController(ClassService classService)
        {
            _classService = classService;
        }

        public IActionResult Teacher()
        {
            //id mac dinh dung trong test
            int id = 2;
            List<Class> list = teacherDAO.ReadATeacher(id).Classes;
            foreach(Class c in list){
                Console.WriteLine(c.Id);
            }
            ViewBag.ClassList = list;
            
            return View();
        }
    }
}
