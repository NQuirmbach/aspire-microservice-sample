namespace ProductService.Products;

public class Product
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public double Price { get; set; }
    public double DiscountPercentage { get; set; }
    public double Rating { get; set; }
    public int Stock { get; set; }
    public required string Brand { get; set; }
    public required string Category { get; set; }
    public required string Thumbnail { get; set; }
    public IEnumerable<string> Images { get; set; } 
}