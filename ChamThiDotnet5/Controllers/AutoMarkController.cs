using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


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
        public  IActionResult AutoMark(int ClassId , int ExamId)
        {
            //check classid, examid
            Console.WriteLine(ClassId + "_" + ExamId);
            

            List<Exam_Student> list = _Exam_StudentService.FindStudent_ExamByClassAndExamID(ClassId, ExamId);
            string DuongDanTestCase = @"C:\PRNChamThi\" + ClassId + "_" + ExamId + @"\testcase";

            //lay thong tin toan bo cac qes
            int SoCauHoi = Directory.GetFiles(DuongDanTestCase, "*", SearchOption.AllDirectories).Length;
            
            
            for(int i = 1; i <= SoCauHoi; i++)
            {
                string[] Q = System.IO.File.ReadAllLines(DuongDanTestCase+@"\Q"+i);// tat ca cac dong trong Q cho truoc
                ArrayList inputs = new ArrayList();// chua cac input cua cac testcase
                ArrayList outputs = new ArrayList();// chua cac output cua cac testcase
                ArrayList marks = new ArrayList();// chua diem cua cac testcase
                foreach (string s in Q)
                {
                    if(s.Contains("INPUT:"))
                        inputs.Add(s.Substring(6));
                    if (s.Contains("OUTPUT:"))
                        outputs.Add(s.Substring(6));
                    if (s.Contains("MARK:"))
                        outputs.Add(s.Substring(5));

                }
            }

           
            
            //doc file va lay du lieu cac phan
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
            ViewBag.ans = list;
            return View();
        }
        private int MarkOnATesecase(string path, string inputList , string output)
        {

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(@"cd \");
            
            cmd.StandardInput.WriteLine("java -jar " + path);
            string[] inputs = inputList.Split("|");
            foreach (string i in inputs)
            {
                cmd.StandardInput.WriteLine(i);
            }
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            //cmd.WaitForExit();
            string txt = cmd.StandardOutput.ReadToEnd();

            //cham diem

            return 1;
        }
        private ArrayList SeperateInput(string file)
        {
            ArrayList ans = new ArrayList();

            return ans;

        }
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
