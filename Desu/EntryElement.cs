namespace Wacton.Desu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using Wacton.Tovarisch.Enum;

    public class EntryElement : Enumeration
    {
        public static readonly EntryElement Sequence = new EntryElement("Sequence", "ent_seq", (entry, reader) => entry.SetIdentifier(int.Parse(reader.ReadString())));

        public static readonly EntryElement Kanji = new EntryElement("Kanji", "k_ele");
        public static readonly EntryElement KanjiText = new EntryElement("KanjiText", "keb", (entry, reader) => entry.AddKanji(reader.ReadString()));
        public static readonly EntryElement KanjiInformation = new EntryElement("KanjiInformation", "ke_inf");
        public static readonly EntryElement KanjiPriority = new EntryElement("KanjiPriority", "ke_pri");

        public static readonly EntryElement Reading = new EntryElement("Reading", "r_ele");
        public static readonly EntryElement ReadingText = new EntryElement("ReadingText", "reb", (entry, reader) => entry.AddKana(reader.ReadString()));
        public static readonly EntryElement ReadingNoKanji = new EntryElement("ReadingNoKanji", "re_nokanji");
        public static readonly EntryElement ReadingRestricted = new EntryElement("ReadingRestricted", "re_restr");
        public static readonly EntryElement ReadingInformation = new EntryElement("ReadingInformation", "re_inf");
        public static readonly EntryElement ReadingPriority = new EntryElement("ReadingInformation", "re_pri");

        public static readonly EntryElement Sense = new EntryElement("Sense", "sense");
        public static readonly EntryElement SenseKanaRestricted = new EntryElement("SenseKanaRestricted", "stagk");
        public static readonly EntryElement SenseReadingRestricted = new EntryElement("SenseReadingRestricted", "stagr");
        public static readonly EntryElement PartOfSpeech = new EntryElement("PartOfSpeech", "pos", (entry, reader) => entry.AddSpeechMarking(reader.ReadString()));
        public static readonly EntryElement CrossReference = new EntryElement("CrossReference", "xref");
        public static readonly EntryElement Antonym = new EntryElement("Antonym", "ant");
        public static readonly EntryElement Field = new EntryElement("Field", "field", (entry, reader) => entry.AddFieldMarking(reader.ReadString()));
        public static readonly EntryElement Miscellaneous = new EntryElement("Miscellaneous", "misc", (entry, reader) => entry.AddMiscellaneousMarking(reader.ReadString()));
        public static readonly EntryElement SenseInformation = new EntryElement("SenseInformation", "s_inf");
        public static readonly EntryElement LoanwordSource = new EntryElement("LoanwordSource", "lsource");
        public static readonly EntryElement Dialect = new EntryElement("Dialect", "dial");
        public static readonly EntryElement Gloss = new EntryElement("Gloss", "gloss", (entry, reader) => entry.AddTranslation(Glosses[reader.GetAttribute(0)], reader.ReadString()));

        // TODO: also need enumerations for part of speech, field of application, miscellaneous, priorities, dialects (at least)
        // see http://www.edrdg.org/jmdict/edict_doc.html for good starting point
        private static readonly Dictionary<string, Gloss> Glosses = Enumeration.GetAll<Gloss>().ToDictionary(gloss => gloss.Code, gloss => gloss);

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
