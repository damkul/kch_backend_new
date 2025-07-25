using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KchDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("KchDb")));
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
