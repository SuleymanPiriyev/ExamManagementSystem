using ExamManagementSystem.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Exam : IEntity
    {
        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public int ExamScore { get; set; }

        public int? LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}