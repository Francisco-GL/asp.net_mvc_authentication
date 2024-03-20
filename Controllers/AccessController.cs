using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// Librerias necesarias para la creacion de cookie de acceso
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
// Importancion del modelo para el inicio de sesion
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class AccessController : Controller
    {
        // GET: AccessController
        public ActionResult Login()
        {
            // Proceso para verificacion de si ya cuenta con sesion activa
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home"); // Redireccion a la vista Home (VIEW, CONTROLLER)
            
            return View();
        }
        
        // -------------------------------------------------- Asignacion de permiso para entrar a la app -----------------------------------------
        [HttpPost]
        public async Task<ActionResult> Login(AppLogin modelLogin) 
        {
            if (modelLogin.Email == "user@gmail.com" && modelLogin.Password == "Contrasena123") {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties() {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");

            }
            ViewData["ValidateMessage"] = "Email or Password incorrect!";
            return View();
        }
        // ----------------------------------------------------------------------------------------------------------------------------------------

        // GET: AccessController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccessController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccessController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
