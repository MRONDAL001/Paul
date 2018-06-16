using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartAdmin.Web.Data;
using SmartAdmin.Web.Models.Sistema;

namespace SmartAdmin.Web.Controllers
{
    [Authorize]
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext bd;

        public PersonasController(ApplicationDbContext context)
        {
            bd = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await bd.Persona.ToListAsync());
        }
        public async Task<Persona> GetList()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://riesgostecnologicos.azurewebsites.net/");
                var url = string.Format("{0}{1}", "api/Persona","/ListaPersonas");
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                   
                }
               
                var list = JsonConvert.DeserializeObject<List<Persona>>(result);
                return list;
               
            }
            catch (Exception ex)
            {
               
            }
        }

        

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                bd.Add(persona);
                await bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await bd.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Persona persona)
        {
            if (id != persona.IdPersona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(persona);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona))
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
            return View(persona);
        }

        // GET: Personas/Delete/5        
        public async Task<IActionResult> Delete(int id)
        {
            var persona = await bd.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
            bd.Persona.Remove(persona);
            await bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PersonaExists(Persona persona)
        {
            return bd.Persona.Any(e => e.Cedula == persona.Cedula);
        }
    }
}
