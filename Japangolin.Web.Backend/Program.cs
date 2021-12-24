using System.Diagnostics;
using Wacton.Desu.Japanese;
using Wacton.Japangolin.Domain.Commands;
using Wacton.Japangolin.Domain.Mains;
using Wacton.Tovarisch.MVVM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var stopwatch = new Stopwatch();
stopwatch.Start();
var japaneseEntries = JapaneseDictionary.ParseEntries().ToList();
stopwatch.Stop();
Console.WriteLine($"Initialisation took {stopwatch.Elapsed}");

app.MapGet("/random", async () =>
{
    // TODO: extract MVVM stuff from Tovarisch lib
    // create a new "japangolin main" that captures everything required, update it via the desktop UI's command pattern, then return it
    // TODO: does any of this really work if > 1 client is accessing simultaneously?
    var japangolinSettings = new Settings();
    var japangolinMain = new Main(japaneseEntries, japangolinSettings);
    var updateCommand = new UpdateWordAndInflectionCommand(new ModelChangeNotifier(), japangolinMain);
    var wordFilterCommand = new ChangeWordFilterCommand(new ModelChangeNotifier(), japangolinSettings);
    await updateCommand.ExecuteAndNotifyAsync();
    // TODO: allow parameter to allow JLPT N5 toggle - wordFilterCommand.ExecuteAndNotifyAsync()...
    return japangolinMain;
}).WithName("GetRandomJapangolin");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}