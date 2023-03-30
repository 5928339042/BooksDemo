using System.Text.Json.Serialization;
using BooksDemo.Data;
using BooksDemo.Middleware;
using BooksDemo.Migrations;
using BooksDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LibraryContext>();

builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // Serialize enums as strings in api responses (e.g. Gender)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // Ignore possible object cycles
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.MapControllers();

await MigrationHelper.ApplyDatabaseMigrationsAsync(app.Services);

app.Run();
