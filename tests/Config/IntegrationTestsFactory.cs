using System.Data;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySql.Data.MySqlClient;

namespace Integration.Tests.Config;

public sealed class IntegrationTestsFactory : WebApplicationFactory<ProductRequest>, IAsyncLifetime
{
    private const string DB_IMAGE = "mariadb:10.5.8";
    private const string DB_DATABASE = "demo";
    private const string DB_USERNAME = "root";
    private const string DB_ROOT_PASSWORD = "testpassword";
    private const int DB_CONTAINER_PORT = 3306;

    private readonly IContainer _dbContainer;
    private readonly string _dbSQL;

    public IntegrationTestsFactory()
    {
        _dbContainer = new ContainerBuilder()
            .WithImage(DB_IMAGE)
            .WithEnvironment("MYSQL_DATABASE", DB_DATABASE)
            .WithEnvironment("MYSQL_ROOT_PASSWORD", DB_ROOT_PASSWORD)
            .WithPortBinding(DB_CONTAINER_PORT, true)
            .WithResourceMapping(Path.GetFullPath("./Data"), "/docker-entrypoint-initdb.d")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DB_CONTAINER_PORT))
            .Build();

        _dbSQL = File.ReadAllText("./Data/02-db-prepare.sql");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IDbConnection));
            services.AddScoped<IDbConnection>(sp =>
            {
                var connection = new MySqlConnection(_getDBConnectionString());
                connection.Open();

                return connection;
            });
        });


    public void PrepareDatabase()
    {
        using var connection = new MySqlConnection(_getDBConnectionString());
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = _dbSQL;
        command.ExecuteNonQuery();
    }




    public async Task InitializeAsync()
        => await _dbContainer.StartAsync().ConfigureAwait(false);

    public new async Task DisposeAsync()
        => await _dbContainer.StopAsync().ConfigureAwait(false);

    private string _getDBConnectionString() => $"server=localhost; Port={_dbContainer.GetMappedPublicPort(DB_CONTAINER_PORT)}; database={DB_DATABASE}; uid={DB_USERNAME}; password={DB_ROOT_PASSWORD};";
}

[CollectionDefinition(nameof(CollectionIntegrationTests))]
public sealed class CollectionIntegrationTests : ICollectionFixture<IntegrationTestsFactory> { }
