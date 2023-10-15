using Integration.Tests.Config;

namespace Integration.Tests.UsesCases.duplicated;

[Collection(nameof(CollectionIntegrationTests))]
public sealed class DeleteProduct19Tests
{
    private readonly IntegrationTestsFactory _factory;

    public DeleteProduct19Tests(IntegrationTestsFactory factory)
    {
        _factory = factory;
        _factory.PrepareDatabase();
    }


    [Fact]
    public async Task ProductId41_Delete_StatusCode204()
    {
        // Arrange
        var id = 41;


        // Act
        var act = await _factory.CreateClient()
            .DeleteAsync($"/products/{id}");


        // Assert
        act.Should().Be204NoContent();
    }

    [Fact]
    public async Task ProductId57_Delete_StatusCode204()
    {
        // Arrange
        var id = 57;


        // Act
        var act = await _factory.CreateClient()
            .DeleteAsync($"/products/{id}");


        // Assert
        act.Should().Be204NoContent();
    }

    [Fact]
    public async Task ProductId84_Delete_StatusCode204()
    {
        // Arrange
        var id = 84;


        // Act
        var act = await _factory.CreateClient()
            .DeleteAsync($"/products/{id}");


        // Assert
        act.Should().Be204NoContent();
    }

    [Fact]
    public async Task ProductId651_Put_StatusCode404()
    {
        // Arrange
        var id = 651;


        // Act
        var act = await _factory.CreateClient()
            .DeleteAsync($"/products/{id}");


        // Assert
        act.Should().Be404NotFound();
    }
}
