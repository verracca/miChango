using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace miChango.Models
{
    public class Lista
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }

    }

    public class Product
    {
        [Key]
        public string Name { get; set; }
        public string Image { get; set; }
    }
}