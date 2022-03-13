using ChamThiDotnet5.DAO;
using ChamThiDotnet5.Models;
using System.Collections.Generic;

namespace ChamThiDotnet5.Services
{
    public class Exam_StudentServiceImpl : Exam_StudentService
    {

        // lay thong tin danh sach hoc sinh theo class id va exam id 
        public List<Exam_Student> FindStudent_ExamByClassAndExamID(int ClassID, int ExamID)
        {
            Exam_StudentDAO dao = new Exam_StudentDAO();
            List<Exam_Student> exam_Students = new List<Exam_Student>();

            dao.ReadAllExam_Student().ForEach(x => {

                if (x.Exam.Id == ExamID && x.Student.ClassId == ClassID)
                {
                    exam_Students.Add(x);
                }
            });

            return exam_Students;
        }
    }
}
