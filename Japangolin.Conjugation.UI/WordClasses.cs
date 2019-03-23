namespace Wacton.Japangolin.Conjugation
{
    using System.Collections.Generic;
    using System.Linq;

    public static class WordClasses
    {
        public static readonly List<WordClass> Nouns = new List<WordClass> { WordClass.Noun };
        public static readonly List<WordClass> Adjectives = new List<WordClass> { WordClass.AdjectiveNa, WordClass.AdjectiveI };
        public static readonly List<WordClass> Verbs = new List<WordClass> { WordClass.VerbRu, WordClass.VerbU };
        public static readonly List<WordClass> AdjectivesOrVerbs = Adjectives.Concat(Verbs).ToList();
        public static readonly List<WordClass> Any = Nouns.Concat(Adjectives).Concat(Verbs).ToList();
    }
}
