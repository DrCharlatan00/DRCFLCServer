using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!Directory.Exists("AudioFiles"))
{
    Directory.CreateDirectory("AudioFiles");
    Environment.Exit(-1);
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.MapGet("/audio/{name}",async (string name) => {
    if (!Directory.Exists("AudioFiles")) {
        Directory.CreateDirectory("AudioFiles");
        Console.WriteLine("Create Directory AudioFiles /n Please Replace files flac to this folder"); 
        Environment.Exit(-1);
    }
    var file = Path.Combine("AudioFiles", name);

    if (!File.Exists(file)) {
        Console.WriteLine($"Not found {file}");
        return Results.NotFound();
    }

    var bytes = await File.ReadAllBytesAsync(file);
    Console.WriteLine("Found!");
    return Results.File(bytes, "audio/flac", name);
});

app.MapGet("/list", () => { 
    var dir = "AudioFiles"; 
    var files = Directory.GetFiles(dir, "*.flac").Select(Path.GetFileName).ToList(); 
    return Results.Json(files); 
});

app.MapGet("/version", () => {
    return Results.Ok("MoonRise V 0.1");
});

app.MapGet("/alive", () => { return Results.Ok("Live"); });

app.Run();

