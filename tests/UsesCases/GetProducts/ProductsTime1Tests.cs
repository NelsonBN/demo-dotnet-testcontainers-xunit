using Integration.Tests.Config;

namespace Demo.Tests.UsesCases.GetProducts;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class ProductsTime1Tests
{
    private readonly IntegrationTestsFactory _factory;

    public ProductsTime1Tests(IntegrationTestsFactory factory)
        => _factory = factory;


    [Fact]
    public async Task All_Get_StatusCode200And100Products()
    {
        // Arrange && Act
        var act = await _factory.CreateClient()
            .GetAsync("/products");


        // Assert
        act.Should()
           .Be200Ok()
           .And.Satisfy<IEnumerable<ProductResponse>>(model =>
                model.Should().HaveCount(100));
    }
}
