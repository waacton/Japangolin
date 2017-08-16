using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Wacton.Desu.Japanese;

namespace ReactDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var japaneseDictionary = new JapaneseDictionary();
            var entries = japaneseDictionary.GetEntries();
            var entry = entries.ToList().Last();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
