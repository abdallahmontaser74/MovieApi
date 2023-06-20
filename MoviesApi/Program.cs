using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesApi.Models;
using MoviesApi.Services.Genres;
using MoviesApi.Services.Genres.Repositories;
using MoviesApi.Services.Movies;
using MoviesApi.Services.Movies.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IGenresRepository, GenresRepository>();
builder.Services.AddTransient<IGenresService, GenresService>();

builder.Services.AddTransient<IMoviesRepository, MoviesRepository>();
builder.Services.AddTransient<IMoviesService, MoviesService>();

builder.Services.AddSwaggerGen(options=>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movie Api",
        Contact = new OpenApiContact
        {
            Name = "Abdo",
            Email = "abdallahmontaser71@gmail.com"
        }
    });

});

var app = builder.Build();

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
