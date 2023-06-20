using DAL;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PRN231_PE_Trial_API.Utils;
using PRN231_PE_Trial_API.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region EDM Model
var modelBuilder = new ODataConventionModelBuilder();

modelBuilder.EnumType<PropjectRole>();
modelBuilder.EnumType<EmployeeStatus>();

modelBuilder.EntitySet<HRStaff>("HrStaff");
modelBuilder.EntitySet<CompanyProject>("CompanyProject");
//modelBuilder.EntitySet<CompanyProjectViewModel>("CompanyProject").EntityType.HasKey(x => x.CompanyProjectID);
modelBuilder.EntitySet<Department>("Department");
modelBuilder.EntitySet<Employee>("Employee");
modelBuilder.EntitySet<ParticipatingProject>("ParticipatingProject");
//modelBuilder.EntitySet<ParticipatingProjectViewModel>("ParticipatingProject").EntityType.HasKey(pp => new { pp.CompanyProjectID, pp.EmployeeID });

//modelBuilder.EntityType<CompanyProjectViewModel>().HasKey(x => x.CompanyProjectID);
//modelBuilder.EntityType<ParticipatingProjectViewModel>().HasKey(pp => new { pp.CompanyProjectID, pp.EmployeeID });
//modelBuilder.EntityType<EmployeeViewModel>().HasKey(x => x.EmployeeID);
//modelBuilder.EntityType<HrStaffViewModel>().HasKey(x => x.Email);
//modelBuilder.EntityType<DepartmentViewModel>().HasKey(x => x.DepartmentID);
#endregion



builder.Services.AddControllers().AddOData(
        options =>
        {
            options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents("odata", modelBuilder.GetEdmModel());

        });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
        options =>
        {
            // Add a swagger document for each discovered endpoint
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization"
            });

            // Add the JWT bearer token support
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] { }
                }
            });

            // Add a custom operation filter which sets default values
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


            //// maps api/ v1 to v1. skips /$metadata, /$count
            //options.DocInclusionPredicate((docName, apiDesc) =>
            //{
            //    if (apiDesc.RelativePath.Contains('$')) return false;
            //    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

            //    var routeAttrs = methodInfo.DeclaringType
            //        .GetCustomAttributes(true)
            //        .OfType<ODataRouteComponentAttribute>();
            //    var versions = routeAttrs.Select(i => i.RoutePrefix.Split('/').Last());
            //    return versions.Any(v => string.Equals(v, docName, StringComparison.CurrentCultureIgnoreCase));
            //});

            options.CustomSchemaIds(type => type.FullName);
            options.OperationFilter<ODataOperationFilter>();


            //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }
    );




builder.Services.AddDbContext<CompanyXDbContext>();

builder.Services.AddScoped<HrStaffRepository>();
builder.Services.AddScoped<CompanyProjectRepository>();
builder.Services.AddScoped<ParticipatingProjectRepository>();
builder.Services.AddScoped<EmployeeRepository>();


// Add Authentication services
builder.Services.AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(
        options =>
        {
            // Configure JWT Bearer Auth to expect our security key
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "CompanyX",
                ValidateAudience = false,
                ValidAudience = "CompanyX",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SecretKey").Value))
            };
        }
    );


//Allow CROSs - origin
var CORS_CONFIG = "_CORS_CONFIG";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_CONFIG,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7263").AllowAnyHeader().AllowAnyMethod();
                      });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CORS_CONFIG);

app.UseODataBatching();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
