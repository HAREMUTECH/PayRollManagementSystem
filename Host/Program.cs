using Coravel;
using FluentValidation.AspNetCore;
using PayRoll.Application;
using PayRoll.Infrastructure;
using PayRoll.Infrastructure.Helpers;
using Serilog;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");


// Add services to the container.


//========= DbContext starts =============
builder.Services.AddDatabase(connectionString);
configuration.SerilogSettings();


builder.Host.ConfigureAppConfiguration((config, context) =>
{

    // context.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //     .AddJsonFile($"appsettings.{config.HostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
    //     .AddEnvironmentVariables();
    MailTemplateHelper.Initialize(config.HostingEnvironment);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});
builder.Services.AddControllers();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
//app.UseCors("MyPolicy");


app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.UseInfrastructure(builder.Configuration);

app.MapControllers();

app.Run();