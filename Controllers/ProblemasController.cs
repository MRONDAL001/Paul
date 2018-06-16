using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.Web.Data;
using SmartAdmin.Web.Models.Sistema;

namespace SmartAdmin.Web.Controllers
{
    [Authorize]
    public class ProblemasController : Controller
    {
        private readonly ApplicationDbContext bd;

        public ProblemasController(ApplicationDbContext context)
        {
            bd = context;
        }

        // GET: Problemas
        public async Task<IActionResult> Index()
        {
            return View(await bd.Problema.ToListAsync());
        }
               

        // GET: Problemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problemas/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Problema problema)
        {
            if (ModelState.IsValid)
            {
                bd.Add(problema);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problema);
        }

        // GET: Problemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problema = await bd.Problema.SingleOrDefaultAsync(m => m.IdProblema == id);
            if (problema == null)
            {
                return NotFound();
            }
            return View(problema);
        }

        // POST: Problemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Problema problema)
        {
            if (id != problema.IdProblema)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(problema);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemaExists(problema))
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
            return View(problema);
        }

        // GET: Problemas/Delete/5
        
        // POST: Problemas/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var problema = await bd.Problema.SingleOrDefaultAsync(m => m.IdProblema == id);
            bd.Problema.Remove(problema);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemaExists(Problema problema)
        {
            return bd.Problema.Any(e => e.Descripcion == problema.Descripcion);
        }
    }
}
