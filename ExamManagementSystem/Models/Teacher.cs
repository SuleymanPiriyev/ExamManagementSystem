using ExamManagementSystem.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Teacher : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string TeacherName { get; set; }
        [Required, MaxLength(20)]
        public string TeacherSurname { get; set; }
    }
}