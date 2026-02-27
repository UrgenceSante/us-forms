using UsFormAdmin.Application.Form;
using UsFormAdmin.Application.Forms.Delete;
using UsFormAdmin.Application.Forms.Queries;
using UsFormAdmin.Application.Submissions;
using UsFormAdmin.Application.Submissions.Create;
using UsFormAdmin.Application.Submissions.Queries;
using UsFormAdmin.Infrastructure.Forms;
using UsFormAdmin.Infrastructure.Submissions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application services
builder.Services.AddScoped<CreateFormService>();
builder.Services.AddScoped<GetFormService>();
builder.Services.AddScoped<ListFormsService>();
builder.Services.AddScoped<DeleteFormService>();

builder.Services.AddScoped<CreateSubmissionService>();
builder.Services.AddScoped<ListSubmissionsService>();
builder.Services.AddScoped<GetSubmissionService>();

// Repository (Infrastructure)
builder.Services.AddSingleton<IFormRepository, InMemoryFormRepository>();
builder.Services.AddSingleton<ISubmissionRepository, InMemorySubmissionRepository>();


// ✅ Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");

app.MapControllers();




app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
