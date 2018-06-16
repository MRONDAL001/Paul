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
    public class RiesgoesController : Controller
    {
        private readonly ApplicationDbContext bd;

        public RiesgoesController(ApplicationDbContext context)
        {
            bd = context;
        }

        // GET: Riesgoes
        public async Task<IActionResult> Index()
        {
            return View(await bd.Riesgo.ToListAsync());
        }

        // GET: Riesgoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riesgo = await bd.Riesgo
                .SingleOrDefaultAsync(m => m.IdRiesgo == id);
            if (riesgo == null)
            {
                return NotFound();
            }

            return View(riesgo);
        }

        // GET: Riesgoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Riesgoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Riesgo riesgo)
        {
            if (ModelState.IsValid)
            {
                bd.Add(riesgo);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(riesgo);
        }

        // GET: Riesgoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riesgo = await bd.Riesgo.SingleOrDefaultAsync(m => m.IdRiesgo == id);
            if (riesgo == null)
            {
                return NotFound();
            }
            return View(riesgo);
        }

        // POST: Riesgoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Riesgo riesgo)
        {
            if (id != riesgo.IdRiesgo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(riesgo);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiesgoExists(riesgo))
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
            return View(riesgo);
        }



        // POST: Riesgoes/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var riesgo = await bd.Riesgo.SingleOrDefaultAsync(m => m.IdRiesgo == id);
            bd.Riesgo.Remove(riesgo);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiesgoExists(Riesgo riesgo)
        {
            return bd.Riesgo.Any(e => e.Descripcion == riesgo.Descripcion);
        }
    }
}
