using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Item
    {
        public int Id { get; set; }

        // properties by default marks as a non-nullable. 
        public string Name { get; set; } = null!;

        public double Price { get; set; }

        // each item has one serial number. one -> one
        public int? SerialNumberId { get; set; }
        public SerialNumber? SerialNumber { get; set; }
        // here don't need to specify data annotation in both of model classes


        // each item belongs to one category.
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public List<ItemClient>? ItemClients { get; set; }
    }

}
