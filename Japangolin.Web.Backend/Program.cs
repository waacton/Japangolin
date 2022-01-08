using System.Diagnostics;
using Wacton.Desu.Japanese;
using Wacton.Japangolin.Domain.Commands;
using Wacton.Japangolin.Domain.Enums;
using Wacton.Japangolin.Domain.Mains;
using Wacton.Japangolin.Domain.MVVM;

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

// enables static /wwwroot/index.html to be served by default
if (app.Environment.IsProduction())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

var japaneseEntries = await GetJapaneseEntries();

/*
 * could declare parameters as part of the route pattern (e.g. /random/{jlptN5} --> /random/true or /random/false)
 * but optional parameters feel more natural here:
 * /random --> no filter, all words
 * /random?jlptN5=false --> no filter, all words
 * /random?jlptN5=true --> only JLPT N5 words
 * see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#request-handling
 */
app.MapGet("/random", async (bool? jlptN5) =>
{
    // create a new "japangolin main" that captures everything required, update it via the desktop UI's command pattern, then return it
    var japangolinSettings = new Settings();
    var japangolinMain = new Main(japaneseEntries, japangolinSettings);

    var wordFilter = jlptN5.HasValue && jlptN5.Value ? WordFilter.JLPTN5 : WordFilter.None;
    var wordFilterCommand = new ChangeWordFilterCommand(new ModelChangeNotifier(), japangolinSettings);
    await wordFilterCommand.ExecuteAndNotifyAsync(wordFilter);
    
    var updateCommand = new UpdateWordAndInflectionCommand(new ModelChangeNotifier(), japangolinMain);
    await updateCommand.ExecuteAndNotifyAsync();
    
    return japangolinMain;
}).WithName("GetRandomJapangolin");

app.Run();

static async Task<List<IJapaneseEntry>> GetJapaneseEntries()
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var japaneseEntries = await JapaneseDictionary.ParseEntriesAsync();
    stopwatch.Stop();
    Console.WriteLine($"Initialisation took {stopwatch.Elapsed}");
    return japaneseEntries.ToList();
}