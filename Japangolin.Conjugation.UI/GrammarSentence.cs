namespace Wacton.Japangolin.Conjugation
{
    public class GrammarSentence : GrammarBase
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
        public static readonly GrammarSentence ThereExistsNonLiving = new GrammarSentence("ThereExistsNonLiving", "{0}があります", GetConj(WordClasses.Nouns, Stem));
        public static readonly GrammarSentence ThereExistsLiving = new GrammarSentence("ThereExistsLiving", "{0}がいます", GetConj(WordClasses.Nouns, Stem));
        public static readonly GrammarSentence Invitation = new GrammarSentence("Invitation", "{0}ませんか", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarSentence LetsDo = new GrammarSentence("LetsDo", "{0}ましょう", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarSentence ShallWe = new GrammarSentence("ShallWe", "{0}ましょうか", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarSentence ComparisonTwo = new GrammarSentence("ComparisonTwo", "{0}のほうが{1}より{2}です", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Adjectives, Dictionary));
        public static readonly GrammarSentence ComparisonGroup = new GrammarSentence("ComparisonGroup", "{0}のなかで{1}がいちばん{2}です", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Adjectives, Dictionary));
        public static readonly GrammarSentence FromTo = new GrammarSentence("FromTo", "{0}から{1}まで", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem));
        public static readonly GrammarSentence PurposeOfMovement = new GrammarSentence("PurposeOfMovement", "{0}にいく", GetConj(WordClasses.Verbs, Stem));
        public static readonly GrammarSentence TooMuch = new GrammarSentence("TooMuch", "{0}すぎる", GetConj(WordClasses.AdjectivesOrVerbs, Stem));
        public static readonly GrammarSentence WantToDo = new GrammarSentence("WantToDo", "{0}たいです", GetConj(WordClasses.Verbs, Stem));

        public static readonly GrammarSentence Request = new GrammarSentence("Request", "{0}ください", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarSentence Permission = new GrammarSentence("Permission", "{0}もいいです", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarSentence Forbidden = new GrammarSentence("Forbidden", "{0}はいけません", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarSentence OngoingAction = new GrammarSentence("OngoingAction", "{0}いる", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarSentence Joining = new GrammarSentence("Joining", "{0}、{1}", GetConj(WordClasses.Any, Te), GetConj(WordClasses.Any, Dictionary));
        public static readonly GrammarSentence HaveNotYet = new GrammarSentence("HaveNotYet", "まだ{0}いません", GetConj(WordClasses.Verbs, Te));
        public static readonly GrammarSentence ImmediatelyAfterDoing = new GrammarSentence("RightAfterDoing", "{0}から", GetConj(WordClasses.Verbs, Te));

        public static readonly GrammarSentence BeforeDoing = new GrammarSentence("BeforeDoing", "{0}まえに", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarSentence AfterDoing = new GrammarSentence("AfterDoing", "{0}あとで", GetConj(WordClasses.Verbs, PastAffirmativeShort)); // ～た
        public static readonly GrammarSentence PleaseDoNot = new GrammarSentence("PleaseDoNot", "{0}でください", GetConj(WordClasses.Verbs, PresentNegativeShort)); // ～ない
        public static readonly GrammarSentence LikeDoing = new GrammarSentence("LikeDoing", "{0}のがすきです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarSentence GoodAtDoing = new GrammarSentence("GoodAtDoing", "{0}のがじょうずです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarSentence BadAtDoing = new GrammarSentence("BadAtDoing", "{0}のがへたです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarSentence PlanningToDo = new GrammarSentence("PlanningToDo", "{0}つもりです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarSentence Because = new GrammarSentence("Because", "{0}から", GetConj(WordClasses.Any, PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarSentence HaveHadExperience = new GrammarSentence("HaveHadExperience", "{0}ことがいます", GetConj(WordClasses.Verbs, PastAffirmativeShort)); // ～た
        public static readonly GrammarSentence SuchThingsAs = new GrammarSentence("SuchThingsAs", "{0}り{1}りする", GetConj(WordClasses.Any, PastAffirmativeShort), GetConj(WordClasses.Any, PastAffirmativeShort)); // ～た

        // TODO: build on top of existing conjugation functions
        // sentence patterns from https://www.learn-japanese-adventure.com/japanese-grammar-cause-reason.html
        // applies to BecausePolite and ExplainImplicit

        public static readonly GrammarSentence BecausePolite = new GrammarSentence("BecausePolite", "{0}ので", GetConj(WordClasses.Any, Na)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/node-genki-i-chapter-12/
        public static readonly GrammarSentence ExplainImplicit = new GrammarSentence("ExplainImplicit", "{0}んです", GetConj(WordClasses.Any, Na)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/n-desu-genki-i-chapter-12/

        //public static readonly Grammar Must = new Grammar("Must", "{0}", ShortPastAffirmative); // short negative -い...
        //public static readonly Grammar Probably = new Grammar("Probably", "{0}", ShortPastAffirmative); // replaces だ　for noun, adjective-な
        //public static readonly Grammar BetterToDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar BetterToNotDo = new Grammar("BetterToNotDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar Become = new Grammar("Become", "{0}", ShortPastAffirmative); // ななる　vs. くなる...

        public GrammarSentence(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
