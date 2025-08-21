using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Controllers
{
    public class LessonController : Controller
    {
        private readonly Repo repo;

        public LessonController(Repo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var lessons = await repo.Get<Lesson>().Include(x => x.SchoolClass).Include(x => x.Teacher).ToListAsync();

            await GetData();

            return View(lessons);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                await repo.Add(lesson);
                return RedirectToAction(nameof(Index));
            }

            await GetData();

            var lessons = await repo.Get<Lesson>().ToListAsync();
            return View("Index", lessons);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                await repo.Update(lesson);
                return RedirectToAction(nameof(Index));
            }

            await GetData();

            var lessons = await repo.Get<Lesson>().ToListAsync();
            return View("Index", lessons);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var lesson = await repo.Get<Lesson>().FirstOrDefaultAsync(t => t.Id == id);
            if (lesson != null)
            {
                await repo.Remove(lesson);
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task GetData()
        {
            ViewBag.Classes = new SelectList(await repo.Get<SchoolClass>().ToListAsync(), "Id", "SchoolClassNumber");
            ViewBag.Teachers = new SelectList(await repo.Get<Teacher>().ToListAsync(), "Id", "TeacherName");
        }
    }
}