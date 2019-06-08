using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using miChango.Models;
using Microsoft.AspNet.Identity;


namespace miChango.Controllers
{
    public class ProductoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        [Authorize]
        public ActionResult Create([Bind(Include = "Name,Image")] Product product)
        {
            // como obtener el usuario en ASP MVC 5? -> https://codeday.me/es/qa/20181206/14675.html
            // obtenemos el id del usuario como dice el post
            var userId = User.Identity.GetUserId();

            //lo buscamos en la base de datos
            var user = db.Users.Find(userId);

            // le decimos al producto a que lista de shopping pertenece
            product.ShoppingListID = user.ShoppingListID;


            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                // Redirigimos a la pagina principal
                return RedirectToAction("Index", "Lista");
            }

            ViewBag.ShoppingListID = new SelectList(db.Listas, "ShoppingListID", "Name", product.ShoppingListID);
            return RedirectToAction("Index", "Lista");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();

            // Redirigimos a la pagina principal
            return RedirectToAction("Index", "Lista");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
