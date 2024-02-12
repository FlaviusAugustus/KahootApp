using BracketMaker;
using BracketMaker.AppConfigurationExtensions;
using BracketMaker.Services.DateTimeProvider;
using BracketMaker.Services.UserService;
using QuizApi.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.ConfigureIdentity();

builder.Services.ConfigureAuthorization();

builder.Services.ConfigureJwtAuth(builder.Configuration);

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