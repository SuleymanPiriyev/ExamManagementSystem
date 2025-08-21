using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Controllers
{
    public class ExamController : Controller
    {
        private readonly Repo repo;

        public ExamController(Repo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var exams = await repo.Get<Exam>().Include(x => x.Lesson).Include(x => x.Student).ToListAsync();

            await GetData();

            return View(exams);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                await repo.Add(exam);
                return RedirectToAction(nameof(Index));
            }

            await GetData();

            var exams = await repo.Get<Exam>().ToListAsync();
            return View("Index", exams);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Exam exam)
        {
            if (ModelState.IsValid)
            {
                await repo.Update(exam);
                return RedirectToAction(nameof(Index));
            }

            await GetData();

            var exams = await repo.Get<Exam>().ToListAsync();
            return View("Index", exams);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var exam = await repo.Get<Exam>().FirstOrDefaultAsync(t => t.Id == id);
            if (exam != null)
            {
                await repo.Remove(exam);
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task GetData()
        {
            var lessons = await repo.Get<Lesson>().ToListAsync();
            ViewBag.Lessons = lessons.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LessonCode
            }).ToList();

            var students = await repo.Get<Student>().ToListAsync();
            ViewBag.Students = students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.StudentNumber} - {s.StudentName} {s.StudentSurname}"
            }).ToList();
        }
    }
}