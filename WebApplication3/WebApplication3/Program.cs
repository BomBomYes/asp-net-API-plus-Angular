using Microsoft.EntityFrameworkCore;
using SimpleBlog;
using SimpleBlog.Repositories;
using SimpleBlog.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавление подключения к базе данных PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options => 
     options.UseNpgsql(connectionString));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowLocalhost4200",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost4200");

app.UseAuthorization();

app.MapControllers();

app.Run();