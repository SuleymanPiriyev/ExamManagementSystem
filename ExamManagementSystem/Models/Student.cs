using ExamManagementSystem.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public int StudentNumber { get; set; }
        [Required, MaxLength(30)]
        public string StudentName { get; set; }
        [Required, MaxLength(30)]
        public string StudentSurname { get; set; }

        public int? SchoolClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
    }
}