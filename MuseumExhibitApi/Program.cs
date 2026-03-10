using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Dependency injection for repositories and service
builder.Services.AddSingleton<MuseumExhibitApi.Repositories.IAlertRepository, MuseumExhibitApi.Repositories.AlertRepository>();
builder.Services.AddSingleton<MuseumExhibitApi.Repositories.IReportRepository, MuseumExhibitApi.Repositories.ReportRepository>();
builder.Services.AddSingleton<MuseumExhibitApi.Services.ReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
