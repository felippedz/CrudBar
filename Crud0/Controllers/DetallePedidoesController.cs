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
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Hosting;

namespace Crud0.Controllers
{
    public class DetallePedidoesController : Controller
    {
        private readonly BarContext _context;

        public DetallePedidoesController(BarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<FileResult> ExportarDetallePedidos()
        {
            var detallePedidos = await _context.DetallePedidos.ToListAsync();
            var NombreArchivo = $"Usuarios.xlsx";
            return Exportar(NombreArchivo, detallePedidos);
        }

        private FileResult Exportar(string NombreArchivo, IEnumerable<DetallePedido> detallePedidos)
        {
            DataTable dataTable = new DataTable("Detalle Pedidos");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                
                new DataColumn("Id Producto"),
                new DataColumn("Nombre Producto"),
                new DataColumn("Cantidad Vendida"),
                new DataColumn("Costo Venta"),
                new DataColumn("Precio Venta"),
                new DataColumn("Ganancia"),
                new DataColumn("Sede")                
            });
            foreach (var detallePedido in detallePedidos)
            {
                dataTable.Rows.Add(
                    detallePedido.IdProducto,
                    detallePedido.IdProductoNavigation.Nombre,
                    detallePedido.Cantidad,
                    detallePedido.IdProductoNavigation.Precio,
                    detallePedido.IdProductoNavigation.PrecioVenta,
                    detallePedido.Subtotal,
                    detallePedido.SedeVenta
                    );
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


        // GET: DetallePedidoes
        public async Task<IActionResult> Index()
        {
            var barContext = _context.DetallePedidos.Include(d => d.IdPedidoNavigation).Include(d => d.IdProductoNavigation);
            return View(await barContext.ToListAsync());
        }

        // GET: DetallePedidoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // GET: DetallePedidoes/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id");
            return View();
        }

        // POST: DetallePedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,SedeVenta,Cantidad,PrecioUnitario,Subtotal,Total,IdPedido,IdProducto")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallePedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Estado,SedeVenta,Cantidad,PrecioUnitario,Subtotal,Total,IdPedido,IdProducto")] DetallePedido detallePedido)
        {
            if (id != detallePedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePedidoExists(detallePedido.Id))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // POST: DetallePedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido != null)
            {
                _context.DetallePedidos.Remove(detallePedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallePedidoExists(byte id)
        {
            return _context.DetallePedidos.Any(e => e.Id == id);
        }

        private static List<Pedido> _pedidos = new();

       
    }
}
