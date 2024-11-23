using Microsoft.OpenApi.Models;
using MRC_API.Configurations;
using MRC_API.Constant;
using MRC_API.Infrastructure;
using MRC_API.Middlewares;
using MRC_API.Payload.Request.Email;
using MRC_API.Payload.Response.Pay;
using Prepare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy(),
            IgnoreSerializableAttribute = true
        };
    });

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLazyResolution();
// Add other custom services and configurations
builder.Services.AddAuthentication();
builder.Services.AddInfrastructure();
builder.Services.AddDatabase();
builder.Services.AddUnitOfWork();
builder.Services.AddCustomServices();
builder.Services.AddJwtValidation();
builder.Services.Configure<PayOSSettings>(builder.Configuration.GetSection("PayOS"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<VNPaySettings>(builder.Configuration.GetSection("VNPaySettings"));

builder.Services.AddHttpClientServices();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        policyBuilder =>
//        {
//            policyBuilder.AllowAnyOrigin()
//                         .AllowAnyMethod()
//                         .AllowAnyHeader();
//        });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsConstant.PolicyName,
        policy => { policy.WithOrigins("http://localhost:5173", "https://mrc-web-mu.vercel.app", "https://mrc-project.vercel.app", "https://mrc-web-admin.vercel.app", "http://localhost:5174").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });
});

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Description = "Enter 'Bearer' [space] and then your token in the text input below.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            new string[] { }
        },
    };
    c.AddSecurityRequirement(securityRequirement);
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{

    app.UseSwaggerUI();
}
else
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseCors(CorsConstant.PolicyName);
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();
