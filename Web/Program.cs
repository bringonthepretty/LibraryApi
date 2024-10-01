using System.Text;
using Application.Configs;
using Domain.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.ExceptionHandlers;
using Presentation.Requests;
using Presentation.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Web;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterMapsterConfig();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser())
    .AddPolicy("Admin", policy => policy.RequireRole("admin"));

{
    builder.Services.AddScoped<IValidator<CreateOrUpdateAuthorRequest>, CreateOrUpdateAuthorRequestValidator>();
    builder.Services.AddScoped<IValidator<CreateOrUpdateBookRequest>, CreateOrUpdateBookRequestValidator>();
    builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
    builder.Services.AddScoped<IPageAndLimitValidator, PageAndLimitValidator>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
    builder.Services.AddScoped<ISecurity, Security>();
    
    builder.Services.AddScopedFromNamespace("Application.UseCases.BookUseCases");
    builder.Services.AddScopedFromNamespace("Application.UseCases.AuthorUseCases");
    builder.Services.AddScopedFromNamespace("Application.UseCases.UserUseCases");
}

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryDbContext>();

var app = builder.Build();

app.UseStatusCodePages();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();