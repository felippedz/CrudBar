using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud0.Models;

namespace Crud0.Controllers
{
    public class EnvaseProductoesController : Controller
    {
        private readonly BarContext _context;

        public EnvaseProductoesController(BarContext context)
        {
            _context = context;
        }

        // GET: EnvaseProductoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnvaseProductos.ToListAsync());
        }

        // GET: EnvaseProductoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envaseProducto = await _context.EnvaseProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envaseProducto == null)
            {
                return NotFound();
            }

            return View(envaseProducto);
        }

        // GET: EnvaseProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnvaseProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreEnvase,Estado")] EnvaseProducto envaseProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(envaseProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(envaseProducto);
        }

        // GET: EnvaseProductoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envaseProducto = await _context.EnvaseProductos.FindAsync(id);
            if (envaseProducto == null)
            {
                return NotFound();
            }
            return View(envaseProducto);
        }

        // POST: EnvaseProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,NombreEnvase,Estado")] EnvaseProducto envaseProducto)
        {
            if (id != envaseProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(envaseProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvaseProductoExists(envaseProducto.Id))
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
            return View(envaseProducto);
        }

        // GET: EnvaseProductoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envaseProducto = await _context.EnvaseProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envaseProducto == null)
            {
                return NotFound();
            }

            return View(envaseProducto);
        }

        // POST: EnvaseProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var envaseProducto = await _context.EnvaseProductos.FindAsync(id);
            if (envaseProducto != null)
            {
                _context.EnvaseProductos.Remove(envaseProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvaseProductoExists(byte id)
        {
            return _context.EnvaseProductos.Any(e => e.Id == id);
        }
    }
}
