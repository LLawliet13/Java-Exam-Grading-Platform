﻿using Aspose.Zip.Rar;
using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace ChamThiDotnet5.Controllers
{
    public class LoadfileController : Controller
    {
        private ExamDAO examDAO = new ExamDAO();
        public LoadfileController()
        {
        }

        public string getDefaultFilePath()
        {
            string dirPath = "C:/PRNChamThi/DeThi";

            bool exist = Directory.Exists(dirPath);

            // Nếu không tồn tại, tạo thư mục này.
            if (!exist)
            {
                // Tạo thư mục.
                Directory.CreateDirectory(dirPath);
            }

            return dirPath;
        }
        
        public FilesViewModel Index()
        {
            // Get files from the server
            var model = new FilesViewModel();
            //foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "upload")))
            foreach (var item in Directory.GetFiles(Path.Combine(getDefaultFilePath())))
            {
                string iname = System.IO.Path.GetFileName(item);
               //if (iname.Contains(".rar") || iname.Contains(".zip"))
                    model.Files.Add(
                        new FileDetails { Name = iname, Path = item });
            }
            return model;
        }

        public IActionResult Load()
        {
            // Get files from the server
            var model = new FilesViewModel();
            //foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "upload")))
            foreach (var item in Directory.GetFiles(Path.Combine(getDefaultFilePath())))
            {
                string iname = System.IO.Path.GetFileName(item);
                //if (iname.Contains(".rar") || iname.Contains(".zip"))
                model.Files.Add(
                    new FileDetails { Name = iname, Path = item });
            }
            return View("~/Views/Teacher/ExamBankList.cshtml", model);
        }

        public void UnzipFile(string filePath, string directory)
        {
            if (filePath.Contains(".rar") || filePath.Contains(".zip"))
            {
                using (RarArchive archive = new RarArchive(filePath))
                {
                    archive.ExtractToDirectory(directory);
                }
                
            }
        }

        [HttpPost]
        public IActionResult Index(IFormFile[] fileDe, IFormFile[] fileTest)
        {
            string name = null, detail = null, testcase = null;
            

            // Iterate each files
            foreach (var file in fileDe)
            {
                // Get the file name from the browser
                var fileName = System.IO.Path.GetFileName(file.FileName);
                name = file.FileName;
                name = name.Substring(0, name.Length-4);
                // Get file path to be uploaded
                var filePath = Path.Combine(getDefaultFilePath(), fileName);
                detail = getDefaultFilePath() + $@"/{fileName}";
                // Check If file with same name exists and delete it
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Create a new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }
            foreach (var file in fileTest)
            {
                // Get the file name from the browser
                var fileName = System.IO.Path.GetFileName(file.FileName);

                // Get file path to be uploaded
                var filePath = Path.Combine(getDefaultFilePath(), fileName);
                testcase = getDefaultFilePath()+$@"/{fileName}";
                // Check If file with same name exists and delete it
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Create a new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }
            ViewBag.Message = "Files are successfully uploaded";

            // Get files from the server
            var model = new FilesViewModel();
            foreach (var item in Directory.GetFiles(Path.Combine(getDefaultFilePath())))
            {
                string iname = System.IO.Path.GetFileName(item);
                //if (iname.Contains(".rar") || iname.Contains(".zip"))
                model.Files.Add(
                    new FileDetails { Name = iname, Path = item });
            }
            UnzipFile(testcase,getDefaultFilePath());
            testcase=testcase.Substring(0,testcase.Length-4);
    
            examDAO.AddNewExam(new Exam() { Examname=name, Detail=detail,Testcase=testcase});
            return View("~/Views/Teacher/ExamBankAdd.cshtml", model);
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(getDefaultFilePath(), filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var text = Path.GetExtension(path).ToLowerInvariant();
            return types[text];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".rar", "application/x-rar-compressed"}
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteFile(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var fullPath = Path.Combine(getDefaultFilePath(), filename);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                string path = getDefaultFilePath() + $@"/{filename}";
                TempData["deleted"] = "Files are successfully Deleted";
                var x = examDAO.ReadAExamByLink(path);
                if (x != null)
                {
                    TempData["deleted"] += " - Exam " + x.Examname + " was Unable.";
                    x.Detail = "";
                    examDAO.UpdateExam(x.Id, x);
                }
            }
            var model = new FilesViewModel();
            foreach (var item in Directory.GetFiles(Path.Combine(getDefaultFilePath())))
            {
                model.Files.Add(
                    new FileDetails { Name = System.IO.Path.GetFileName(item), Path = item });
            }
            return View("~/Views/Teacher/ExamBankList.cshtml", model);
        }
    }
}
