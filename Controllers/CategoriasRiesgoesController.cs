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
    public class CategoriasRiesgoesController : Controller
    {
        private readonly ApplicationDbContext bd;

        public CategoriasRiesgoesController(ApplicationDbContext context)
        {
            bd = context;
        }

        // GET: CategoriasRiesgoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = bd.CategoriasRiesgo.Include(c => c.IdRiesgoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CategoriasRiesgoes/Details/5


        // GET: CategoriasRiesgoes/Create
        public IActionResult Create()
        {
            ViewData["IdRiesgo"] = new SelectList(bd.Riesgo, "IdRiesgo", "Descripcion");
            return View();
        }

        // POST: CategoriasRiesgoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriasRiesgo categoriasRiesgo)
        {
            if (ModelState.IsValid)
            {
                bd.Add(categoriasRiesgo);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRiesgo"] = new SelectList(bd.Riesgo, "IdRiesgo", "Descripcion", categoriasRiesgo.IdRiesgo);
            return View(categoriasRiesgo);
        }

        // GET: CategoriasRiesgoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriasRiesgo = await bd.CategoriasRiesgo.SingleOrDefaultAsync(m => m.IdCategoriasRiesgo == id);
            if (categoriasRiesgo == null)
            {
                return NotFound();
            }
            ViewData["IdRiesgo"] = new SelectList(bd.Riesgo, "IdRiesgo", "Descripcion", categoriasRiesgo.IdRiesgo);
            return View(categoriasRiesgo);
        }

        // POST: CategoriasRiesgoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriasRiesgo categoriasRiesgo)
        {
            if (id != categoriasRiesgo.IdCategoriasRiesgo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(categoriasRiesgo);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasRiesgoExists(categoriasRiesgo))
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
            ViewData["IdRiesgo"] = new SelectList(bd.Riesgo, "IdRiesgo", "Descripcion", categoriasRiesgo.IdRiesgo);
            return View(categoriasRiesgo);
        }

        // GET: CategoriasRiesgoes/Delete/5

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriasRiesgo = await bd.CategoriasRiesgo.SingleOrDefaultAsync(m => m.IdCategoriasRiesgo == id);
            bd.CategoriasRiesgo.Remove(categoriasRiesgo);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasRiesgoExists(CategoriasRiesgo categoriasRiesgo)
        {
            return bd.CategoriasRiesgo.Any(e => e.Descripcion == categoriasRiesgo.Descripcion && e.IdRiesgo == categoriasRiesgo.IdRiesgo);
        }
    }
}
