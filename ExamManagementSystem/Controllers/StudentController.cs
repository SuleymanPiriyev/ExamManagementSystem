using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly Repo repo;

        public StudentController(Repo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var students = await repo.Get<Student>().Include(x => x.SchoolClass).ToListAsync();

            await GetData();

            return View(students);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await repo.Add(student);
                return RedirectToAction(nameof(Index));
            }

            await GetData();

            var students = await repo.Get<Student>().ToListAsync();
            return View("Index", students);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                await repo.Update(student);
                return RedirectToAction(nameof(Index));
            }

            await GetData();

            var students = await repo.Get<Student>().ToListAsync();
            return View("Index", students);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var student = await repo.Get<Student>().FirstOrDefaultAsync(t => t.Id == id);
            if (student != null)
            {
                await repo.Remove(student);
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task GetData()
        {
            ViewBag.Classes = new SelectList(await repo.Get<SchoolClass>().ToListAsync(), "Id", "SchoolClassNumber");
        }
    }
}