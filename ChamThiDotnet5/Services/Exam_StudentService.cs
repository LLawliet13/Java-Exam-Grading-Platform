using ChamThiDotnet5.Models;
using System.Collections.Generic;

namespace ChamThiDotnet5.Services
{
    public interface Exam_StudentService
    {

        public abstract List<Exam_Student> FindStudent_ExamByClassAndExamID(int ClassID, int ExamID);

    }
}
