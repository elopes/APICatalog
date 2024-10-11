namespace APICatalog.Models
{
    public class Category
    {
        public int CategoryId { get; set; } 
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? ImageUrl { get; set; } = null;

        // Propriedade de navegação para os produtos
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
