using Integration.Tests.Config;

namespace Demo.Tests.UsesCases.GetProduct;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class GetProductTests
{
    private readonly IntegrationTestsFactory _factory;

    public GetProductTests(IntegrationTestsFactory factory)
        => _factory = factory;

    [Fact]
    public async Task ProductId41_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 41;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 41 &&
                    m.Name == "Drone" &&
                    m.Quantity == 5));
    }

    [Fact]
    public async Task ProductId57_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 57;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 57 &&
                    m.Name == "Battery Backup" &&
                    m.Quantity == 10));
    }

    [Fact]
    public async Task ProductId84_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 84;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 84 &&
                    m.Name == "Smart Washer" &&
                    m.Quantity == 15));
    }

    [Fact]
    public async Task ProductId101_Get_StatusCode404()
    {
        // Arrange
        var id = 101;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should().Be404NotFound();
    }
}
