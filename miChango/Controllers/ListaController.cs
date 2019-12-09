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
    public class ListaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        private List<Product> productosfull;

        public ListaController()
        {
            productosfull = new List<Product>();

            var productos = new List<string>() {
                "Morron",
                "Naranja",
                "Arandanos",
                "Anana",
                "Aji",
                "Banana",
                "Berenjena",
                "Brocoli",
                "Cebolla",
                "Cereza",
                "Frutilla",
                "Kiwi",
                "Melon",
                "Limon",
                "Palta",
                "Pepino",
                "Pera",
                "Uva",
                "Zanahoria",
                "Sandia",
                "Anana",
                "Baguette",
                "Cookies",
                "Croissant",
                "Donuts",
                "Baguette",
                "Magdalenas",
                "Pan",
                "Pancho",
                "Paty",
                "Cordero",
                "Pescado",
                "Pizza",
                "Pollo",
                "Queso",
                "Salchicha",
                "Tarta",
                "Wafles",
                "Yogurt",
                "Leche",
                "Mascarpone",
                "Torta",
            };

            // creo la lista con todos los productos  
            foreach (var nombre in productos)
            {
                var producto = new Product();
                producto.Name = nombre;
                producto.Image = nombre.ToLower() + ".gif";
                productosfull.Add(producto);
            };

        }

        private bool seEncuentra(Product productoBuscado, List<Product> productosDelUsuario)
        {
            var iter = 0;
            var encontrado = false;
            while(!encontrado && iter < productosDelUsuario.Count)
            {
                var productoActual = productosDelUsuario[iter];
                if (productoBuscado.Name == productoActual.Name) {
                    encontrado = true;
                }
                iter++;
            }

            return encontrado;
        }

        private List<Product> productosRestantes(List<Product> productosDelUsuario)
        {
            var otrosProductos = new List<Product>();

            foreach(var producto in productosfull)
            {
                if (!seEncuentra(producto, productosDelUsuario))
                {
                    otrosProductos.Add(producto);
                }
            }
            return otrosProductos;
        }

        // GET: Lista    
        [Authorize]
        public ActionResult Index()
        {
            // como obtener el usuario en ASP MVC 5? -> https://codeday.me/es/qa/20181206/14675.html
            // obtenemos el id del usuario como dice el post
            var userId = User.Identity.GetUserId();

            //lo buscamos en la base de datos
            var user = db.Users.Find(userId);

            // creamos el modelo de la vista y se lo pasamos a al view
            var productosRestantes = this.productosRestantes(user.ShoppingList.Products);

            var shoppingListVM = new ShowShoppingListViewModel() { defaultList = productosRestantes, userList = user.ShoppingList };

            return View(shoppingListVM);
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
