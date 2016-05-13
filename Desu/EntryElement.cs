namespace Wacton.Desu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using Wacton.Tovarisch.Enum;

    public class EntryElement : Enumeration
    {
        public static readonly EntryElement Sequence = new EntryElement("Sequence", "ent_seq", (entry, reader) => entry.Sequence = int.Parse(reader.ReadElementContentAsString()));

        public static readonly EntryElement Kanji = new EntryElement("Kanji", "k_ele", (entry, reader) => entry.StartNewKanji());
        public static readonly EntryElement KanjiText = new EntryElement("KanjiText", "keb", (entry, reader) => entry.CurrentKanji.Text = reader.ReadElementContentAsString());
        public static readonly EntryElement KanjiInformation = new EntryElement("KanjiInformation", "ke_inf", (entry, reader) => entry.CurrentKanji.Informations.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement KanjiPriority = new EntryElement("KanjiPriority", "ke_pri", (entry, reader) => entry.CurrentKanji.Priorities.Add(reader.ReadElementContentAsString()));

        public static readonly EntryElement Reading = new EntryElement("Reading", "r_ele", (entry, reader) => entry.StartNewReading());
        public static readonly EntryElement ReadingText = new EntryElement("ReadingText", "reb", (entry, reader) => entry.CurrentReading.Text = reader.ReadElementContentAsString());
        public static readonly EntryElement ReadingNoKanji = new EntryElement("ReadingNoKanji", "re_nokanji", (entry, reader) => entry.CurrentReading.NoKanji = reader.ReadElementContentAsString());
        public static readonly EntryElement ReadingRestricted = new EntryElement("ReadingRestricted", "re_restr", (entry, reader) => entry.CurrentReading.Restricted.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement ReadingInformation = new EntryElement("ReadingInformation", "re_inf", (entry, reader) => entry.CurrentReading.Informations.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement ReadingPriority = new EntryElement("ReadingInformation", "re_pri", (entry, reader) => entry.CurrentReading.Priorities.Add(reader.ReadElementContentAsString()));

        public static readonly EntryElement Sense = new EntryElement("Sense", "sense", (entry, reader) => entry.StartNewSense());
        public static readonly EntryElement SenseKanjiRestricted = new EntryElement("SenseKanjiRestricted", "stagk", (entry, reader) => entry.CurrentSense.KanjiRestricted.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement SenseReadingRestricted = new EntryElement("SenseReadingRestricted", "stagr", (entry, reader) => entry.CurrentSense.ReadingRestricted.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement PartOfSpeech = new EntryElement("PartOfSpeech", "pos", (entry, reader) => entry.CurrentSense.PartsOfSpeech.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement CrossReference = new EntryElement("CrossReference", "xref", (entry, reader) => entry.CurrentSense.CrossReferences.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement Antonym = new EntryElement("Antonym", "ant", (entry, reader) => entry.CurrentSense.Antonyms.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement Field = new EntryElement("Field", "field", (entry, reader) => entry.CurrentSense.Fields.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement Miscellaneous = new EntryElement("Miscellaneous", "misc", (entry, reader) => entry.CurrentSense.Miscellanea.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement SenseInformation = new EntryElement("SenseInformation", "s_inf", (entry, reader) => entry.CurrentSense.Informations.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement LoanwordSource = new EntryElement("LoanwordSource", "lsource", (entry, reader) => entry.CurrentSense.LoanwordSource.Add(new Gloss(Languages[reader.GetAttribute("xml:lang")], reader.ReadElementContentAsString())));
        public static readonly EntryElement Dialect = new EntryElement("Dialect", "dial", (entry, reader) => entry.CurrentSense.Dialects.Add(reader.ReadElementContentAsString()));
        public static readonly EntryElement Gloss = new EntryElement("Gloss", "gloss", (entry, reader) => entry.CurrentSense.Glosses.Add(new Gloss(Languages[reader.GetAttribute("xml:lang")], reader.ReadElementContentAsString())));

        // TODO: also need enumerations for part of speech, field of application, miscellaneous, priorities, dialects (at least)
        // see http://www.edrdg.org/jmdict/edict_doc.html#IREF05 for good starting point
        private static readonly Dictionary<string, Language> Languages = Enumeration.GetAll<Language>().ToDictionary(language => language.Code, language => language);

        public string Code { get; }
        public readonly Action<JapaneseDictionaryEntry, XmlReader> ReaderAction;

        private static int counter;
        public EntryElement(string displayName, string code, Action<JapaneseDictionaryEntry, XmlReader> readerAction = null)
            : base(counter++, displayName)
        {
            this.Code = code;
            this.ReaderAction = readerAction ?? ((entry, reader) => { });
        }
    }
}
