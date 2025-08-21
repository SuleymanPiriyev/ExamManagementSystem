using ExamManagementSystem.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Lesson : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(3)]
        public string LessonCode { get; set; }
        [Required, MaxLength(30)]
        public string LessonName { get; set; }

        public int? SchoolClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}