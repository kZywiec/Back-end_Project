using Core.Data;
using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<DocumentRepository>();
builder.Services.AddScoped<LogRepository>();

// Register services
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<TokenService>(provider =>
{
    var secretKey = builder.Configuration["Token:SecretKey"];
    var expirationTime = TimeSpan.FromMinutes(int.Parse(builder.Configuration["Token:ExpirationMinutes"]));
    return new TokenService(secretKey, expirationTime);
});
builder.Services.AddScoped<HashingService>();
builder.Services.AddScoped<LogService>();

// Add controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceProvider = new ServiceCollection()
    .AddDbContext<ProjectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")))
    .BuildServiceProvider();

var _ProjectContext = serviceProvider.GetService<ProjectContext>();
//_ProjectContext.Database.EnsureDeleted();
_ProjectContext.Database.EnsureCreated();

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
