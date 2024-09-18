using System.Text;
using Application.Dtos;
using Application.Services.Api;
using Application.Services.Implementations;
using Application.Validators;
using Domain.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IBookService, BookService>();
    builder.Services.AddScoped<IAuthorService, AuthorService>();
    builder.Services.AddScoped<IAuthService, AuthService>();

    builder.Services.AddScoped<IValidator<BookDto>, BookValidator>();
    builder.Services.AddScoped<IValidator<AuthorDto>, AuthorValidator>();
    builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
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