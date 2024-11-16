using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud0.Models;
using MiniExcelLibs;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;

namespace Crud0.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly BarContext _context;

        public UsuariosController(BarContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var barContext = _context.Usuarios.Include(u => u.IdDocumentoNavigation).Include(u => u.IdRolNavigation).Include(u => u.IdSedeNavigation);
            return View(await barContext.ToListAsync());
        }

        [HttpGet]
        public async Task<FileResult> ExportarUsuario()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            var NombreArchivo = $"Usuarios.xlsx";
            return Exportar(NombreArchivo, usuarios);
        }

        private FileResult Exportar(string NombreArchivo, IEnumerable<Usuario> usuarios)
        {
            DataTable dataTable = new DataTable("Usuario");
            dataTable.Columns.AddRange(new DataColumn[]
            {

                new DataColumn("Correo"),
                new DataColumn("Nombre"),
                new DataColumn("Apellido"),
                new DataColumn("Documento"),
                new DataColumn("Rol"),
                new DataColumn("Sede")
            });
            foreach (var usuario in usuarios)
            {
                dataTable.Rows.Add(
                    usuario.Correo,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Documento,
                    usuario.IdRol,
                    usuario.IdSede);
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


        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdDocumentoNavigation)
                .Include(u => u.IdRolNavigation)
                .Include(u => u.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "TipoDocumento");
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "NombreRol");
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "NombreSede");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Documento,Telefono,Correo,Estado,FechaCreacion,Password,IdDocumento,IdSede,IdRol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Estado = true;
                usuario.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "TipoDocumento", usuario.IdDocumento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "NombreRol", usuario.IdRol);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "NombreSede", usuario.IdSede);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "TipoDocumento", usuario.IdDocumento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "NombreRol", usuario.IdRol);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "NombreSede", usuario.IdSede);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Nombre,Apellido,Documento,Telefono,Correo,Estado,FechaCreacion,Password,IdDocumento,IdSede,IdRol")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "TipoDocumento", usuario.IdDocumento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "NombreRol", usuario.IdRol);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "NombreSede", usuario.IdSede);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdDocumentoNavigation)
                .Include(u => u.IdRolNavigation)
                .Include(u => u.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(byte id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
