using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Controllers
{
    public class SchoolClassController : Controller
    {
        private readonly Repo repo;

        public SchoolClassController(Repo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var classes = await repo.Get<SchoolClass>().ToListAsync();

            return View(classes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                await repo.Add(schoolClass);
                return RedirectToAction(nameof(Index));
            }

            var classes = await repo.Get<SchoolClass>().ToListAsync();
            return View("Index", classes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                await repo.Update(schoolClass);
                return RedirectToAction(nameof(Index));
            }

            var classes = await repo.Get<SchoolClass>().ToListAsync();
            return View("Index", classes);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var schoolClass = await repo.Get<SchoolClass>().FirstOrDefaultAsync(t => t.Id == id);
            if (schoolClass != null)
            {
                await repo.Remove(schoolClass);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}