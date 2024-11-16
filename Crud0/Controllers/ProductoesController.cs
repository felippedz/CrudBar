using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud0.Models;
using ClosedXML.Excel;
using System.Data;

namespace Crud0.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly BarContext _context;

        public ProductoesController(BarContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var barContext = _context.Productos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdEnvaseNavigation).Include(p => p.IdMarcaNavigation);
            return View(await barContext.ToListAsync());
        }

        [HttpGet]
        public async Task<FileResult> ExportarProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            var NombreArchivo = $"Usuarios.xlsx";
            return Exportar(NombreArchivo, productos);
        }

        private FileResult Exportar(string NombreArchivo, IEnumerable<Producto> productos)
        {
            DataTable dataTable = new DataTable("Productos");
            dataTable.Columns.AddRange(new DataColumn[]
            {

                
                new DataColumn("Nombre"),
                new DataColumn("Marca"),
                new DataColumn("Categoria"),
                new DataColumn("Precio"),
                new DataColumn("PrecioVenta")
            });
            foreach (var producto in productos)
            {
                dataTable.Rows.Add(
                    producto.Nombre,
                    producto.IdMarca,
                    producto.IdCategoria,
                    producto.Precio,
                    producto.PrecioVenta);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", NombreArchivo);
                }
            }
        }


        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdEnvaseNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "NombreCategoria");
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "Id");
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "Id");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,PrecioVenta,Disponibilidad,Estado,ImagenProducto,IdCategoria,IdEnvase,IdMarca")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Estado = true;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "Id", producto.IdCategoria);
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "Id", producto.IdEnvase);
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "Id", producto.IdMarca);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "Id", producto.IdCategoria);
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "Id", producto.IdEnvase);
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "Id", producto.IdMarca);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Nombre,Descripcion,Precio,PrecioVenta,Disponibilidad,Estado,ImagenProducto,IdCategoria,IdEnvase,IdMarca")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "Id", producto.IdCategoria);
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "Id", producto.IdEnvase);
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "Id", producto.IdMarca);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdEnvaseNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(byte id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
