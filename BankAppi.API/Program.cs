using BankApi.API.Middleware.Extensions;
using BankApi.API.Services;
using BankApp.Data.Profiles;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthenticationExtentsion(
    issuer: builder.Configuration["JwtSettings:Issuer"],
    audience: builder.Configuration["JwtSettings:Audience"],
    signingKey: builder.Configuration["JwtSettings:Secret"]
    );

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("BankAppData");
    return new SqlConnection(connectionString);
});

builder.Services.AddSwaggerExtended();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseSwaggerExtended();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });
app.Run();
