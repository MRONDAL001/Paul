﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.Web.Data;
using SmartAdmin.Web.Models.Sistema;
using SmartAdmin.Web.Models.ViewModel;

namespace SmartAdmin.Web.Controllers
{
    public class TecnicoesController : Controller
    {
        private readonly ApplicationDbContext bd;

        public TecnicoesController(ApplicationDbContext context)
        {
            bd = context;
        }
        private void InicializarMensaje(string mensaje)
        {
            if (mensaje == null)
            {
                mensaje = "";
            }
            ViewData["Error"] = mensaje;
        }
        // GET: Tecnicoes
        public async Task<IActionResult> Index(string mensaje)
        {
            InicializarMensaje(mensaje);
            var tecni = await bd.Tecnico.Select(x => new ViewModelTecnico
            {
                Nombre = x.IdPersonaNavigation.Nombre,
                Apellido = x.IdPersonaNavigation.Apellido,
                Cedula = x.IdPersonaNavigation.Cedula,
                Direccion = x.IdPersonaNavigation.Direccion,
                IdPersona = x.IdPersona,
                IdTecnico = x.IdTecnico

            }).ToListAsync();
            return View(tecni);
        }
        public async Task<IActionResult> Asignar(string mensaje)
        {
            InicializarMensaje(mensaje);
            var lista = new List<ViewModelTecnico>();
            var personas = await bd.Persona.ToListAsync();
            foreach (var item in personas)
            {
                var tecnic = await bd.Tecnico.Where(y => y.IdPersona == item.IdPersona).FirstOrDefaultAsync();
                if (tecnic==null)
                {
                    var tecnic1 = await bd.Persona.Where(y => y.IdPersona == item.IdPersona).Select(x => new ViewModelTecnico
                    {
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,
                        Cedula = x.Cedula,
                        Direccion = x.Direccion,
                        IdPersona = x.IdPersona
                    }).FirstOrDefaultAsync();
                    if (tecnic1 != null)
                    {
                        lista.Add(tecnic1);
                    }

                }
                
            }
            return View(lista);
        }
        public async Task<IActionResult> AsignarTecnico(int id)
        {

            try
            {
                var tecnico = new Tecnico();
                tecnico.Estado = 1;
                tecnico.IdPersona = id;
                if (!TecnicoExists(tecnico))
                {
                    bd.Tecnico.Add(tecnico);
                    await bd.SaveChangesAsync();
                    return RedirectToAction(nameof(Asignar));
                }
                return RedirectToAction("Asignar", new { mensaje = "Ya esta asigando como tecnico" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Asignar", new { mensaje = ex });
            }
        }

        // GET: Tecnicoes/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(bd.Persona, "IdPersona", "Apellido");
            return View();
        }

        // POST: Tecnicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                bd.Add(tecnico);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(bd.Persona, "IdPersona", "Apellido", tecnico.IdPersona);
            return View(tecnico);
        }

        // GET: Tecnicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await bd.Tecnico.SingleOrDefaultAsync(m => m.IdTecnico == id);
            if (tecnico == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(bd.Persona, "IdPersona", "Apellido", tecnico.IdPersona);
            return View(tecnico);
        }

        // POST: Tecnicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTecnico,IdPersona,Estado")] Tecnico tecnico)
        {
            if (id != tecnico.IdTecnico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(tecnico);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnicoExists(tecnico))
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
            ViewData["IdPersona"] = new SelectList(bd.Persona, "IdPersona", "Apellido", tecnico.IdPersona);
            return View(tecnico);
        }

        // GET: Tecnicoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tecnico = await bd.Tecnico.SingleOrDefaultAsync(m => m.IdTecnico == id);
            bd.Tecnico.Remove(tecnico);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnicoExists(Tecnico tecnico)
        {
            return bd.Tecnico.Any(e => e.IdPersona == tecnico.IdPersona);
        }
    }
}
