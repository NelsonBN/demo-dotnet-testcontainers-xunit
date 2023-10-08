using Integration.Tests.Config;

namespace Demo.Tests.UsesCases.GetProduct;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class GetProductUntil30Tests
{
    private readonly IntegrationTestsFactory _factory;

    public GetProductUntil30Tests(IntegrationTestsFactory factory)
        => _factory = factory;


    [Fact]
    public async Task ProductId21_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 21;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 21 &&
                    m.Name == "Mouse Pad" &&
                    m.Quantity == 60));
    }

    [Fact]
    public async Task ProductId23_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 23;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 23 &&
                    m.Name == "Printer" &&
                    m.Quantity == 10));
    }

    [Fact]
    public async Task ProductId27_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 27;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 27 &&
                    m.Name == "SD Card" &&
                    m.Quantity == 50));
    }
}
