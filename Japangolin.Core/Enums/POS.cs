namespace Wacton.Japangolin.Core.Enums;

using System.Collections.Generic;
using Wacton.Desu.Enums;

public static class POS
{
    public static readonly List<PartOfSpeech> Nouns = new() { PartOfSpeech.NounCommon, PartOfSpeech.NounNo, PartOfSpeech.NounSuru };
    public static readonly List<PartOfSpeech> AdjectivesNa = new() { PartOfSpeech.AdjectiveNa };
    public static readonly List<PartOfSpeech> AdjectivesI = new() { PartOfSpeech.AdjectiveI };
    public static readonly List<PartOfSpeech> VerbsRu = new() { PartOfSpeech.Verb1 };
    public static readonly List<PartOfSpeech> VerbsU = new() { PartOfSpeech.Verb5U, PartOfSpeech.Verb5Tsu, PartOfSpeech.Verb5Ru, PartOfSpeech.Verb5Mu, PartOfSpeech.Verb5Bu, PartOfSpeech.Verb5Nu, PartOfSpeech.Verb5Ku, PartOfSpeech.Verb5Gu, PartOfSpeech.VerbSu };
}