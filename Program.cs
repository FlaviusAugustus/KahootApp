using BracketMaker;
using BracketMaker.Context;
using BracketMaker.ItemContext;
using BracketMaker.Models;
using BracketMaker.Repository;
using BracketMaker.Services;
using Microsoft.EntityFrameworkCore;

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
app.UseCors("corsapp");
app.MapHub<GameHub>("/game");
app.MapControllers();
app.Run();