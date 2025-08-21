using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ExamManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repo repo;

        public HomeController(Repo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var studentsPerClass = await repo.Get<Student>()
                .GroupBy(s => s.SchoolClass.SchoolClassNumber)
                .Select(g => new
                {
                    ClassNumber = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            var examsPerLesson = await repo.Get<Exam>()
                .GroupBy(e => e.Lesson.LessonName)
                .Select(g => new
                {
                    Lesson = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            var lessonsPerTeacher = await repo.Get<Lesson>()
                .GroupBy(l => l.Teacher.TeacherName + " " + l.Teacher.TeacherSurname)
                .Select(g => new
                {
                    Teacher = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            var examResults = await repo.Get<Exam>()
                .GroupBy(e => e.Lesson.LessonName)
                .Select(g => new
                {
                    Lesson = g.Key,
                    AvgScore = g.Average(e => e.ExamScore)
                }).ToListAsync();

            ViewBag.StudentsPerClassLabels = JsonSerializer.Serialize(studentsPerClass.Select(x => "Class " + x.ClassNumber));
            ViewBag.StudentsPerClassData = JsonSerializer.Serialize(studentsPerClass.Select(x => x.Count));

            ViewBag.ExamsPerLessonLabels = JsonSerializer.Serialize(examsPerLesson.Select(x => x.Lesson));
            ViewBag.ExamsPerLessonData = JsonSerializer.Serialize(examsPerLesson.Select(x => x.Count));

            ViewBag.LessonsPerTeacherLabels = JsonSerializer.Serialize(lessonsPerTeacher.Select(x => x.Teacher));
            ViewBag.LessonsPerTeacherData = JsonSerializer.Serialize(lessonsPerTeacher.Select(x => x.Count));

            ViewBag.ExamResultsLabels = JsonSerializer.Serialize(examResults.Select(x => x.Lesson));
            ViewBag.ExamResultsData = JsonSerializer.Serialize(examResults.Select(x => x.AvgScore));

            return View();
        }
    }
}