using Microsoft.AspNetCore.Mvc;
using Crud0.Models;
using Microsoft.EntityFrameworkCore;
using Crud0.Services;

namespace Crud0.Controllers
{
    public class AccesoController : Controller
    {
        private readonly BarContext _context;
        public AccesoController(BarContext context)
        {
            _context = context;
        }
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Login(Login login)
        //{
        //    Usuario? usuario = await _context.Usuarios.Where(u => u.Correo == login.Correo && u.Password == login.Password);
        //    if(usuario == null)
        //    {
        //        ViewData["Mensaje"] = "No se encontraron coincidencias";
        //        return View();
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        public ActionResult Login()
        {
            return View();
        }

   
    }
}