using Integration.Tests.Config;

namespace Integration.Tests.UsesCases.GetProduct;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class GetProductUntil20Tests
{
    private readonly IntegrationTestsFactory _factory;

    public GetProductUntil20Tests(IntegrationTestsFactory factory)
    {
        _factory = factory;
        _factory.PrepareDatabase();
    }


    [Fact]
    public async Task ProductId12_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 12;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 12 &&
                    m.Name == "Cooling Fan" &&
                    m.Quantity == 30));
    }

    [Fact]
    public async Task ProductId13_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 13;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 13 &&
                    m.Name == "USB Cable" &&
                    m.Quantity == 100));
    }

    [Fact]
    public async Task ProductId19_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 19;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 19 &&
                    m.Name == "Router" &&
                    m.Quantity == 10));
    }
}
