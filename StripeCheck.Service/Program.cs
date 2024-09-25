using Microsoft.EntityFrameworkCore;
using StripeCheck.Application.Admin;
using StripeCheck.Application.DBOperation;
using StripeCheck.Application.Platform;
using StripeCheck.Application.ServiceProvider;
using StripeCheck.Persistence.DBContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbConnection>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your services
builder.Services.AddScoped<ISPOperation, SPOperation>();
builder.Services.AddScoped<IPlatformOperation, PlatformOperation>();
builder.Services.AddScoped<IAdminOperation, AdminOperation>();
builder.Services.AddScoped<DbConnection>();
builder.Services.AddScoped<DBOperations>();


// Add CORS policy to allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()    
               .AllowAnyMethod()    
               .AllowAnyHeader();   
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

// Enable CORS before authorization middleware
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
