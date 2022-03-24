using ChamThiDotnet5.Models;
using ChamThiDotnet5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Management;
using ChamThiDotnet5.DAO;

namespace ChamThiDotnet5.Controllers
{

    public class AutoMarkController : Controller
    {
        private Exam_StudentDAO esDao = new Exam_StudentDAO();
        private readonly Exam_StudentService _Exam_StudentService;
        private readonly AutoMarkService _AutoMarkService;
        public AutoMarkController(Exam_StudentService exam_StudentService, AutoMarkService autoMarkService)
        {
            _Exam_StudentService = exam_StudentService;
            _AutoMarkService = autoMarkService;
        }
        [HttpPost]
        public IActionResult AutoMark(int ClassId, int ExamId)
        {
            //check classid, examid
            Console.WriteLine(ClassId + "_" + ExamId);



            string DuongDanTestCase = @"C:\PRNChamThi\" + ClassId + "_" + ExamId + @"\testcase";

            //lay thong tin toan bo cac qes
            int SoCauHoi = Directory.GetFiles(DuongDanTestCase, "*", SearchOption.AllDirectories).Length;
            //danh sach hoc sinh tham gia vao bai thi
            List<Exam_Student> students = _Exam_StudentService.FindStudent_ExamByClassAndExamID(ClassId, ExamId);
            foreach (Exam_Student es in students)
            {
                float totalMark = 0;
                string submittedFolderPath = es.SubmittedFolder;
                if (submittedFolderPath != null && !submittedFolderPath.Equals(""))
                {
                    Console.WriteLine("HOC SINH MANG ID:" + es.Id+" Da nop bai");
                    for (int i = 1; i <= SoCauHoi; i++)
                    {

                        string[] Q = System.IO.File.ReadAllLines(DuongDanTestCase + @"\Q" + i + ".TXT");// tat ca cac dong trong Q cho truoc
                        List<string> inputs = new List<string>();// chua cac input cua tung testcase
                        List<string> outputs = new List<string>();// chua cac output cua cac testcase
                        List<string> marks = new List<string>();// chua diem cua cac testcase
                        foreach (string s in Q)
                        {
                            if (s.Contains("INPUT:"))
                                inputs.Add(s.Substring(6));
                            if (s.Contains("OUTPUT:"))
                                outputs.Add(s.Substring(7));
                            if (s.Contains("MARK:"))
                                marks.Add(s.Substring(5));

                        }

                        string submittedJarFile = submittedFolderPath + @"\Q" + i + @"\dist" + @"\Q" + i + ".jar";

                        if(System.IO.File.Exists(submittedJarFile))

                        for (int j = 0; j < inputs.Count; j++)
                        {
                            
                            totalMark += MarkOnATesecase(submittedJarFile, inputs.ElementAt(j), outputs.ElementAt(j), marks.ElementAt(j));
                        }


                    }
                }
               
                Console.WriteLine("HOC SINH MANG ID:" + es.Id+" Co diem la "+totalMark);
                es.Score = totalMark;
                esDao.UpdateExam_Student(es.Id, es);

            }

         
            ViewBag.ans = students;
            return View();
        }
        private int MarkOnATesecase(string path, string inputList, string output, string mark)
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

            //System.Threading.Thread.Sleep(50);// ngung chuong trinh 50ms
            string[] inputs = inputList.Split("|");
            foreach (string i in inputs)
            {
                cmd.StandardInput.WriteLine(i);
            }
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            bool end = cmd.WaitForExit(500);
            
            if (!end)
            {
                //KillProcessAndChildren(cmd.Id);
                cmd.Kill(true);
                cmd.Close();// xu ly vong lap vo han
                
                return 0;
            }
            string realOutput = cmd.StandardOutput.ReadToEnd();
            if (!realOutput.Contains("OUTPUT:")) return 0; // truong hop bi exception
            string[] txt = realOutput.Split("OUTPUT:");//output
            string[] clearAns = txt[1].Split("\r\n");
            //for(int i = 0; i < clearAns.Length;i ++)
            //{
            //    Console.WriteLine("phan thu "+i+" la start"+clearAns[i]+"end");
            //}
            //Console.WriteLine("OUTPUT: la start" + output + "end");
            //Console.WriteLine("Mark: la start" + mark + "end");
            //cham diem
            if (clearAns[1].Equals(output)) {
                return int.Parse(mark); 
            }
            return 0;
        }

       
        private static void KillProcessAndChildren(int pid)
        {
            // Cannot close 'system idle process'.
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
