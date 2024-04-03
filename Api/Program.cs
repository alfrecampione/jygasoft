using DataAccess;
using DataAccess.Repository;
using InitialData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add database service.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<ILoanService, LoanService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();

// Add controller services to build the api.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Takes only one of the controllers in the same route in case of conflict.
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    options.AddPolicy("SpecificOrigins",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});
//-------------

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    // Applies any pending migrations for the context to the database
    dbContext.Database.Migrate();

    var dbInitializer = new DatabaseInitializer(dbContext);
    dbInitializer.EnsureInitialData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("SpecificOrigins");
//app.UseHttpsRedirection();

//app.UseRouting();
app.UseAuthorization();
//app.UseEndpoints();
app.MapControllers();

app.Run();