using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Load Serilog configuration from appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KchDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("KchDb")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<ICateringService, CateringService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDecorationService, DecorationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IVendorPaymentService, VendorPaymentService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<ITimelineService, TimelineService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()   // Allow any domain (you can restrict later)
                .AllowAnyMethod()   // GET, POST, PUT, DELETE
                .AllowAnyHeader();  // Allow custom headers
        });
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5047); // HTTP
    
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting up the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}


app.Run();
