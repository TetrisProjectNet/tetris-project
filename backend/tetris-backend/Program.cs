global using tetris_backend.Models;
global using tetris_backend.Services;
global using tetris_backend.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TetrisProjectDatabaseSettings>(
    builder.Configuration.GetSection("TetrisProjectDatabase"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ShopItemService>();
builder.Services.AddSingleton<VerificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
