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
    private static readonly Semaphore _semaphore = new(3, 3);


    public IntegrationTestsFactory()
        => _dbContainer = new ContainerBuilder()
            .WithImage(DB_IMAGE)
            .WithEnvironment("MYSQL_DATABASE", DB_DATABASE)
            .WithEnvironment("MYSQL_ROOT_PASSWORD", DB_ROOT_PASSWORD)
            .WithPortBinding(DB_CONTAINER_PORT, true)
            .WithResourceMapping(Path.GetFullPath("./Data"), "/docker-entrypoint-initdb.d")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DB_CONTAINER_PORT))
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IDbConnection));
            services.AddScoped<IDbConnection>(sp =>
            {
                var connection = new MySqlConnection($"server=localhost; Port={_dbContainer.GetMappedPublicPort(DB_CONTAINER_PORT)}; database={DB_DATABASE}; uid={DB_USERNAME}; password={DB_ROOT_PASSWORD};");
                connection.Open();

                return connection;
            });
        });




    public async Task InitializeAsync()
    {
        _semaphore.WaitOne();
        ;
        await _dbContainer.StartAsync().ConfigureAwait(false);
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync().ConfigureAwait(false);
        _semaphore.Release();
        ;
    }
}

public abstract class IntegrationTests : IClassFixture<IntegrationTestsFactory> { }

[CollectionDefinition(nameof(CollectionIntegrationTests))]
public sealed class CollectionIntegrationTests : ICollectionFixture<IntegrationTestsFactory> { }
