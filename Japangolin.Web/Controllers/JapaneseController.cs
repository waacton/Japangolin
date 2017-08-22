using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

using Wacton.Desu.Japanese;
using Wacton.Desu.Enums;

namespace ReactDemo.Controllers
{
    [Route("api/[controller]")]
    public class JapaneseController : Controller
    {
        private static IJapaneseDictionary japaneseDictionary = new JapaneseDictionary();
        private static List<IJapaneseEntry> japaneseEntries;

        // [HttpGet("{id}")]
        [HttpGet("[action]")]
        public JapanesePhrase Get(int id)
        {
            if (japaneseEntries == null)
            {
                japaneseEntries = japaneseDictionary.GetEntries().ToList();
            }

            var entry = japaneseEntries[id];
            return new JapanesePhrase 
            { 
                Kana = entry.Readings.First().Text, 
                English = entry.Senses.First().Glosses.Where(gloss => gloss.Language == Language.English).First().Term,
                Kanji = entry.Kanjis.Any() ? entry.Kanjis.First().Text : "n/a"
            };
        }
    }
}
