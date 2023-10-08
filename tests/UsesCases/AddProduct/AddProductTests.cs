using System.Net.Http.Json;
using Bogus;
using Integration.Tests.Config;

namespace Integration.Tests.UsesCases.AddProduct;

public sealed class AddProductTests : IntegrationTests
{
    private readonly IntegrationTestsFactory _factory;

    public AddProductTests(IntegrationTestsFactory factory)
        => _factory = factory;



    [Fact]
    public async Task NewPrduct1_Post_StatusCode201AndId()
    {
        // Arrange
        var product = new Faker<ProductRequest>()
            .RuleFor(p => p.Name, s => s.Commerce.ProductName())
            .RuleFor(p => p.Quantity, s => s.Random.Int(1, 100))
            .Generate();


        // Act
        var act = await _factory.CreateClient()
            .PostAsync(
                "/products",
                JsonContent.Create(product));


        // Assert
        act.Should()
           .Be201Created()
           .And.Satisfy<ulong>(model =>
                model.Should().BeGreaterThan(0));
    }

    [Fact]
    public async Task NewPrduct2_Post_StatusCode201AndId()
    {
        // Arrange
        var product = new Faker<ProductRequest>()
            .RuleFor(p => p.Name, s => s.Commerce.ProductName())
            .RuleFor(p => p.Quantity, s => s.Random.Int(1, 100))
            .Generate();


        // Act
        var act = await _factory.CreateClient()
            .PostAsync(
                "/products",
                JsonContent.Create(product));


        // Assert
        act.Should()
           .Be201Created()
           .And.Satisfy<ulong>(model =>
                model.Should().BeGreaterThan(0));
    }
}
