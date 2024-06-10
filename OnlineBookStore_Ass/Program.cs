using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using OnlineBookStore_Ass.Data;
using OnlineBookStore_Ass.Services;
using OnlineBookStore_Ass.Controllers;
using FluentMigrator.Runner;
using OnlineBookStore_Ass.Data.FluentMigrator.MigrationClass;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
Environment.SetEnvironmentVariable("DOTNET_SYSTEM_GLOBALIZATION_INVARIANT", "false");
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.Conventions.Controller<UserController>().HasApiVersion(new ApiVersion(1, 0));
    options.Conventions.Controller<BookController>().HasApiVersion(new ApiVersion(1, 0));
});
builder.Services.AddDataDependencies(builder.Configuration);
builder.Services.AddServiceDependencies(builder.Configuration);
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres() // or your database type
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ScanIn(typeof(CreateBooksTable).Assembly, typeof(CreateCartsTable).Assembly, typeof(CreateUsersTable).Assembly, typeof(CreatePurchasesTable).Assembly)
        .For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole());

// Register the database dependencies
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//RunMigrations(app.Services);
//using (var scope = app.Services.CreateScope())
//{
//    var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
//    migrator.MigrateUp();
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
//void RunMigrations(IServiceProvider services)
//{
//    using var scope = services.CreateScope();
//    var migrator = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
//    migrator.MigrateUp();
//}