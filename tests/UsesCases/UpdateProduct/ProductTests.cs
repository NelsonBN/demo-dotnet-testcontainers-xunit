using System.Net.Http.Json;
using Bogus;
using Integration.Tests.Config;

namespace Integration.Tests.UsesCases.UpdateProduct;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class UpdateProductTests
{
    private readonly IntegrationTestsFactory _factory;

    public UpdateProductTests(IntegrationTestsFactory factory)
    {
        _factory = factory;
        _factory.PrepareDatabase();
    }


    [Fact]
    public async Task ProductId41_Put_StatusCode204()
    {
        // Arrange
        var id = 41;

        var product = new Faker<ProductRequest>()
            .RuleFor(p => p.Name, s => s.Commerce.ProductName())
            .RuleFor(p => p.Quantity, s => s.Random.Int(1, 100))
            .Generate();


        // Act
        var act = await _factory.CreateClient()
            .PutAsync(
                $"/products/{id}",
                JsonContent.Create(product));


        // Assert
        act.Should().Be204NoContent();
    }

    [Fact]
    public async Task ProductId57_Put_StatusCode204()
    {
        // Arrange
        var id = 57;

        var product = new Faker<ProductRequest>()
            .RuleFor(p => p.Name, s => s.Commerce.ProductName())
            .RuleFor(p => p.Quantity, s => s.Random.Int(1, 100))
            .Generate();


        // Act
        var act = await _factory.CreateClient()
            .PutAsync(
                $"/products/{id}",
                JsonContent.Create(product));


        // Assert
        act.Should().Be204NoContent();
    }

    [Fact]
    public async Task ProductId84_Put_StatusCode204()
    {
        // Arrange
        var id = 84;

        var product = new Faker<ProductRequest>()
            .RuleFor(p => p.Name, s => s.Commerce.ProductName())
            .RuleFor(p => p.Quantity, s => s.Random.Int(1, 100))
            .Generate();


        // Act
        var act = await _factory.CreateClient()
            .PutAsync(
                $"/products/{id}",
                JsonContent.Create(product));


        // Assert
        act.Should().Be204NoContent();
    }

    [Fact]
    public async Task ProductId204_Put_StatusCode404()
    {
        // Arrange
        var id = 204;

        var product = new Faker<ProductRequest>()
            .RuleFor(p => p.Name, s => s.Commerce.ProductName())
            .RuleFor(p => p.Quantity, s => s.Random.Int(1, 100))
            .Generate();


        // Act
        var act = await _factory.CreateClient()
            .PutAsync(
                $"/products/{id}",
                JsonContent.Create(product));


        // Assert
        act.Should().Be404NotFound();
    }
}
