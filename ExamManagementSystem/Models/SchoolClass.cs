using ExamManagementSystem.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class SchoolClass : IEntity
    {
        public int Id { get; set; }
        [Required, Range(1, 99)]
        public int SchoolClassNumber { get; set; }
    }
}