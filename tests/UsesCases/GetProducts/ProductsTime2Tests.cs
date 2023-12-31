﻿using Integration.Tests.Config;

namespace Integration.Tests.UsesCases.GetProducts;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class ProductsTime2Tests
{
    private readonly IntegrationTestsFactory _factory;

    public ProductsTime2Tests(IntegrationTestsFactory factory)
    {
        _factory = factory;
        _factory.PrepareDatabase();
    }


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
