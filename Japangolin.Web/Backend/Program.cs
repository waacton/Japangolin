using System.Diagnostics;
using Serilog;
using Serilog.Events;
using Wacton.Desu.Japanese;
using Wacton.Japangolin.Core.Enums;
using Wacton.Japangolin.Core.Mains;
using Wacton.Japangolin.Core.Mutations;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("Japangolin server launched");
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging(); // https://github.com/serilog/serilog-aspnetcore#request-logging

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
app.MapGet("/random", async (bool? jlptN5, Serilog.ILogger logger) =>
{
    var useN5 = jlptN5.HasValue && jlptN5.Value;
    var settings = new Settings();
    var main = new Main(japaneseEntries, settings);

    var wordFilter = useN5 ? WordFilter.JLPTN5 : WordFilter.None;
    var wordFilterMutation = new WordFilterMutation(settings);
    await wordFilterMutation.ExecuteAsync(wordFilter);

    var wordAndInflectionMutation = new WordAndInflectionMutation(main);
    await wordAndInflectionMutation.ExecuteAsync();

    logger.Information("JLPT N5: {N5}, Word: {Word}, Inflection: {Inflection}", useN5, main.Word.English, main.Inflection);
    return main;
}).WithName("GetRandomJapangolin");

app.Run();

static async Task<List<IJapaneseEntry>> GetJapaneseEntries()
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var japaneseEntries = await JapaneseDictionary.ParseEntriesAsync();
    stopwatch.Stop();
    Log.Logger.Information("Japanese dictionary parsed in {Elapsed}", stopwatch.Elapsed);
    return japaneseEntries.ToList();
}