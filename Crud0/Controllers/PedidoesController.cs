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
    public class PedidoesController : Controller
    {
        private readonly BarContext _context;

        public PedidoesController(BarContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var barContext = _context.Pedidos.Include(p => p.IdMesaNavigation).Include(p => p.IdMetodoPagoNavigation);
            return View(await barContext.ToListAsync());
        }


        [HttpGet]
        public async Task<FileResult> ExportarPedidos()
        {
            var pedidos = await _context.Pedidos.ToListAsync();
            var NombreArchivo = $"Usuarios.xlsx";
            return Exportar(NombreArchivo, pedidos);
        }

        private FileResult Exportar(string NombreArchivo, IEnumerable<Pedido> pedidos)
        {
            DataTable dataTable = new DataTable("Pedidos");
            dataTable.Columns.AddRange(new DataColumn[]
            {

                new DataColumn("Fecha Pedido"),
                new DataColumn("Estado Pedido"),
                new DataColumn("Metodo de Pago"),
                new DataColumn("Total")
            });
            foreach (var pedido in pedidos)
            {
                dataTable.Rows.Add(
                    pedido.FechaPedido,
                    pedido.EstadoPedido,
                    pedido.IdMetodoPago,
                    pedido.Total);
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

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdMesaNavigation)
                .Include(p => p.IdMetodoPagoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "Id", "Id");
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodoPagos, "Id", "Id");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaPedido,EstadoPedido,Total,Observaciones,IdMesa,IdMetodoPago")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.EstadoPedido = "Activo";
                pedido.FechaPedido= DateOnly.FromDateTime(DateTime.Now);
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "Id", "Id", pedido.IdMesa);
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodoPagos, "Id", "Id", pedido.IdMetodoPago);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "Id", "Id", pedido.IdMesa);
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodoPagos, "Id", "Id", pedido.IdMetodoPago);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,FechaPedido,EstadoPedido,Total,Observaciones,IdMesa,IdMetodoPago")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "Id", "Id", pedido.IdMesa);
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodoPagos, "Id", "Id", pedido.IdMetodoPago);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdMesaNavigation)
                .Include(p => p.IdMetodoPagoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(byte id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }


    }
}
