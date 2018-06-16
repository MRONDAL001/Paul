using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.Web.Data;
using SmartAdmin.Web.Models.Sistema;

namespace SmartAdmin.Web.Controllers
{
    public class ProblemaRiesgoesController : Controller
    {
        private readonly ApplicationDbContext db;

        public ProblemaRiesgoesController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: ProblemaRiesgoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.ProblemaRiesgo.Include(p => p.IdCategoriaRiesgoNavigation).Include(p => p.IdCategoriaRiesgoNavigation.IdRiesgoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProblemaRiesgoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaRiesgo = await db.ProblemaRiesgo
                .Include(p => p.IdCategoriaRiesgoNavigation)
                .SingleOrDefaultAsync(m => m.IdProblemaRiesgo == id);
            if (problemaRiesgo == null)
            {
                return NotFound();
            }

            return View(problemaRiesgo);
        }

        // GET: ProblemaRiesgoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoriaRiesgo"] = new SelectList(db.CategoriasRiesgo, "IdCategoriasRiesgo", "Descripcion");
            return View();
        }

        // POST: ProblemaRiesgoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProblemaRiesgo problemaRiesgo)
        {
            if (ModelState.IsValid)
            {
                db.Add(problemaRiesgo);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoriaRiesgo"] = new SelectList(db.CategoriasRiesgo, "IdCategoriasRiesgo", "Descripcion", problemaRiesgo.IdCategoriaRiesgo);
            return View(problemaRiesgo);
        }

        // GET: ProblemaRiesgoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaRiesgo = await db.ProblemaRiesgo.SingleOrDefaultAsync(m => m.IdProblemaRiesgo == id);
            if (problemaRiesgo == null)
            {
                return NotFound();
            }
            ViewData["IdCategoriaRiesgo"] = new SelectList(db.CategoriasRiesgo, "IdCategoriasRiesgo", "Descripcion", problemaRiesgo.IdCategoriaRiesgo);
            return View(problemaRiesgo);
        }

        // POST: ProblemaRiesgoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ProblemaRiesgo problemaRiesgo)
        {
            if (id != problemaRiesgo.IdProblemaRiesgo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(problemaRiesgo);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemaRiesgoExists(problemaRiesgo.IdProblemaRiesgo))
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
            ViewData["IdCategoriaRiesgo"] = new SelectList(db.CategoriasRiesgo, "IdCategoriasRiesgo", "Descripcion", problemaRiesgo.IdCategoriaRiesgo);
            return View(problemaRiesgo);
        }

        // GET: ProblemaRiesgoes/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var problemaRiesgo = await db.ProblemaRiesgo.SingleOrDefaultAsync(m => m.IdProblemaRiesgo == id);
            db.ProblemaRiesgo.Remove(problemaRiesgo);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemaRiesgoExists(int id)
        {
            return db.ProblemaRiesgo.Any(e => e.IdProblemaRiesgo == id);
        }
    }
}
