using AspUppgift.Data;
using AspUppgift.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspUppgift.Controllers
{
    [Authorize]
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Classes.ToListAsync());
        }


        // GET: Classes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.Classes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }
            var model = new List<AddStudent>();

            foreach (var user in _userManager.Users)
            {
                var addStudent = new AddStudent
                {
                    UserId = user.Id,
                    UserName = user.DisplayName
                };

                addStudent.IsSelected = false;
                model.Add(addStudent);
            }


            
            return View(schoolClass);
        }

        // GET: Classes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.Classes.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }
            var model = new List<AddTeacher>();

            foreach (var user in _userManager.Users)
            {
                var addTeacher = new AddTeacher
                {
                    UserId = user.Id,
                    UserName = user.DisplayName
                };

                addTeacher.IsSelected = false;
                model.Add(addTeacher);
            }


            return View(model);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id")] SchoolClass schoolClass, List<AddTeacher> model)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var teacher = await _userManager.FindByIdAsync(model[i].UserId);

                    if (model[i].IsSelected)
                    {
                        schoolClass.Teacher = teacher;
                        _context.Classes.Update(schoolClass);
                        //_context.Users.Update();
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("Index", "Classes");
            }
            return View(schoolClass);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.Classes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var schoolClass = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(schoolClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassExists(string id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}