namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ConjugationsUI;
    using Wacton.Tovarisch.Enum;

    public class Grammar : Enumeration
    {
        public static readonly Func<WordClass, ConjFunc> Dictionary = wordClass => ConjFuncs.GetDictFunc(wordClass);
        public static readonly Func<WordClass, ConjFunc> Stem = wordClass => ConjFuncs.GetStemFunc(wordClass);
        public static readonly Func<WordClass, ConjFunc> Te = wordClass => ConjFuncs.GetTeFunc(wordClass);

        public static readonly Func<WordClass, ConjFunc> PresentAffirmativeLong = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Present, Polarity.Affirmative, Formality.Long);
        public static readonly Func<WordClass, ConjFunc> PresentAffirmativeShort = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Present, Polarity.Affirmative, Formality.Short);
        public static readonly Func<WordClass, ConjFunc> PresentNegativeLong = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Present, Polarity.Negative, Formality.Long);
        public static readonly Func<WordClass, ConjFunc> PresentNegativeShort = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Present, Polarity.Negative, Formality.Short);
        public static readonly Func<WordClass, ConjFunc> PastAffirmativeLong = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Past, Polarity.Affirmative, Formality.Long);
        public static readonly Func<WordClass, ConjFunc> PastAffirmativeShort = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Past, Polarity.Affirmative, Formality.Short);
        public static readonly Func<WordClass, ConjFunc> PastNegativeLong = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Past, Polarity.Affirmative, Formality.Long);
        public static readonly Func<WordClass, ConjFunc> PastNegativeShort = wordClass
            => ConjFuncs.GetConjFunc(wordClass, Tense.Past, Polarity.Negative, Formality.Short);

        // TODO: rename word classes
        public static readonly List<WordClass> Nouns = new List<WordClass> { WordClass.JapaneseNoun };
        public static readonly List<WordClass> Adjectives = new List<WordClass> { WordClass.JapaneseAdjectiveNa, WordClass.JapaneseAdjectiveI };
        public static readonly List<WordClass> Verbs = new List<WordClass> { WordClass.JapaneseVerbIchidan, WordClass.JapaneseVerbGodan };
        public static readonly List<WordClass> AdjectivesAndVerbs = Adjectives.Concat(Verbs).ToList();
        public static readonly List<WordClass> All = Nouns.Concat(Adjectives).Concat(Verbs).ToList();

        public static readonly Grammar FormDictionary = new Grammar("Form: dictionary", "{0}", (Dictionary, All));
        public static readonly Grammar FormStem = new Grammar("Form: stem", "{0}", (Stem, All));
        public static readonly Grammar FormTe = new Grammar("Form: ～て", "{0}", (Te, All));
        public static readonly Grammar ConjugatePresentAffirmativeLong = new Grammar("Conjugate: →＋L", "{0}", (PresentAffirmativeLong, All));
        public static readonly Grammar ConjugatePresentAffirmativeShort = new Grammar("Conjugate: →＋S", "{0}", (PresentAffirmativeShort, All));
        public static readonly Grammar ConjugatePresentNegativeLong = new Grammar("Conjugate: →ーL", "{0}", (PresentNegativeLong, All));
        public static readonly Grammar ConjugatePresentNegativeShort = new Grammar("Conjugate: →ーS", "{0}", (PresentNegativeShort, All));
        public static readonly Grammar ConjugatePastAffirmativeLong = new Grammar("Conjugate: ←＋L", "{0}", (PastAffirmativeLong, All));
        public static readonly Grammar ConjugatePastAffirmativeShort = new Grammar("Conjugate: ←＋S", "{0}", (PastAffirmativeShort, All));
        public static readonly Grammar ConjugatePastNegativeLong = new Grammar("Conjugate: ←ーL", "{0}", (PastNegativeLong, All));
        public static readonly Grammar ConjugatePastNegativeShort = new Grammar("Conjugate: ←ーS", "{0}", (PastNegativeShort, All));

        public static readonly Grammar ThereExistsNonLiving = new Grammar("ThereExistsNonLiving", "{0}があります", (Stem, Nouns));
        public static readonly Grammar ThereExistsLiving = new Grammar("ThereExistsLiving", "{0}がいます", (Stem, Nouns));
        public static readonly Grammar Invitation = new Grammar("Invitation", "{0}ませんか", (Stem, Verbs));
        public static readonly Grammar LetsDo = new Grammar("LetsDo", "{0}ましょう", (Stem, Verbs));
        public static readonly Grammar ShallWe = new Grammar("ShallWe", "{0}ましょうか", (Stem, Verbs));
        public static readonly Grammar ComparisonTwo = new Grammar("ComparisonTwo", "{0}のほうが{1}より{2}です", (Stem, Nouns), (Stem, Nouns), (Dictionary, Adjectives));
        public static readonly Grammar ComparisonGroup = new Grammar("ComparisonGroup", "{0}のなかで{1}がいちばん{2}です", (Stem, Nouns), (Stem, Nouns), (Dictionary, Adjectives));
        public static readonly Grammar FromTo = new Grammar("FromTo", "{0}から{1}まで", (Stem, Nouns), (Stem, Nouns));
        public static readonly Grammar PurposeOfMovement = new Grammar("PurposeOfMovement", "{0}にいく", (Stem, Verbs));
        public static readonly Grammar TooMuch = new Grammar("TooMuch", "{0}すぎる", (Stem, AdjectivesAndVerbs));
        public static readonly Grammar WantToDo = new Grammar("WantToDo", "{0}たいです", (Stem, Verbs));

        public static readonly Grammar Request = new Grammar("Request", "{0}ください", (Te, Verbs));
        public static readonly Grammar Permission = new Grammar("Permission", "{0}もいいです", (Te, Verbs));
        public static readonly Grammar Forbidden = new Grammar("Forbidden", "{0}はいけません", (Te, Verbs));
        public static readonly Grammar OngoingAction = new Grammar("OngoingAction", "{0}いる", (Te, Verbs));
        public static readonly Grammar Joining = new Grammar("Joining", "{0}、{1}", (Te, All), (Dictionary, All));
        public static readonly Grammar HaveNotYet = new Grammar("HaveNotYet", "まだ{0}いません", (Te, Verbs));
        public static readonly Grammar ImmediatelyAfterDoing = new Grammar("RightAfterDoing", "{0}から", (Te, Verbs));

        public static readonly Grammar BeforeDoing = new Grammar("BeforeDoing", "{0}まえに", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly Grammar AfterDoing = new Grammar("AfterDoing", "{0}あとで", (PastAffirmativeShort, Verbs)); // ～た
        public static readonly Grammar PleaseDoNot = new Grammar("PleaseDoNot", "{0}でください", (PresentNegativeShort, Verbs)); // ～ない
        public static readonly Grammar LikeDoing = new Grammar("LikeDoing", "{0}のがすきです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly Grammar GoodAtDoing = new Grammar("GoodAtDoing", "{0}のがじょうずです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly Grammar BadAtDoing = new Grammar("BadAtDoing", "{0}のがへたです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly Grammar PlanningToDo = new Grammar("PlanningToDo", "{0}つもりです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly Grammar Because = new Grammar("Because", "{0}から", (PresentAffirmativeShort, All)); // ～だ/dict
        public static readonly Grammar HaveHadExperience = new Grammar("HaveHadExperience", "{0}ことがいます", (PastAffirmativeShort, Verbs)); // ～た
        public static readonly Grammar SuchThingsAs = new Grammar("SuchThingsAs", "{0}り{1}りする", (PastAffirmativeShort, All), (PastAffirmativeShort, All)); // ～た

        // TODO: build on top of existing conjugation functions
        // sentence patterns from https://www.learn-japanese-adventure.com/japanese-grammar-cause-reason.html
        // applies to BecausePolite and ExplainImplicit
        //public static readonly Grammar BecausePolite = new Grammar("Because", "{0}あとで", ShortPastAffirmative); // TODO: need "Short-な form or something...
        //public static readonly Grammar ExplainImplicit = new Grammar("ExplainImplicit", "{0}あとで", ShortPastAffirmative); // TODO: need "Short-な form or something...
        //public static readonly Grammar Must = new Grammar("Must", "{0}", ShortPastAffirmative); // short negative -い...
        //public static readonly Grammar Probably = new Grammar("Probably", "{0}", ShortPastAffirmative); // replaces だ　for noun, adjective-な
        //public static readonly Grammar BetterToDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar BetterToNotDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar Become = new Grammar("Become", "{0}", ShortPastAffirmative); // ななる　vs. くなる...

        private readonly string format;
        private readonly (Func<WordClass, ConjFunc> functionByWordClass, List<WordClass> wordClasses)[] conjugations;

        public int RequiredWordDataCount { get; private set; }
        public bool IsHighLevel => !this.DisplayName.StartsWith("Form:") && !this.DisplayName.StartsWith("Conjugate:"); // ugh...

        public Grammar(string displayName, string format, params (Func<WordClass, ConjFunc> functionByWordClass, List<WordClass> wordClasses)[] conjugations)
            : base(displayName)
        {
            this.format = format;
            this.conjugations = conjugations;
            this.RequiredWordDataCount = this.format.Count(character => character.Equals('{')); // naive!

            if (this.conjugations.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }
        }

        public string Conjugate(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var conjugatedWords = wordDatas
                .Select((wordData, i) => this.conjugations[i].functionByWordClass(wordData.Class).Conjugate(wordData.Kana))
                .ToArray();

            return string.Format(this.format, conjugatedWords);
        }

        public string Information(params WordData[] wordDatas)
        {
            if (wordDatas.Length != this.RequiredWordDataCount)
            {
                throw new InvalidOperationException();
            }

            var info = this.format;
            for (var i = 0; i < this.RequiredWordDataCount; i++)
            {
                var functionByWordClass = conjugations[i].functionByWordClass;

                // note: showing high-level information when grammar is more than simple conjugation
                var information = this.IsHighLevel
                    ? GetGeneralConjugationInformation(functionByWordClass)
                    : GetDetailedConjugationInformation(functionByWordClass, wordDatas[i].Class);

                info = info.Replace("{" + i + "}", information);
            }

            return info;
        }

        public List<List<WordClass>> GetRequiredWordClasses()
        {
            return this.conjugations.Select(conjugation => conjugation.wordClasses).ToList();
        }

        private static string GetDetailedConjugationInformation(Func<WordClass, ConjFunc> functionByWordClass, WordClass wordClass)
        {
            return functionByWordClass(wordClass).Information;
        }

        private static string GetGeneralConjugationInformation(Func<WordClass, ConjFunc> functionByWordClass)
        {
            if (functionByWordClass == Dictionary) { return "｛dict｝"; }
            if (functionByWordClass == Stem) { return "｛stem｝"; }
            if (functionByWordClass == Te) { return "｛～て｝"; }
            if (functionByWordClass == PresentAffirmativeShort) { return "｛→＋S　～だ｝"; }
            if (functionByWordClass == PastAffirmativeShort) { return "｛←＋S　～た｝"; }
            if (functionByWordClass == PresentNegativeShort) { return "｛→－S　～ない｝"; }
            throw new InvalidOperationException();
        }

    }
}
