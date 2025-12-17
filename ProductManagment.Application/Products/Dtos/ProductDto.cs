
namespace ProductManagment.Application.Products.Dtos
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
