using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace miChango.Models
{
    public class ShowShoppingListViewModel
    {
        public ShoppingList defaultList { get; set; }
        public ShoppingList userList { get; set; }
    }

    public class ShoppingList
    {
        [Key]
        [Display(Name = "Shopping id")]
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        [Key]
        public string Name { get; set; }
        public string Image { get; set; }
    }
}