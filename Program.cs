using BracketMaker;
using BracketMaker.AppConfigurationExtensions;
using BracketMaker.AuthHandlers.Requirements;
using BracketMaker.Services.DateTimeProvider;
using BracketMaker.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using QuizApi.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthorizationHandler, RemoveQuizHandler>();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.ConfigureAuthorization();

builder.Services.ConfigureIdentity();

builder.Services.ConfigureJwtAuth(builder.Configuration);

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


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