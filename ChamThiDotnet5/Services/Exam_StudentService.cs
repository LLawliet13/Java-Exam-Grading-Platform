using ChamThiDotnet5.Models;
using System.Collections.Generic;

namespace ChamThiDotnet5.Services
{
    public interface Exam_StudentService
    {

        public abstract List<Exam_Student> FindStudent_ExamByClassAndExamID(int ClassID, int ExamID);
        public abstract List<Class_Exam> FindPending_ResultExamOfTeacher(int teacherid);
        public abstract List<Class_Exam> FindResultExamOfTeacher(int id);
    }
}
