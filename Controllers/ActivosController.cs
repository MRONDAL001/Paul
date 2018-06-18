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
    public class ActivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivosController(ApplicationDbContext context)
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
        // GET: Activos
        public async Task<IActionResult> Index(string mensaje)
        {
            InicializarMensaje(mensaje);
            var applicationDbContext = _context.Activos.Include(a => a.IdTecnicoNavigation).Include(a => a.IdTipoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }       

        // GET: Activos/Create
        public IActionResult Create()
        {
            ViewData["IdTecnico"] = new SelectList(_context.Tecnico, "IdTecnico", "IdTecnico");
            ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "IdTipo");
            return View();
        }

        // POST: Activos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Activos activos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTecnico"] = new SelectList(_context.Tecnico, "IdTecnico", "IdTecnico", activos.IdTecnico);
            ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "IdTipo", activos.IdTipo);
            return View(activos);
        }

        // GET: Activos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activos = await _context.Activos.SingleOrDefaultAsync(m => m.IdActivos == id);
            if (activos == null)
            {
                return NotFound();
            }
            ViewData["IdTecnico"] = new SelectList(_context.Tecnico, "IdTecnico", "IdTecnico", activos.IdTecnico);
            ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "IdTipo", activos.IdTipo);
            return View(activos);
        }

        // POST: Activos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Activos activos)
        {
            if (id != activos.IdActivos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivosExists(activos.IdActivos))
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
            ViewData["IdTecnico"] = new SelectList(_context.Tecnico, "IdTecnico", "IdTecnico", activos.IdTecnico);
            ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "IdTipo", activos.IdTipo);
            return View(activos);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var activos = await _context.Activos.SingleOrDefaultAsync(m => m.IdActivos == id);
                _context.Activos.Remove(activos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                return RedirectToAction("Index", new { mensaje = "Existe Relacion con problemas de riesgos " });
            }
        }

        private bool ActivosExists(int id)
        {
            return _context.Activos.Any(e => e.IdActivos == id);
        }
    }
}
