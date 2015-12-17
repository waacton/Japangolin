namespace Wacton.Japangolin.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    using Wacton.Tovarisch.Randomness;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var transliterator = new Transliterator();

            var structures = new List<Structure>();

            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse };
            using (XmlReader reader = XmlReader.Create(@"Resources\JMdict", settings))
            {
                reader.MoveToContent();

                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name == "entry")
                    {
                        // within an entry, can now create data entry
                        var structure = new Structure();

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
                                        structure.SetIdentifier(int.Parse(reader.ReadString()));
                                        break;
                                    case "keb":
                                        structure.AddKanji(reader.ReadString());
                                        break;
                                    case "reb":
                                        structure.AddKana(reader.ReadString());
                                        break;
                                    case "gloss":
                                        var language = reader.GetAttribute(0);
                                        if (language == "eng")
                                        {
                                            structure.SetEnglish(reader.ReadString());
                                        }

                                        break;
                                }
                            }
                            else if (reader.NodeType == XmlNodeType.EndElement)
                            {
                                isEndOfEntry = reader.Name == "entry";
                            }
                        }

                        structures.Add(structure);
                    }
                }
            }

            // TODO: consider loading a list of recorded correct answers, strip them out of a "not yet done" list
            var transliteratedKanas = new List<TransliteratedKana>();
            var unprocessed = new List<string>();
            foreach (var structure in structures)
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
                        transliteratedKanas.Add(new TransliteratedKana(kana, romaji, meaning, kanji, entryId));
                    }
                }
            }

            var randomItem = RandomSelection.SelectOne(transliteratedKanas);
            var randomKana = randomItem.Kana;
        }

        public class Structure
        {
            private bool isIdentifierSet;
            public int Identifier { get; private set; }
            public List<string> Kanji { get; private set; }
            public List<string> Kana { get; private set; } 
            public string English { get; private set; }

            public List<string> Unprocessed { get; private set; } 

            public Structure()
            {
                this.Kanji = new List<string>();
                this.Kana = new List<string>();
                this.Unprocessed = new List<string>();
            }

            public void SetIdentifier(int identifier)
            {
                if (this.isIdentifierSet)
                {
                    throw new InvalidOperationException("Identifier is already set");
                }

                this.Identifier = identifier;
                this.isIdentifierSet = true;
            }

            public void AddKanji(string kanji)
            {
                this.Kanji.Add(kanji);
            }

            public void AddKana(string kana)
            {
                if (!this.Kana.Contains(kana))
                {
                    this.Kana.Add(kana);
                }
            }

            public void SetEnglish(string english)
            {
                this.English = english;
            }

            public override string ToString()
            {
                var stringbuilder = new StringBuilder();
                stringbuilder.Append(this.Identifier.ToString() + ": ");

                foreach (var kanji in this.Kanji)
                {
                    stringbuilder.Append(kanji + " | ");
                }

                foreach (var kana in this.Kana)
                {
                    stringbuilder.Append(kana + " | ");
                }

                stringbuilder.Append(this.English);
                return stringbuilder.ToString();
            }
        }
    }
}
