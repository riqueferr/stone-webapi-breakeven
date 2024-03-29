using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ReadConnection");

builder.Services.AddDbContext<ReadContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddScoped<IAccountBankingService, AccountBankingService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IExtractService, ExtractService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();



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
