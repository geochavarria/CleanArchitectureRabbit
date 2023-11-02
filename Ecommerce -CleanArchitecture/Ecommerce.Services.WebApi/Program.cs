using Asp.Versioning.ApiExplorer;
using Ecommerce.Application.UseCases;
using Ecommerce.Infraestructure;
using Ecommerce.Persistence;
using Ecommerce.Services.WebApi.Features;
using Ecommerce.Services.WebApi.HealthChecks;
using Ecommerce.Services.WebApi.Helpers;
using Ecommerce.Services.WebApi.Injection;
using Ecommerce.Services.WebApi.RateLimiter;
using Ecommerce.Services.WebApi.Redis;
using Ecommerce.Services.WebApi.Swagger;
using Ecommerce.Services.WebApi.Versioning;
using Ecommerce.Services.WebApi.Watch;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WatchDog;


var myPolice = "policyCustom";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Config = builder.Configuration;
var Services = builder.Services;

Services.AddFeatures(config: Config);

var appSettingSection = Config.GetSection("Config");
Services.Configure<AppSettings>(appSettingSection);
var appSetting = appSettingSection.Get<AppSettings>();

Services.AddPersistence(Config);
Services.AddApplicationServices();
Services.AddInfraestructureServices();
Services.AddInjection(Config);


var key = Encoding.ASCII.GetBytes(appSetting.Secret);
var Issuer = appSetting.Issuer;
var Audience = appSetting.Audience;

Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userID = int.Parse(context.Principal.Identity.Name);
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

Services.AddHealthCheck(Config);
Services.AddWatchDog(Config);
Services.AddRedisCache(Config);
Services.AddRateLimiter(Config);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

Services.AddVersioning();
Services.AddSwagger();


var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(o =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});

app.UseReDoc(opt =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        opt.DocumentTitle = "Ecommerce TECH Api Market";
        opt.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
    }
});

app.UseWatchDogExceptionLogger();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseRateLimiter();
app.UseEndpoints(_ => { });
app.UseCors(myPolice);

app.UseHttpsRedirection();



app.MapControllers();

//Healths Checks
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

});

app.UseWatchDog(conf =>
{
    conf.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
    conf.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUserName"];
});

app.Run();


//PAra Pruebas unitarias
public partial class Program { };