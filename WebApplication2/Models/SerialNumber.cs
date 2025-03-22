using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        // put ? because we want to make it easy for us that we don't have to connect the serial number with an item
        // we create the item andthen afterwards we can connect it to a serial number.
    }
}
