//namespace Wacton.Japangolin.Sentences.Domain.Conjugations
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;

//    using ConjugationsUI;
//    using Wacton.Tovarisch.Enum;

//    public class Grammar : Enumeration
//    {
//        public static readonly Func<string, WordClass, string> Dictionary = (text, wordClass) => text;
//        public static readonly Func<string, WordClass, string> Stem = (text, wordClass) => ConjugationFunctions2.GetStem(text, wordClass);
//        public static readonly Func<string, WordClass, string> Te = (text, wordClass) => ConjugationFunctions2.GetTe(text, wordClass);

//        public static readonly Func<string, WordClass, string> PresentAffirmativeLong = (text, wordClass)
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Present, Polarity.Affirmative, Formality.Long);
//        public static readonly Func<string, WordClass, string> PresentAffirmativeShort = (text, wordClass) 
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Present, Polarity.Affirmative, Formality.Short);
//        public static readonly Func<string, WordClass, string> PresentNegativeLong = (text, wordClass)
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Present, Polarity.Negative, Formality.Long);
//        public static readonly Func<string, WordClass, string> PresentNegativeShort = (text, wordClass)
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Present, Polarity.Negative, Formality.Short);
//        public static readonly Func<string, WordClass, string> PastAffirmativeLong = (text, wordClass)
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Past, Polarity.Affirmative, Formality.Long);
//        public static readonly Func<string, WordClass, string> PastAffirmativeShort = (text, wordClass) 
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Past, Polarity.Affirmative, Formality.Short);
//        public static readonly Func<string, WordClass, string> PastNegativeLong = (text, wordClass)
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Past, Polarity.Affirmative, Formality.Long);
//        public static readonly Func<string, WordClass, string> PastNegativeShort = (text, wordClass)
//            => ConjugationFunctions2.Get(text, wordClass, Tense.Past, Polarity.Negative, Formality.Short);

//        // TODO: rename word classes
//        public static readonly List<WordClass> Nouns = new List<WordClass> { WordClass.JapaneseNoun };
//        public static readonly List<WordClass> Adjectives = new List<WordClass> { WordClass.JapaneseAdjectiveNa, WordClass.JapaneseAdjectiveI };
//        public static readonly List<WordClass> Verbs = new List<WordClass> { WordClass.JapaneseVerbIchidan, WordClass.JapaneseVerbGodan };
//        public static readonly List<WordClass> AdjectivesAndVerbs = Adjectives.Concat(Verbs).ToList();
//        public static readonly List<WordClass> All = Nouns.Concat(Adjectives).Concat(Verbs).ToList();

//        public static readonly Grammar None = new Grammar("None", "{0}", (Dictionary, All));

//        public static readonly Grammar ConjugatePresentAffirmativeLong = new Grammar("Conjugate: →＋L", "{0}", (PresentAffirmativeLong, All));
//        public static readonly Grammar ConjugatePresentAffirmativeShort = new Grammar("Conjugate: →＋S", "{0}", (PresentAffirmativeShort, All));
//        public static readonly Grammar ConjugatePresentNegativeLong = new Grammar("Conjugate: →ーL", "{0}", (PresentNegativeLong, All));
//        public static readonly Grammar ConjugatePresentNegativeShort = new Grammar("Conjugate: →ーS", "{0}", (PresentNegativeShort, All));
//        public static readonly Grammar ConjugatePastAffirmativeLong = new Grammar("Conjugate: ←＋L", "{0}", (PastAffirmativeLong, All));
//        public static readonly Grammar ConjugatePastAffirmativeShort = new Grammar("Conjugate: ←＋S", "{0}", (PastAffirmativeShort, All));
//        public static readonly Grammar ConjugatePastNegativeLong = new Grammar("Conjugate: ←ーL", "{0}", (PastNegativeLong, All));
//        public static readonly Grammar ConjugatePastNegativeShort = new Grammar("Conjugate: ←ーS", "{0}", (PastNegativeShort, All));

//        public static readonly Grammar ThereExistsNonLiving = new Grammar("ThereExistsNonLiving", "{0}があります", (Stem, Nouns));
//        public static readonly Grammar ThereExistsLiving = new Grammar("ThereExistsLiving", "{0}がいます", (Stem, Nouns));
//        public static readonly Grammar Invitation = new Grammar("Invitation", "{0}ませんか", (Stem, Verbs));
//        public static readonly Grammar LetsDo = new Grammar("LetsDo", "{0}ましょう", (Stem, Verbs));
//        public static readonly Grammar ShallWe = new Grammar("ShallWe", "{0}ましょうか", (Stem, Verbs));
//        public static readonly Grammar ComparisonTwo = new Grammar("ComparisonTwo", "{0}のほうが{1}より{2}です", (Stem, Nouns), (Stem, Nouns), (Dictionary, Adjectives));
//        public static readonly Grammar ComparisonGroup = new Grammar("ComparisonGroup", "{0}のなかで{1}がいちばん{2}です", (Stem, Nouns), (Stem, Nouns), (Dictionary, Adjectives));
//        public static readonly Grammar FromTo = new Grammar("FromTo", "{0}から{1}まで", (Stem, Nouns), (Stem, Nouns));
//        public static readonly Grammar PurposeOfMovement = new Grammar("PurposeOfMovement", "{0}にいく", (Stem, Verbs));
//        public static readonly Grammar TooMuch = new Grammar("TooMuch", "{0}すぎる", (Stem, AdjectivesAndVerbs));
//        public static readonly Grammar WantToDo = new Grammar("WantToDo", "{0}たいです", (Stem, Verbs));

//        public static readonly Grammar Request = new Grammar("Request", "{0}ください", (Te, Verbs));
//        public static readonly Grammar Permission = new Grammar("Permission", "{0}もいいです", (Te, Verbs));
//        public static readonly Grammar Forbidden = new Grammar("Forbidden", "{0}はいけません", (Te, Verbs));
//        public static readonly Grammar OngoingAction = new Grammar("OngoingAction", "{0}いる", (Te, Verbs));
//        public static readonly Grammar Joining = new Grammar("Joining", "{0}、{1}", (Te, All), (Dictionary, All));
//        public static readonly Grammar HaveNotYet = new Grammar("HaveNotYet", "まだ{0}いません", (Te, Verbs));
//        public static readonly Grammar ImmediatelyAfterDoing = new Grammar("RightAfterDoing", "{0}から", (Te, Verbs));

//        public static readonly Grammar BeforeDoing = new Grammar("BeforeDoing", "{0}まえに", (PresentAffirmativeShort, Verbs)); // ～だ/dict
//        public static readonly Grammar AfterDoing = new Grammar("AfterDoing", "{0}あとで", (PastAffirmativeShort, Verbs)); // ～た
//        public static readonly Grammar PleaseDoNot = new Grammar("PleaseDoNot", "{0}でください", (PresentNegativeShort, Verbs)); // ～ない
//        public static readonly Grammar LikeDoing = new Grammar("LikeDoing", "{0}のがすきです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
//        public static readonly Grammar GoodAtDoing = new Grammar("GoodAtDoing", "{0}のがじょうずです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
//        public static readonly Grammar BadAtDoing = new Grammar("BadAtDoing", "{0}のがへたです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
//        public static readonly Grammar PlanningToDo = new Grammar("PlanningToDo", "{0}つもりです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
//        public static readonly Grammar Because = new Grammar("Because", "{0}から", (PresentAffirmativeShort, All)); // ～だ/dict
//        public static readonly Grammar HaveHadExperience = new Grammar("HaveHadExperience", "{0}ことがいます", (PastAffirmativeShort, Verbs)); // ～た
//        public static readonly Grammar SuchThingsAs = new Grammar("SuchThingsAs", "{0}り{1}りする", (PastAffirmativeShort, All), (PastAffirmativeShort, All)); // ～た

//        // TODO: build on top of existing conjugation functions
//        // sentence patterns from https://www.learn-japanese-adventure.com/japanese-grammar-cause-reason.html
//        // applies to BecausePolite and ExplainImplicit
//        //public static readonly Grammar BecausePolite = new Grammar("Because", "{0}あとで", ShortPastAffirmative); // TODO: need "Short-な form or something...
//        //public static readonly Grammar ExplainImplicit = new Grammar("ExplainImplicit", "{0}あとで", ShortPastAffirmative); // TODO: need "Short-な form or something...
//        //public static readonly Grammar Must = new Grammar("Must", "{0}", ShortPastAffirmative); // short negative -い...
//        //public static readonly Grammar Probably = new Grammar("Probably", "{0}", ShortPastAffirmative); // replaces だ　for noun, adjective-な
//        //public static readonly Grammar BetterToDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
//        //public static readonly Grammar BetterToNotDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
//        //public static readonly Grammar Become = new Grammar("Become", "{0}", ShortPastAffirmative); // ななる　vs. くなる...

//        private readonly string format;
//        private readonly (Func<string, WordClass, string> function, List<WordClass> wordClasses)[] conjugations;

//        public string Details { get; private set; }
//        public int RequiredWordDataCount { get; private set; }

//        public Grammar(string displayName, string format, params (Func<string, WordClass, string> function, List<WordClass> wordClasses)[] conjugations)
//            : base(displayName)
//        {
//            this.format = format;
//            this.conjugations = conjugations;
//            this.RequiredWordDataCount = this.format.Count(character => character.Equals('{')); // naive!

//            this.Details = this.format;
//            for (var i = 0; i < this.RequiredWordDataCount; i++)
//            {
//                var conjugationName = GetConjugationName(conjugations[i].function);
//                this.Details = this.Details.Replace("{" + i + "}", conjugationName);
//            }

//            if (this.conjugations.Length != this.RequiredWordDataCount)
//            {
//                throw new InvalidOperationException();
//            }
//        }

//        public string Conjugate(params WordData[] wordDatas)
//        {
//            if (wordDatas.Length != this.RequiredWordDataCount)
//            {
//                throw new InvalidOperationException();
//            }

//            var conjugatedWords = wordDatas
//                .Select((wordData, i) => this.conjugations[i].function(wordData.Text, wordData.Class))
//                .ToArray();

//            return string.Format(this.format, conjugatedWords);
//        }

//        public List<List<WordClass>> GetRequiredWordClasses()
//        {
//            return this.conjugations.Select(conjugation => conjugation.wordClasses).ToList();
//        }

//        private static string GetConjugationName(Func<string, WordClass, string> conjugationFunction)
//        {
//            if (conjugationFunction == Dictionary) { return "｛dict｝"; }
//            if (conjugationFunction == Stem) { return "｛stem｝"; }
//            if (conjugationFunction == Te) { return "｛～て｝"; }
//            if (conjugationFunction == PresentAffirmativeShort) { return "｛→＋S　～だ｝"; }
//            if (conjugationFunction == PastAffirmativeShort) { return "｛←＋S　～た｝"; }
//            if (conjugationFunction == PresentNegativeShort) { return "｛→－S　～ない｝"; }
//            throw new InvalidOperationException();
//        }

//    }
//}
