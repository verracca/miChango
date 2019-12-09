using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace miChango.Models
{
    /*
     * definimos el Modelo de la Vista que vamos a usar para la pagina principal
     * debemos tener una lista de productos 
     *  y la shopping list del usuario 
     */
    public class ShowShoppingListViewModel
    {
        public List<Product> defaultList { get; set; }

        public ShoppingList userList { get; set; }
    }

    /* La relaciones entre los modelos 
     * es similar a la explicada en este video
     * https://youtu.be/txR3S4zisuw
    */

    public class ShoppingList
    {
        public ShoppingList()
        {
            this.Name = "MiChango de compras";
        }

        public int ShoppingListID { get; set; }

        public string Name { get; set; }

        // Los productos estan en una lista virtual, lo que significa que 
        // no se cargan inmediatamente cuando se carga la Lista de shopping
        // se cargan recien cuando se los necestia
        public virtual List<Product> Products { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        
        public string Name { get; set; }

        public string Image { get; set; }

        public int ShoppingListID { get; set; }

        // aparentemente tiene que tener ShoppingList virtual
        // para relaciones uno a muchos  https://youtu.be/txR3S4zisuw?t=166
        public virtual ShoppingList ShoppingList { get; set; }
    }
}