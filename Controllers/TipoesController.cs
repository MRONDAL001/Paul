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
    public class TipoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private void InicializarMensaje(string mensaje)
        {
            if (mensaje == null)
            {
                mensaje = "";
            }
            ViewData["Error"] = mensaje;
        }
        // GET: Tipoes
        public async Task<IActionResult> Index(string mensaje)
        {
            InicializarMensaje(mensaje);
            return View(await _context.Tipo.ToListAsync());
        }
        // GET: Tipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        // GET: Tipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo = await _context.Tipo.SingleOrDefaultAsync(m => m.IdTipo == id);
            if (tipo == null)
            {
                return NotFound();
            }
            return View(tipo);
        }

        // POST: Tipoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tipo tipo)
        {
            if (id != tipo.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoExists(tipo))
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
            return View(tipo);
        }

        // GET: Tipoes/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tipo = await _context.Tipo.SingleOrDefaultAsync(m => m.IdTipo == id);
                _context.Tipo.Remove(tipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { mensaje = "Persona asiganda como tecnico" });
            };
        }

        private bool TipoExists(Tipo tipo)
        {
            return _context.Tipo.Any(e => e.Descripcion == tipo.Descripcion);
        }
    }
}
