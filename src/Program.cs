using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();


builder.Services
     .AddScoped<IDbConnection>(sp =>
     {
         var connection = new MySqlConnection(sp.GetRequiredService<IConfiguration>().GetConnectionString("MySQL"));
         connection.Open();

         return connection;
     });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();




app.MapGet("/products", async (IDbConnection connection) =>
{
    var result = await connection.QueryAsync<ProductResponse>(
        """
        SELECT
            `Id`,
            `Name`,
            `Quantity`
        FROM `Product` ;
        """);

    return Results.Ok(result);
});


app.MapGet("/products/{id:int}", async (IDbConnection connection, uint id) =>
{
    var result = await connection.QuerySingleOrDefaultAsync<ProductResponse>(
        """
        SELECT
            `Id`,
            `Name`,
            `Quantity`
        FROM `Product`
        WHERE `id` = @id ;
        """,
        new { id });

    if(result is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(result);
}).WithName("GetProduct");


app.MapPost("/products", async (IDbConnection connection, ProductRequest product) =>
{
    var id = await connection.ExecuteScalarAsync<ulong>(
        """
        INSERT `Product` (`Name`, `Quantity`)
                   VALUE (@Name , @Quantity );
        SELECT LAST_INSERT_ID();
        """,
        product);

    return TypedResults.CreatedAtRoute(
        id,
        "GetProduct",
        new { id });
});


app.MapPut("/products/{id:int}", async (IDbConnection connection, uint id, ProductRequest product) =>
{
    var rows = await connection.ExecuteAsync(
        """
        UPDATE `Product`
           SET `Name` = @Name,
               `Quantity` = @Quantity
        WHERE `id` = @id ;
        """,
        new
        {
            id,
            product.Name,
            product.Quantity
        });

    if(rows == 0)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});


app.MapDelete("/products/{id:int}", async (IDbConnection connection, uint id) =>
{
    var rows = await connection.ExecuteAsync(
        """
        Delete FROM `Product`
        WHERE `id` = @id ;
        """,
        new { id });

    if(rows == 0)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.Run();



public sealed record ProductRequest
{
    public string? Name { get; init; }
    public int Quantity { get; init; }
};


public sealed record ProductResponse
{
    public uint Id { get; init; }
    public string? Name { get; init; }
    public int Quantity { get; init; }
};
