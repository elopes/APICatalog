namespace APICatalog.Models
{
    public class Product
    {
        public int ProductId { get; set; } 
        public int CategoryId { get; set; } = 0;
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public decimal Price { get; set; } = 0;
        public string? ImageUrl { get; set; } = null;
        public float Stock { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.Now;

        // Propriedade de navegação para a categoria
        public Category Category { get; set; } = new Category();
    }
}
