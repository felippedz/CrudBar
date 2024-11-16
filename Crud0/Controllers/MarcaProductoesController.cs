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
    public class MarcaProductoesController : Controller
    {
        private readonly BarContext _context;

        public MarcaProductoesController(BarContext context)
        {
            _context = context;
        }

        // GET: MarcaProductoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarcaProductos.ToListAsync());
        }

        // GET: MarcaProductoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaProducto = await _context.MarcaProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marcaProducto == null)
            {
                return NotFound();
            }

            return View(marcaProducto);
        }

        // GET: MarcaProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarcaProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreMarca,Estado")] MarcaProducto marcaProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marcaProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marcaProducto);
        }

        // GET: MarcaProductoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaProducto = await _context.MarcaProductos.FindAsync(id);
            if (marcaProducto == null)
            {
                return NotFound();
            }
            return View(marcaProducto);
        }

        // POST: MarcaProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,NombreMarca,Estado")] MarcaProducto marcaProducto)
        {
            if (id != marcaProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marcaProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaProductoExists(marcaProducto.Id))
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
            return View(marcaProducto);
        }

        // GET: MarcaProductoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaProducto = await _context.MarcaProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marcaProducto == null)
            {
                return NotFound();
            }

            return View(marcaProducto);
        }

        // POST: MarcaProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var marcaProducto = await _context.MarcaProductos.FindAsync(id);
            if (marcaProducto != null)
            {
                _context.MarcaProductos.Remove(marcaProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaProductoExists(byte id)
        {
            return _context.MarcaProductos.Any(e => e.Id == id);
        }
    }
}
