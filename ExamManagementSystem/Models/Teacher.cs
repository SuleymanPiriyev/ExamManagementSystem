using ExamManagementSystem.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Teacher : IEntity
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
    }
}