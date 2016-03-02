namespace Wacton.Japangolin.Domain.JapanesePhrases
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    using Wacton.Tovarisch.Randomness;

    public class JapanesePhraseRepository : IJapanesePhraseRepository
    {
        private List<JapanesePhrase> japanesePhrases;

        public int PhraseCount { get; }

        public JapanesePhraseRepository()
        {
            this.Initialise();
            this.PhraseCount = this.japanesePhrases.Count;
        }

        public JapanesePhrase GetPhrase(int index)
        {
            return index < this.PhraseCount ? this.japanesePhrases[index] : null;
        }

        public JapanesePhrase GetRandomPhrase()
        {
            return RandomSelection.SelectOne(this.japanesePhrases);
        }

        public void Initialise()
        {
            var transliterator = new Transliterator();

            var dictionaryEntries = new List<DictionaryEntry>();

            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var resourceName = resourceNames.Single(resource => resource.Contains("JMdict"));
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (XmlReader reader = XmlReader.Create(stream, settings))
                {
                    reader.MoveToContent();

                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name == "entry")
                        {
                            // within an entry, can now create data entry
                            var dictionaryEntry = new DictionaryEntry();

                            // keep reading until the end entry tag is reached
                            var isEndOfEntry = false;
                            while (!isEndOfEntry)
                            {
                                reader.Read();
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "ent_seq":
                                            dictionaryEntry.SetIdentifier(int.Parse(reader.ReadString()));
                                            break;
                                        case "keb":
                                            dictionaryEntry.AddKanji(reader.ReadString());
                                            break;
                                        case "reb":
                                            dictionaryEntry.AddKana(reader.ReadString());
                                            break;
                                        case "gloss":
                                            var language = reader.GetAttribute(0);
                                            if (language == "eng")
                                            {
                                                dictionaryEntry.AddEnglish(reader.ReadString());
                                            }

                                            break;
                                    }
                                }
                                else if (reader.NodeType == XmlNodeType.EndElement)
                                {
                                    isEndOfEntry = reader.Name == "entry";
                                }
                            }

                            dictionaryEntries.Add(dictionaryEntry);
                        }
                    }
                }
            }

            this.japanesePhrases = new List<JapanesePhrase>();
            var unprocessed = new List<string>();
            foreach (var structure in dictionaryEntries)
            {
                var meaning = structure.English;
                var kanji = structure.Kanji;
                var entryId = structure.Identifier;

                foreach (var kana in structure.Kana)
                {
                    var romaji = transliterator.GetRomaji(kana);
                    if (romaji == null)
                    {
                        unprocessed.Add(kana);
                    }
                    else
                    {
                        this.japanesePhrases.Add(new JapanesePhrase(kana, romaji, meaning, kanji, entryId));
                    }
                }
            }
        }
    }
}