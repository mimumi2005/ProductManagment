using FluentAssertions;
using ProductManagment.Application.Common;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Application.Products.Mappers;
using ProductManagment.Domain.Entities;

namespace ProductManagment.UnitTests.Products.MapperTests
{
    public class ProductDtoMapperTests
    {
        [Fact]
        public void ToDto_Should_Map_Product_To_GetProductDto_Correctly()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Shampoo",
                Quantity = 5,
                Price = 10m
            };

            // Act
            var dto = ProductDtoMapper.ToDto(product);

            // Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(product.Id);
            dto.Title.Should().Be(product.Title);
            dto.Quantity.Should().Be(product.Quantity);
            dto.Price.Should().Be(product.Price);
            dto.TotalPriceWithVAT.Should().Be((product.Price * product.Quantity) * (1 + PriceConfig.VAT));
        }

        [Fact]
        public void ToDto_Should_Return_Null_If_Product_Is_Null()
        {
            // Act
            var dto = ProductDtoMapper.ToDto(null);

            // Assert
            dto.Should().BeNull();
        }

        [Fact]
        public void ToEntity_Should_Map_ProductDto_To_Product_Correctly()
        {
            // Arrange
            var dto = new ProductDto
            {
                Title = "Car Wax",
                Quantity = 3,
                Price = 15m
            };

            // Act
            var entity = ProductDtoMapper.ToEntity(dto);

            // Assert
            entity.Should().NotBeNull();
            entity.Title.Should().Be(dto.Title);
            entity.Quantity.Should().Be(dto.Quantity);
            entity.Price.Should().Be(dto.Price);
        }

        [Fact]
        public void ToEntity_Should_Return_Null_If_Dto_Is_Null()
        {
            // Act
            var entity = ProductDtoMapper.ToEntity(null);

            // Assert
            entity.Should().BeNull();
        }
    }
}
