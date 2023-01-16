using KursusENGFinal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KursusENGFinal.Models;


namespace KursusENGFinal.Controllers
{
    [Authorize]
    public class DaftarKursusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DaftarKursusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DaftarKursus

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {

            return _context.DaftarKursus != null ?
                        View(await _context.DaftarKursus.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.DaftarKursus'  is null.");
        }

        // GET: DaftarKursus/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DaftarKursus == null)
            {
                return NotFound();
            }

            var daftarKursus = await _context.DaftarKursus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (daftarKursus == null)
            {
                return NotFound();
            }

            return View(daftarKursus);
        }

        // GET: DaftarKursus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DaftarKursus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NickName,FullName,Umur,Tingkat")] DaftarKursus daftarKursus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daftarKursus);
                await _context.SaveChangesAsync();
                return View("Done");
            }
            return View();
        }

        // GET: DaftarKursus/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DaftarKursus == null)
            {
                return NotFound();
            }

            var daftarKursus = await _context.DaftarKursus.FindAsync(id);
            if (daftarKursus == null)
            {
                return NotFound();
            }
            return View(daftarKursus);
        }

        // POST: DaftarKursus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NickName,FullName,Umur,Tingkat")] DaftarKursus daftarKursus)
        {
            if (id != daftarKursus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(daftarKursus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaftarKursusExists(daftarKursus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(daftarKursus);
        }

        // GET: DaftarKursus/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DaftarKursus == null)
            {
                return NotFound();
            }

            var daftarKursus = await _context.DaftarKursus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (daftarKursus == null)
            {
                return NotFound();
            }

            return View(daftarKursus);
        }

        // POST: DaftarKursus/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DaftarKursus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DaftarKursus'  is null.");
            }
            var daftarKursus = await _context.DaftarKursus.FindAsync(id);
            if (daftarKursus != null)
            {
                _context.DaftarKursus.Remove(daftarKursus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DaftarKursusExists(int id)
        {
            return (_context.DaftarKursus?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Done()
        {
            return View();
        }
    }
}
