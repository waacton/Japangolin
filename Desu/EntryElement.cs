namespace Wacton.Desu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wacton.Tovarisch.Enum;

    public class EntryElement : Enumeration
    {
        public static readonly EntryElement Sequence = new EntryElement("Sequence", "ent_seq", (entry, data) => entry.SequenceString = data.Content);

        public static readonly EntryElement Kanji = new EntryElement("Kanji", "k_ele", (entry, data) => entry.StartNewKanji(), false);
        public static readonly EntryElement KanjiText = new EntryElement("KanjiText", "keb", (entry, data) => entry.CurrentKanji.Text = data.Content);
        public static readonly EntryElement KanjiInformation = new EntryElement("KanjiInformation", "ke_inf", (entry, data) => AddContent(entry.CurrentKanji.Informations, data));
        public static readonly EntryElement KanjiPriority = new EntryElement("KanjiPriority", "ke_pri", (entry, data) => AddContent(entry.CurrentKanji.Priorities, data));

        public static readonly EntryElement Reading = new EntryElement("Reading", "r_ele", (entry, data) => entry.StartNewReading(), false);
        public static readonly EntryElement ReadingText = new EntryElement("ReadingText", "reb", (entry, data) => entry.CurrentReading.Text = data.Content);
        public static readonly EntryElement ReadingNoKanji = new EntryElement("ReadingNoKanji", "re_nokanji", (entry, data) => entry.CurrentReading.NoKanji = data.Content);
        public static readonly EntryElement ReadingRestricted = new EntryElement("ReadingRestricted", "re_restr", (entry, data) => AddContent(entry.CurrentReading.Restricted, data));
        public static readonly EntryElement ReadingInformation = new EntryElement("ReadingInformation", "re_inf", (entry, data) => AddContent(entry.CurrentReading.Informations, data));
        public static readonly EntryElement ReadingPriority = new EntryElement("ReadingPriority", "re_pri", (entry, data) => AddContent(entry.CurrentReading.Priorities, data));

        public static readonly EntryElement Sense = new EntryElement("Sense", "sense", (entry, data) => entry.StartNewSense(), false);
        public static readonly EntryElement SenseKanjiRestricted = new EntryElement("SenseKanjiRestricted", "stagk", (entry, data) => AddContent(entry.CurrentSense.KanjiRestricted, data));
        public static readonly EntryElement SenseReadingRestricted = new EntryElement("SenseReadingRestricted", "stagr", (entry, data) => AddContent(entry.CurrentSense.ReadingRestricted, data));
        public static readonly EntryElement PartOfSpeech = new EntryElement("PartOfSpeech", "pos", (entry, data) => AddContent(entry.CurrentSense.PartsOfSpeech, data));
        public static readonly EntryElement CrossReference = new EntryElement("CrossReference", "xref", (entry, data) => AddContent(entry.CurrentSense.CrossReferences, data));
        public static readonly EntryElement Antonym = new EntryElement("Antonym", "ant", (entry, data) => AddContent(entry.CurrentSense.Antonyms, data));
        public static readonly EntryElement Field = new EntryElement("Field", "field", (entry, data) => AddContent(entry.CurrentSense.Fields, data, Fields));
        public static readonly EntryElement Miscellaneous = new EntryElement("Miscellaneous", "misc", (entry, data) => AddContent(entry.CurrentSense.Miscellanea, data));
        public static readonly EntryElement SenseInformation = new EntryElement("SenseInformation", "s_inf", (entry, data) => AddContent(entry.CurrentSense.Informations, data));
        public static readonly EntryElement LoanwordSource = new EntryElement("LoanwordSource", "lsource", (entry, data) => AddLoanwordGloss(entry.CurrentSense.LoanwordSources, data));
        public static readonly EntryElement Dialect = new EntryElement("Dialect", "dial", (entry, data) => AddContent(entry.CurrentSense.Dialects, data, Dialects));
        public static readonly EntryElement Gloss = new EntryElement("Gloss", "gloss", (entry, data) => AddGloss(entry.CurrentSense.Glosses, data));

        // TODO: also need enumerations for part of speech, miscellaneous, priorities, etc.
        // see http://www.edrdg.org/jmdict/edict_doc.html#IREF05 for good starting point
        private static readonly Dictionary<string, Language> Languages = Enumeration.GetAll<Language>().ToDictionary(language => language.Code, language => language);

        private static readonly Dictionary<string, Dialect> Dialects = Enumeration.GetAll<Dialect>().ToDictionary(dialect => dialect.Code, dialect => dialect);
        private static readonly Dictionary<string, Field> Fields = Enumeration.GetAll<Field>().ToDictionary(field => field.Code, field => field);

        public string Code { get; }
        public bool ExpectsContent { get; }
        private readonly Action<JapaneseDictionaryEntry, EntryElementData> addDataToEntryAction;

        private static int counter;
        public EntryElement(string displayName, string code, Action<JapaneseDictionaryEntry, EntryElementData> addDataToEntryAction = null, bool expectsContent = true)
            : base(counter++, displayName)
        {
            this.Code = code;
            this.addDataToEntryAction = addDataToEntryAction ?? ((entry, data) => { });
            this.ExpectsContent = expectsContent;
        }

        internal void AddDataToEntry(JapaneseDictionaryEntry entry, EntryElementData data)
        {
            this.addDataToEntryAction(entry, data);
        }

        internal static void AddContent(List<string> list, EntryElementData data)
        {
            list.Add(data.Content);
        }

        internal static void AddContent<T>(List<T> list, EntryElementData data, Dictionary<string, T> lookupDictionary)
        {
            list.Add(lookupDictionary[data.Content]);
        }

        internal static void AddGloss(List<Gloss> list, EntryElementData data)
        {
            list.Add(new Gloss(data.Content, Languages[data.LanguageAttribute], data.GlossGenderAttribute));
        }

        internal static void AddLoanwordGloss(List<LoanwordGloss> list, EntryElementData data)
        {
            list.Add(new LoanwordGloss(data.Content, Languages[data.LanguageAttribute], data.GlossGenderAttribute, data.LoanwordTypeAttribute != null, data.LoanwordWaseiAttribute != null));
        }
    }
}
