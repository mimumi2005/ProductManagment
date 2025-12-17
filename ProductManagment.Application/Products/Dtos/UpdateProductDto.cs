namespace ProductManagment.Application.Products.Dtos
{
    public class UpdateProductDto
    {
        public string UserId { get; set; }
        public ProductDto NewProductDto { get; set; }
    }
}
