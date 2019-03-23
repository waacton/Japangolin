namespace Wacton.Japangolin.Conjugation
{
    public class GrammarPhrase : GrammarBase
    {
        public override string Variation => "Grammar";
        public override bool IsHighLevel => true;

        /* 
         * NOTE: in order to use a different conjugation with an existing grammar
         * (e.g. ～んです / ExplainImplicit uses past affirmative)
         * simply create another grammar enum with the other conjugation (e.g. ExplainImplicitPresent)
         * ---
         * it is possible to provide a list of possible conjugators for each word in the grammar
         * but adds another layer of complexity, which just isn't worth it
         */
        public static readonly GrammarPhrase ThereExistsNonLiving = new GrammarPhrase("ThereExistsNonLiving", "{0}があります", GetConj(WordClasses.Nouns, Stem));
        public static readonly GrammarPhrase ThereExistsLiving = new GrammarPhrase("ThereExistsLiving", "{0}がいます", GetConj(WordClasses.Nouns, Stem));
        public static readonly GrammarPhrase Invitation = new GrammarPhrase("Invitation", "{0}ませんか", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarPhrase LetsDo = new GrammarPhrase("LetsDo", "{0}ましょう", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarPhrase ShallWe = new GrammarPhrase("ShallWe", "{0}ましょうか", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarPhrase ComparisonTwo = new GrammarPhrase("ComparisonTwo", "{0}のほうが{1}より{2}です", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Adjectives, Dictionary));
        public static readonly GrammarPhrase ComparisonGroup = new GrammarPhrase("ComparisonGroup", "{0}のなかで{1}がいちばん{2}です", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Adjectives, Dictionary));
        public static readonly GrammarPhrase FromTo = new GrammarPhrase("FromTo", "{0}から{1}まで", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem));
        public static readonly GrammarPhrase PurposeOfMovement = new GrammarPhrase("PurposeOfMovement", "{0}にいく", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarPhrase TooMuch = new GrammarPhrase("TooMuch", "{0}すぎる", GetConj(WordClasses.AdjectivesOrVerbs, Stem));
        public static readonly GrammarPhrase WantToDo = new GrammarPhrase("WantToDo", "{0}たいです", GetConj(WordClasses.Verbs, Stem));

        public static readonly GrammarPhrase Request = new GrammarPhrase("Request", "{0}ください", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarPhrase Permission = new GrammarPhrase("Permission", "{0}もいいです", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarPhrase Forbidden = new GrammarPhrase("Forbidden", "{0}はいけません", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarPhrase OngoingAction = new GrammarPhrase("OngoingAction", "{0}いる", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarPhrase Joining = new GrammarPhrase("Joining", "{0}、{1}", GetConj(WordClasses.Any, Te), GetConj(WordClasses.Any, Dictionary));
        public static readonly GrammarPhrase HaveNotYet = new GrammarPhrase("HaveNotYet", "まだ{0}いません", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarPhrase ImmediatelyAfterDoing = new GrammarPhrase("RightAfterDoing", "{0}から", GetConj(WordClasses.Verbs, Te));

        public static readonly GrammarPhrase BeforeDoing = new GrammarPhrase("BeforeDoing", "{0}まえに", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarPhrase AfterDoing = new GrammarPhrase("AfterDoing", "{0}あとで", GetConj(WordClasses.Verbs, PastAffirmativeShort)); // ～た
        public static readonly GrammarPhrase PleaseDoNot = new GrammarPhrase("PleaseDoNot", "{0}でください", GetConj(WordClasses.Verbs, PresentNegativeShort)); // ～ない
        public static readonly GrammarPhrase LikeDoing = new GrammarPhrase("LikeDoing", "{0}のがすきです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarPhrase GoodAtDoing = new GrammarPhrase("GoodAtDoing", "{0}のがじょうずです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarPhrase BadAtDoing = new GrammarPhrase("BadAtDoing", "{0}のがへたです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarPhrase PlanningToDo = new GrammarPhrase("PlanningToDo", "{0}つもりです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarPhrase Because = new GrammarPhrase("Because", "{0}から", GetConj(WordClasses.Any, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarPhrase HaveHadExperience = new GrammarPhrase("HaveHadExperience", "{0}ことがいます", GetConj(WordClasses.Verbs, PastAffirmativeShort)); // ～た
        public static readonly GrammarPhrase SuchThingsAs = new GrammarPhrase("SuchThingsAs", "{0}り{1}りする", GetConj(WordClasses.Any, PastAffirmativeShort), GetConj(WordClasses.Any, PastAffirmativeShort)); // ～た

        // TODO: build on top of existing conjugation functions
        // sentence patterns from https://www.learn-japanese-adventure.com/japanese-grammar-cause-reason.html
        // applies to BecausePolite and ExplainImplicit

        public static readonly GrammarPhrase BecausePolite = new GrammarPhrase("BecausePolite", "{0}ので", GetConj(WordClasses.Any, Na)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/node-genki-i-chapter-12/
        public static readonly GrammarPhrase ExplainImplicit = new GrammarPhrase("ExplainImplicit", "{0}んです", GetConj(WordClasses.Any, Na)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/n-desu-genki-i-chapter-12/

        //public static readonly Grammar Must = new Grammar("Must", "{0}", ShortPastAffirmative); // short negative -い...
        //public static readonly Grammar Probably = new Grammar("Probably", "{0}", ShortPastAffirmative); // replaces だ　for noun, adjective-な
        //public static readonly Grammar BetterToDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar BetterToNotDo = new Grammar("BetterToNotDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar Become = new Grammar("Become", "{0}", ShortPastAffirmative); // ななる　vs. くなる...

        public GrammarPhrase(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
