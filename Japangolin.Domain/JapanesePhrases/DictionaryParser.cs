namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    public class DictionaryParser
    {
        private const string DictionaryName = "JMdict";
        private const string IdentifierNode = "ent_seq";
        private const string KanjiNode = "keb";
        private const string KanaNode = "reb";
        private const string MeaningNode = "gloss";
        private const string SpeechNode = "pos";
        private const string FieldNode = "field";
        private const string MiscNode = "misc";

        public IEnumerable<DictionaryEntry> GetEntries()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var resourceName = resourceNames.Single(resource => resource.Contains(DictionaryName));
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };
                using (var reader = XmlReader.Create(stream, settings))
                {
                    return ParseDictionary(reader);
                }
            }
        }

        private static IEnumerable<DictionaryEntry> ParseDictionary(XmlReader reader)
        {
            var dictionaryEntries = new List<DictionaryEntry>();
            var entryActions = new Dictionary<string, Action<DictionaryEntry>>
                               {
                                   { IdentifierNode, entry => entry.SetIdentifier(int.Parse(reader.ReadString())) },
                                   { KanjiNode, entry => entry.AddKanji(reader.ReadString()) },
                                   { KanaNode, entry => entry.AddKana(reader.ReadString()) },
                                   { MeaningNode, entry => AddEnglishIfPresent(reader, entry) },
                                   { SpeechNode, entry => entry.AddSpeechMarking(reader.ReadString()) },
                                   { FieldNode, entry => entry.AddFieldMarking(reader.ReadString()) },
                                   { MiscNode, entry => entry.AddMiscellaneousMarking(reader.ReadString()) }
                               };

            reader.MoveToContent();
            while (reader.Read())
            {
                if (!reader.IsStartElement() || reader.Name != "entry")
                {
                    continue;
                }

                // now within an entry, can now create data entry
                var dictionaryEntry = new DictionaryEntry();

                // keep reading until the end entry tag is reached
                var isEndOfEntry = false;
                while (!isEndOfEntry)
                {
                    reader.Read();
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        var nodeName = reader.Name;
                        if (entryActions.ContainsKey(nodeName))
                        {
                            entryActions[nodeName](dictionaryEntry);
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        isEndOfEntry = reader.Name == "entry";
                    }
                }

                dictionaryEntries.Add(dictionaryEntry);
            }

            return dictionaryEntries;
        }

        private static void AddEnglishIfPresent(XmlReader reader, DictionaryEntry entry)
        {
            var language = reader.GetAttribute(0);
            if (language == "eng")
            {
                entry.AddEnglish(reader.ReadString());
            }
        }
    }
}
