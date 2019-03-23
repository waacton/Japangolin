namespace Wacton.Japangolin.Conjugation
{
    using System;
    using System.Collections.Generic;

    // TODO: needs some more love - figure out a good way to handle all this complexity
    public class ModifierGrammar : ModifierBase
    {
        public override string Variation => "Grammar";
        public override bool IsHighLevel => true;

        public static readonly ModifierGrammar ThereExistsNonLiving = new ModifierGrammar("ThereExistsNonLiving", "{0}があります", (Stem, Nouns));
        public static readonly ModifierGrammar ThereExistsLiving = new ModifierGrammar("ThereExistsLiving", "{0}がいます", (Stem, Nouns));
        public static readonly ModifierGrammar Invitation = new ModifierGrammar("Invitation", "{0}ませんか", (Stem, Verbs));
        public static readonly ModifierGrammar LetsDo = new ModifierGrammar("LetsDo", "{0}ましょう", (Stem, Verbs));
        public static readonly ModifierGrammar ShallWe = new ModifierGrammar("ShallWe", "{0}ましょうか", (Stem, Verbs));
        public static readonly ModifierGrammar ComparisonTwo = new ModifierGrammar("ComparisonTwo", "{0}のほうが{1}より{2}です", (Stem, Nouns), (Stem, Nouns), (Dictionary, Adjectives));
        public static readonly ModifierGrammar ComparisonGroup = new ModifierGrammar("ComparisonGroup", "{0}のなかで{1}がいちばん{2}です", (Stem, Nouns), (Stem, Nouns), (Dictionary, Adjectives));
        public static readonly ModifierGrammar FromTo = new ModifierGrammar("FromTo", "{0}から{1}まで", (Stem, Nouns), (Stem, Nouns));
        public static readonly ModifierGrammar PurposeOfMovement = new ModifierGrammar("PurposeOfMovement", "{0}にいく", (Stem, Verbs));
        public static readonly ModifierGrammar TooMuch = new ModifierGrammar("TooMuch", "{0}すぎる", (Stem, AdjectivesAndVerbs));
        public static readonly ModifierGrammar WantToDo = new ModifierGrammar("WantToDo", "{0}たいです", (Stem, Verbs));

        public static readonly ModifierGrammar Request = new ModifierGrammar("Request", "{0}ください", (Te, Verbs));
        public static readonly ModifierGrammar Permission = new ModifierGrammar("Permission", "{0}もいいです", (Te, Verbs));
        public static readonly ModifierGrammar Forbidden = new ModifierGrammar("Forbidden", "{0}はいけません", (Te, Verbs));
        public static readonly ModifierGrammar OngoingAction = new ModifierGrammar("OngoingAction", "{0}いる", (Te, Verbs));
        public static readonly ModifierGrammar Joining = new ModifierGrammar("Joining", "{0}、{1}", (Te, All), (Dictionary, All));
        public static readonly ModifierGrammar HaveNotYet = new ModifierGrammar("HaveNotYet", "まだ{0}いません", (Te, Verbs));
        public static readonly ModifierGrammar ImmediatelyAfterDoing = new ModifierGrammar("RightAfterDoing", "{0}から", (Te, Verbs));

        public static readonly ModifierGrammar BeforeDoing = new ModifierGrammar("BeforeDoing", "{0}まえに", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly ModifierGrammar AfterDoing = new ModifierGrammar("AfterDoing", "{0}あとで", (PastAffirmativeShort, Verbs)); // ～た
        public static readonly ModifierGrammar PleaseDoNot = new ModifierGrammar("PleaseDoNot", "{0}でください", (PresentNegativeShort, Verbs)); // ～ない
        public static readonly ModifierGrammar LikeDoing = new ModifierGrammar("LikeDoing", "{0}のがすきです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly ModifierGrammar GoodAtDoing = new ModifierGrammar("GoodAtDoing", "{0}のがじょうずです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly ModifierGrammar BadAtDoing = new ModifierGrammar("BadAtDoing", "{0}のがへたです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly ModifierGrammar PlanningToDo = new ModifierGrammar("PlanningToDo", "{0}つもりです", (PresentAffirmativeShort, Verbs)); // ～だ/dict
        public static readonly ModifierGrammar Because = new ModifierGrammar("Because", "{0}から", (PresentAffirmativeShort, All)); // ～だ/dict
        public static readonly ModifierGrammar HaveHadExperience = new ModifierGrammar("HaveHadExperience", "{0}ことがいます", (PastAffirmativeShort, Verbs)); // ～た
        public static readonly ModifierGrammar SuchThingsAs = new ModifierGrammar("SuchThingsAs", "{0}り{1}りする", (PastAffirmativeShort, All), (PastAffirmativeShort, All)); // ～た

        // TODO: build on top of existing conjugation functions
        // sentence patterns from https://www.learn-japanese-adventure.com/japanese-grammar-cause-reason.html
        // applies to BecausePolite and ExplainImplicit

        public static readonly ModifierGrammar BecausePolite = new ModifierGrammar("BecausePolite", "{0}ので", (Na, All)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/node-genki-i-chapter-12/
        public static readonly ModifierGrammar ExplainImplicit = new ModifierGrammar("ExplainImplicit", "{0}んです", (Na, All)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/n-desu-genki-i-chapter-12/

        //public static readonly Grammar Must = new Grammar("Must", "{0}", ShortPastAffirmative); // short negative -い...
        //public static readonly Grammar Probably = new Grammar("Probably", "{0}", ShortPastAffirmative); // replaces だ　for noun, adjective-な
        //public static readonly Grammar BetterToDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar BetterToNotDo = new Grammar("BetterToNotDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar Become = new Grammar("Become", "{0}", ShortPastAffirmative); // ななる　vs. くなる...

        public ModifierGrammar(string displayName, string format, params (Func<WordClass, Conjugator> conjugatorByWordClass, List<WordClass> wordClasses)[] conjugations)
            : base(displayName, format, conjugations)
        {
        }
    }
}
