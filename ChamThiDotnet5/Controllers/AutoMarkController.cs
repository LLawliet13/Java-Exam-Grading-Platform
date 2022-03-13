using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace ChamThiDotnet5.Controllers
{
  
    public class AutoMarkController : Controller
    {

        private readonly Exam_StudentService _Exam_StudentService;
        private readonly AutoMarkService _AutoMarkService;
        public AutoMarkController(Exam_StudentService exam_StudentService, AutoMarkService autoMarkService)
        {
            _Exam_StudentService = exam_StudentService;
            _AutoMarkService = autoMarkService;
        }
        [HttpPost]
        public IActionResult AutoMark(int ClassId , int ExamId)
        {

            List<Exam_Student> list = _Exam_StudentService.FindStudent_ExamByClassAndExamID(ClassId, ExamId);
            ViewBag.ans = list;
            //if(list.Count != 0)
            //{
            //    // lay trong folder by thi

            //    DirectoryInfo di = new DirectoryInfo("folder path");
            //    // trong truong hop nhieu folder
            //    DirectoryInfo[] innerDi = di.GetDirectories();
            //    DirectoryInfo targetDi = null;
            //    foreach (DirectoryInfo innerDiItem in innerDi)
            //    {
            //        if(innerDiItem.Name == "some name of input output folder")
            //        {
            //            targetDi = innerDiItem;
            //            break;
            //        }
            //    }
            //    // search nhanh link file
            //    //string[] filePaths = Directory.GetFiles(@"c:\MyDir\", "*.bmp",SearchOption.AllDirectories);
                
              
            //    FileInfo[] files = targetDi.GetFiles("*.txt");
            //    string[] input = null;
            //    string[] output = null;
            //    foreach(FileInfo file in files)
            //    {
            //        if(file.Name == "input")
            //        {
            //            input = System.IO.File.ReadAllLines(file.FullName); ;
            //            break;
                           
            //        }

            //        if (file.Name == "output")
            //        {
            //            input = System.IO.File.ReadAllLines(file.FullName); ;
            //            break;

            //        }
            //    }


            //    ViewBag.ans = AutoMarkAll(list, input, output);
            //}

            return View();
        }
        private List<Exam_Student> AutoMarkAll(List<Exam_Student> Exam_Students, string[] input , string[] output)
        {
            // cham thi cho tung hoc sinh
            Exam_Students.ForEach(x => {
                
            });
            

            return Exam_Students;
        }
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
