namespace Wacton.Desu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using Wacton.Tovarisch.Enum;

    public class EntryElement : Enumeration
    {
        public static readonly EntryElement Sequence = new EntryElement("Sequence", "ent_seq", (entry, data) => entry.Sequence = int.Parse(data.Content));

        public static readonly EntryElement Kanji = new EntryElement("Kanji", "k_ele", (entry, data) => entry.StartNewKanji(), false);
        public static readonly EntryElement KanjiText = new EntryElement("KanjiText", "keb", (entry, data) => entry.CurrentKanji.Text = data.Content);
        public static readonly EntryElement KanjiInformation = new EntryElement("KanjiInformation", "ke_inf", (entry, data) => entry.CurrentKanji.Informations.Add(data.Content));
        public static readonly EntryElement KanjiPriority = new EntryElement("KanjiPriority", "ke_pri", (entry, data) => entry.CurrentKanji.Priorities.Add(data.Content));

        public static readonly EntryElement Reading = new EntryElement("Reading", "r_ele", (entry, data) => entry.StartNewReading(), false);
        public static readonly EntryElement ReadingText = new EntryElement("ReadingText", "reb", (entry, data) => entry.CurrentReading.Text = data.Content);
        public static readonly EntryElement ReadingNoKanji = new EntryElement("ReadingNoKanji", "re_nokanji", (entry, data) => entry.CurrentReading.NoKanji = data.Content);
        public static readonly EntryElement ReadingRestricted = new EntryElement("ReadingRestricted", "re_restr", (entry, data) => entry.CurrentReading.Restricted.Add(data.Content));
        public static readonly EntryElement ReadingInformation = new EntryElement("ReadingInformation", "re_inf", (entry, data) => entry.CurrentReading.Informations.Add(data.Content));
        public static readonly EntryElement ReadingPriority = new EntryElement("ReadingInformation", "re_pri", (entry, data) => entry.CurrentReading.Priorities.Add(data.Content));

        public static readonly EntryElement Sense = new EntryElement("Sense", "sense", (entry, data) => entry.StartNewSense(), false);
        public static readonly EntryElement SenseKanjiRestricted = new EntryElement("SenseKanjiRestricted", "stagk", (entry, data) => entry.CurrentSense.KanjiRestricted.Add(data.Content));
        public static readonly EntryElement SenseReadingRestricted = new EntryElement("SenseReadingRestricted", "stagr", (entry, data) => entry.CurrentSense.ReadingRestricted.Add(data.Content));
        public static readonly EntryElement PartOfSpeech = new EntryElement("PartOfSpeech", "pos", (entry, data) => entry.CurrentSense.PartsOfSpeech.Add(data.Content));
        public static readonly EntryElement CrossReference = new EntryElement("CrossReference", "xref", (entry, data) => entry.CurrentSense.CrossReferences.Add(data.Content));
        public static readonly EntryElement Antonym = new EntryElement("Antonym", "ant", (entry, data) => entry.CurrentSense.Antonyms.Add(data.Content));
        public static readonly EntryElement Field = new EntryElement("Field", "field", (entry, data) => entry.CurrentSense.Fields.Add(data.Content));
        public static readonly EntryElement Miscellaneous = new EntryElement("Miscellaneous", "misc", (entry, data) => entry.CurrentSense.Miscellanea.Add(data.Content));
        public static readonly EntryElement SenseInformation = new EntryElement("SenseInformation", "s_inf", (entry, data) => entry.CurrentSense.Informations.Add(data.Content));
        public static readonly EntryElement LoanwordSource = new EntryElement("LoanwordSource", "lsource", (entry, data) => entry.CurrentSense.LoanwordSources.Add(new LoanwordGloss(data.Content, Languages[data.LanguageAttribute], data.GlossGenderAttribute, data.LoanwordTypeAttribute != null, data.LoanwordWaseiAttribute != null)));
        public static readonly EntryElement Dialect = new EntryElement("Dialect", "dial", (entry, data) => entry.CurrentSense.Dialects.Add(Dialects[data.Content]));
        public static readonly EntryElement Gloss = new EntryElement("Gloss", "gloss", (entry, data) => entry.CurrentSense.Glosses.Add(new Gloss(data.Content, Languages[data.LanguageAttribute], data.GlossGenderAttribute)));

        // TODO: also need enumerations for part of speech, field of application, miscellaneous, priorities, dialects (at least)
        // see http://www.edrdg.org/jmdict/edict_doc.html#IREF05 for good starting point
        private static readonly Dictionary<string, Language> Languages = Enumeration.GetAll<Language>().ToDictionary(language => language.Code, language => language);
        private static readonly Dictionary<string, Dialect> Dialects = Enumeration.GetAll<Dialect>().ToDictionary(dialect => dialect.Code, dialect => dialect);

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

        public void AddDataToEntry(JapaneseDictionaryEntry entry, EntryElementData data)
        {
            this.addDataToEntryAction(entry, data);
        }
    }
}
