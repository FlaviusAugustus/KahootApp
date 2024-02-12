using System.Text;
using BracketMaker;
using BracketMaker.Constants;
using BracketMaker.Context;
using BracketMaker.Models;
using BracketMaker.Repository;
using BracketMaker.Services;
using BracketMaker.Services.DateTimeProvider;
using BracketMaker.Services.UserService;
using BracketMaker.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddDbContext<ItemContext>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("database"));
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 
builder.Services.AddControllers();

builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<ItemContext>();
builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequireDigit = false;
    opts.Password.RequiredLength = 0;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequiredUniqueChars = 0;
    opts.Password.RequireNonAlphanumeric = false;
});

//jwt
builder.Services.AddAuthentication(opts =>
    {
        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts =>
    {
        opts.RequireHttpsMetadata = false;
        opts.SaveToken = false;
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            ValidIssuer = builder.Configuration["JWTConfig:Issuer"],
            ValidAudience = builder.Configuration["JWTConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Key"]))
        };
    });

builder.Services.Configure<JWT>(builder.Configuration.GetSection(JWT.JWTConfig));

// roles
var roleManager = builder.Services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole<Guid>>>();
foreach (var role in Enum.GetNames<Role>())
{
    if (!await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole<Guid>(role));
    }
}

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("corsapp");
app.MapHub<GameHub>("/game");
app.MapControllers();
app.Run();