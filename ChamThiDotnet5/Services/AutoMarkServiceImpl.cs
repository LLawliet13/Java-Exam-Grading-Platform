using System.Collections.Generic;
using System.Diagnostics;

namespace ChamThiDotnet5.Services
{
    public class AutoMarkServiceImpl : AutoMarkService
    {
        private List<double> marks = new List<double>();
        private List<string> ExpectedOutputs = new List<string>();
        public int CalScore(string input, string expectedOutput, string SubmittedFolder)
        {
            int score = 0;
            //khoi tao process chay chay terminal
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;

            // khoi chay chuong trinh 
            cmd.Start();


            // sau dien link theo submittedfolder
            string path = @"C:\Users\DELL\Documents\NetBeansProjects\testParse\dist\testParse";

            // yeu cau may chu co jvm
            cmd.StandardInput.WriteLine("java -jar " + path + ".jar");


            //cmd.StandardInput.WriteLine(@"cd \");
            //cmd.StandardInput.WriteLine(@"cd /d " + txtClass.Text);
            //cmd.StandardInput.WriteLine("cd " + st);
            //cmd.StandardInput.WriteLine(@"cd Final-" + listQ[i]);
            //cmd.StandardInput.WriteLine("cd " + listQ[i] + "1");
            //cmd.StandardInput.WriteLine(@"cd Given\dist");
            //cmd.StandardInput.WriteLine("java -jar " + listQ[i] + "1" + ".jar");

            //chia cac input , output ra theo cau
            List<string> inputs = SeperateInput(input);
            List<string> expectedOututs = SeperateInput(expectedOutput);

            // nhap input
            foreach(string i in inputs) { 
                cmd.StandardInput.WriteLine(i);
            }


            //xoa du lieu trong trinh nhap vao
            cmd.StandardInput.Close();
            string output = cmd.StandardOutput.ReadToEnd();

            //cham diem



            if (expectedOutput.Equals(output))
            {
                score = 10;
            }
            

            return score;
        }

        // chia nho gia tri nhap vao
        public List<string> SeperateInput(string input)
        {
            List<string> Seperate = new List<string>();
            //chia nho ra thanh cac cau
            return Seperate;
        }
        public void SeperateExpectedOutput(string ExOutput)
        {
            // lay diem 
            // lay output 


        }
       


    }
}
