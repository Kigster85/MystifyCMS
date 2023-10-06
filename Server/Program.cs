using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using System;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
	options.AddPolicy("DevPortfolioPolicy",
					builder =>
					builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.WithExposedHeaders("Access-Control-Allow-Origin")
					.AllowAnyHeader());
	
});

builder.Services.AddDbContext<AppDBContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDefaultIdentity<IdentityUser>()
		.AddRoles<IdentityRole>()
		.AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = builder.Configuration["Jwt:Issuer"],
				ValidAudience = builder.Configuration["Jwt:Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
			};
		});

builder.Services.AddAutoMapper(typeof(DTOMappings));

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	// Seed data or other development-specific configurations
}

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
	swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "MystifyAPI");
	swaggerUIOptions.RoutePrefix = string.Empty;
});
app.UseCors("DevPortfolioPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();




app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
app.Run();
