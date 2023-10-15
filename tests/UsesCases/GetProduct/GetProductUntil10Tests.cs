using Integration.Tests.Config;

namespace Integration.Tests.UsesCases.GetProduct;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class GetProductUntil10Tests
{
    private readonly IntegrationTestsFactory _factory;

    public GetProductUntil10Tests(IntegrationTestsFactory factory)
    {
        _factory = factory;
        _factory.PrepareDatabase();
    }


    [Fact]
    public async Task ProductId2_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 2;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 2 &&
                    m.Name == "Keyboard" &&
                    m.Quantity == 4));
    }

    [Fact]
    public async Task ProductId3_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 3;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 3 &&
                    m.Name == "Mouse" &&
                    m.Quantity == 7));
    }

    [Fact]
    public async Task ProductId7_Get_StatusCode200AndProduct()
    {
        // Arrange
        var id = 7;


        // Act
        var act = await _factory.CreateClient()
            .GetAsync($"/products/{id}");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<ProductResponse>(model =>
                model.Should().Match<ProductResponse>(m =>
                    m.Id == 7 &&
                    m.Name == "Graphics Card" &&
                    m.Quantity == 10));
    }
}
