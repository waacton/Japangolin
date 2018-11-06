namespace Wacton.Japangolin.Sentences.Domain.Conjugations
{
    using System;
    using System.Linq;

    using ConjugationsUI;
    using Wacton.Tovarisch.Enum;

    public class Grammar : Enumeration
    {
        // TODO: how to enforce certain word classes?!

        public static readonly Func<string, WordClass, string> Dictionary = (text, wordClass) => text;
        public static readonly Func<string, WordClass, string> Stem = (text, wordClass) => ConjugationFunctions2.GetStem(text, wordClass);
        public static readonly Func<string, WordClass, string> Te = (text, wordClass) => ConjugationFunctions2.GetTe(text, wordClass);

        public static readonly Func<string, WordClass, string> ShortPresentAffirmative = (text, wordClass) => ConjugationFunctions2.Get(text, wordClass, Tense.Present, Polarity.Affirmative, Formality.Short);
        public static readonly Func<string, WordClass, string> ShortPastAffirmative = (text, wordClass) => ConjugationFunctions2.Get(text, wordClass, Tense.Past, Polarity.Affirmative, Formality.Short);
        public static readonly Func<string, WordClass, string> ShortPresentNegative = (text, wordClass) => ConjugationFunctions2.Get(text, wordClass, Tense.Present, Polarity.Negative, Formality.Short);

        public static readonly Grammar ThereExistsNonLiving = new Grammar("ThereExistsNonLiving", "{0}があります", Stem);
        public static readonly Grammar ThereExistsLiving = new Grammar("ThereExistsLiving", "{0}がいます", Stem);
        public static readonly Grammar Invitation = new Grammar("Invitation", "{0}ませんか", Stem);
        public static readonly Grammar LetsDo = new Grammar("LetsDo", "{0}ましょう", Stem);
        public static readonly Grammar ShallWe = new Grammar("ShallWe", "{0}ましょうか", Stem);
        public static readonly Grammar ComparisonTwo = new Grammar("ComparisonTwo", "{0}のほうが{1}より{2}です", Stem, Stem, Dictionary);
        public static readonly Grammar ComparisonMany = new Grammar("ComparisonMany", "{0}のなかで{1}がいちばん{2}です", Stem, Stem, Dictionary);
        public static readonly Grammar FromTo = new Grammar("FromTo", "{0}から{1}まで", Stem, Stem);
        public static readonly Grammar PurposeOfMovement = new Grammar("PurposeOfMovement", "{0}にいく", Stem);
        public static readonly Grammar TooMuch = new Grammar("TooMuch", "{0}すきる", Stem);
        public static readonly Grammar WantToDo = new Grammar("WantToDo", "{0}たいです", Stem);

        public static readonly Grammar Request = new Grammar("Request", "{0}ください", Te);
        public static readonly Grammar Permission = new Grammar("Permission", "{0}もいいです", Te);
        public static readonly Grammar Forbidden = new Grammar("Forbidden", "{0}はいけません", Te);
        public static readonly Grammar OngoingAction = new Grammar("OngoingAction", "{0}いる", Te);
        public static readonly Grammar Joining = new Grammar("Joining", "{0}、{1}", Te, Dictionary);
        public static readonly Grammar HaveNotYet = new Grammar("HaveNotYet", "まだ{0}いません", Te);
        public static readonly Grammar ImmediatelyAfterDoing = new Grammar("RightAfterDoing", "{0}から", Te);

        public static readonly Grammar BeforeDoing = new Grammar("BeforeDoing", "{0}まえに", ShortPresentAffirmative); // ～だ/dict
        public static readonly Grammar AfterDoing = new Grammar("AfterDoing", "{0}あとで", ShortPastAffirmative); // ～た
        public static readonly Grammar PleaseDoNot = new Grammar("PleaseDoNot", "{0}でください", ShortPresentNegative); // ～ない
        public static readonly Grammar LikeDoing = new Grammar("LikeDoing", "{0}のがすきです", ShortPresentAffirmative); // ～だ/dict
        public static readonly Grammar GoodAtDoing = new Grammar("GoodAtDoing", "{0}のがじょうずです", ShortPresentAffirmative); // ～だ/dict
        public static readonly Grammar BadAtDoing = new Grammar("BadAtDoing", "{0}のがへたです", ShortPresentAffirmative); // ～だ/dict
        public static readonly Grammar PlanningToDo = new Grammar("PlanningToDo", "{0}つもりです", ShortPresentAffirmative); // ～だ/dict
        public static readonly Grammar Because = new Grammar("Because", "{0}から", ShortPresentAffirmative); // ～だ/dict
        public static readonly Grammar HaveHadExperience = new Grammar("HaveHadExperience", "{0}ことがいます", ShortPastAffirmative); // ～た
        public static readonly Grammar SuchThingsAs = new Grammar("SuchThingsAs", "{0}り{1}りする", ShortPastAffirmative, ShortPastAffirmative); // ～た

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
        private readonly Func<string, WordClass, string>[] conjugationFunctions;

        public string Details { get; private set; }
        public int RequiredWordDataCount { get; private set; }

        public Grammar(string displayName, string format, params Func<string, WordClass, string>[] conjugationFunctions) : base(displayName)
        {
            this.format = format;
            this.conjugationFunctions = conjugationFunctions;
            this.RequiredWordDataCount = this.format.Count(character => character.Equals('{')); // naive!

            this.Details = this.format;
            for (var i = 0; i < this.RequiredWordDataCount; i++)
            {
                var conjugationName = GetConjugationName(conjugationFunctions[i]);
                this.Details = this.Details.Replace("{" + i + "}", conjugationName);
            }

            if (this.conjugationFunctions.Length != this.RequiredWordDataCount)
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
                .Select((wordData, i) => this.conjugationFunctions[i](wordData.Text, wordData.Class))
                .ToArray();

            return string.Format(this.format, conjugatedWords);
        }

        private static string GetConjugationName(Func<string, WordClass, string> conjugationFunction)
        {
            if (conjugationFunction == Dictionary) { return "｛dict｝"; }
            if (conjugationFunction == Stem) { return "｛stem｝"; }
            if (conjugationFunction == Te) { return "｛～て｝"; }
            if (conjugationFunction == ShortPresentAffirmative) { return "｛short→＋　～だ｝"; }
            if (conjugationFunction == ShortPastAffirmative) { return "｛short←＋　～た｝"; }
            if (conjugationFunction == ShortPresentNegative) { return "｛short→－　～ない｝"; }
            throw new InvalidOperationException();
        }

    }
}
