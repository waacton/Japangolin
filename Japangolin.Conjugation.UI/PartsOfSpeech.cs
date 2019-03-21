namespace Wacton.Japangolin.Conjugation.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Wacton.Desu.Enums;

    public static class PartsOfSpeech
    {
        public static readonly List<PartOfSpeech> AllNouns = new List<PartOfSpeech> { PartOfSpeech.NounCommon, PartOfSpeech.NounAdverbial, PartOfSpeech.NounNo, PartOfSpeech.NounSuru, PartOfSpeech.NounTemporal };
        public static readonly List<PartOfSpeech> VerbsIchidan = new List<PartOfSpeech> { PartOfSpeech.Verb1 };
        public static readonly List<PartOfSpeech> VerbsGodan = new List<PartOfSpeech> { PartOfSpeech.Verb5U, PartOfSpeech.Verb5Tsu, PartOfSpeech.Verb5Ru, PartOfSpeech.Verb5Mu, PartOfSpeech.Verb5Bu, PartOfSpeech.Verb5Nu, PartOfSpeech.Verb5Ku, PartOfSpeech.Verb5Gu, PartOfSpeech.VerbSu };
        public static readonly List<PartOfSpeech> AllVerbs = VerbsIchidan.Concat(VerbsGodan).ToList();
        public static readonly List<PartOfSpeech> AllAdjectives = new List<PartOfSpeech> { PartOfSpeech.AdjectiveI, PartOfSpeech.AdjectiveNa };
    }
}
