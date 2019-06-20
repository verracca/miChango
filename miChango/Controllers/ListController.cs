using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using miChango.Models;

namespace miChango.Controllers
{
    public class ListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ShoppingList defaultList;

        public ListController()
        {
            defaultList = new ShoppingList();

            var products = new List<string>() {
                "Mayonesa",
                "Manzana",
                "otro",
            };

            products.ForEach(product =>
           {
               defaultList.Products.Add(new Product()
               {
                   Name = product,
                   Image = product.ToLower()+".jpg"
               });
           });

        }

        // GET: Listas
        public ActionResult Index()
        {
            // redirect a edit list con el id de la lista
            // /Listas/Edit/5
            return View(db.Listas.ToList());
        }

        // GET: Listas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList lista = db.Listas.Find(id);
            if (lista == null)
            {
                return HttpNotFound();
            }
            return View(lista);
        }

        // GET: Listas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Listas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,email")] ShoppingList lista)
        {
            if (ModelState.IsValid)
            {
                db.Listas.Add(lista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lista);
        }

        // GET: Listas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList lista = db.Listas.Find(id);
            // agarrar al usuario
            // verificar que el usuario sea el due;o de la lista

            // user.list
            // si no es el due;o redireccionar al /Listas/Details/id
            if (lista == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new ShowShoppingListViewModel() { userList = lista, defaultList = defaultList };
            return View(viewmodel);
        }

        // POST: Listas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,email")] ShoppingList lista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lista);
        }

        // GET: Listas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList lista = db.Listas.Find(id);
            if (lista == null)
            {
                return HttpNotFound();
            }
            return View(lista);
        }

        // POST: Listas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingList lista = db.Listas.Find(id);
            db.Listas.Remove(lista);
            db.SaveChanges();
            return RedirectToAction("Index");
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
