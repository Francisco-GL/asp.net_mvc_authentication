// Libreria necesaria para creacion de "key" en cookie
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // ----------- Implementacion del servicio de Authentication ----------------
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme
            ).AddCookie(option => {
                option.LoginPath = "/Access/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            });
            // --------------------------------------------------------------------------

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ------------ IMPORTANTE ------------
            app.UseAuthentication();
            // ------------------------------------

            app.UseAuthorization();

            // ------- Cambio de ruta por default a la vista de Login -------
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Access}/{action=Login}/{id?}");
            // --------------------------------------------------------------

            app.Run();
        }
    }
}