namespace WebApplication2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // In category side has many items. one -> many
        public List<Item>? Items { get; set; }
    }
}
