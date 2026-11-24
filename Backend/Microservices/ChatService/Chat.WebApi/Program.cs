using Chat.WebApi.DI;
using Chat.WebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessLogic(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("_allowSignalRCors");

app.MapControllers();
app.MapHub<ChatHub>("/ChatHub").RequireCors("_allowSignalRCors");
app.Run();
