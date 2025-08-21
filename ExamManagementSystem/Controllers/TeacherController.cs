using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly Repo repo;

        public TeacherController(Repo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var teachers = await repo.Get<Teacher>().ToListAsync();

            return View(teachers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeacherName,TeacherSurname")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await repo.Add(teacher);
                return RedirectToAction(nameof(Index));
            }

            var teachers = await repo.Get<Teacher>().ToListAsync();
            return View("Index", teachers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,TeacherName,TeacherSurname")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await repo.Update(teacher);
                return RedirectToAction(nameof(Index));
            }

            var teachers = await repo.Get<Teacher>().ToListAsync();
            return View("Index", teachers);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await repo.Get<Teacher>().FirstOrDefaultAsync(t => t.Id == id);
            if (teacher != null)
            {
                await repo.Remove(teacher);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}