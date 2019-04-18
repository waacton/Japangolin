namespace Wacton.Japangolin.Conjugation
{
    public class GrammarPhrase : GrammarBase
    {
        public override string Variation => "Phrase";
        public override bool IsHighLevel => true;

        /* 
         * NOTE: in order to use a different conjugation with an existing grammar
         * (e.g. ～んです / ExplainImplicit uses past affirmative)
         * simply create another grammar enum with the other conjugation (e.g. ExplainImplicitPresent)
         * ---
         * it is possible to provide a list of possible conjugators for each word in the grammar
         * but adds another layer of complexity, which just isn't worth it
         * ---
         * trivial examples (note that there is no short-form version of these):
         * GrammarPhrase ItIs = new GrammarPhrase("ItIs", "{0}", WordClasses.NounsOrAdjectives.Link(ConjugatorFuncs.PresentAffirmativeLong));
         * GrammarPhrase ItWas = new GrammarPhrase("ItWas", "{0}", WordClasses.NounsOrAdjectives.Link(ConjugatorFuncs.PastAffirmativeLong));
         * GrammarPhrase ItIsNot = new GrammarPhrase("ItIsNot", "{0}", WordClasses.NounsOrAdjectives.Link(ConjugatorFuncs.PresentNegativeLong));
         * GrammarPhrase ItWasNot = new GrammarPhrase("ItWasNot", "{0}", WordClasses.NounsOrAdjectives.Link(ConjugatorFuncs.PastNegativeLong));
         * GrammarPhrase Do = new GrammarPhrase("Do", "{0}", WordClasses.Verbs.Link(ConjugatorFuncs.PresentAffirmativeLong));
         * GrammarPhrase Did = new GrammarPhrase("Did", "{0}", WordClasses.Verbs.Link(ConjugatorFuncs.PastAffirmativeLong));
         * GrammarPhrase DoNot = new GrammarPhrase("DoNot", "{0}", WordClasses.Verbs.Link(ConjugatorFuncs.PresentNegativeLong));
         * GrammarPhrase DidNot = new GrammarPhrase("ItWasNot", "{0}", WordClasses.Verbs.Link(ConjugatorFuncs.PastNegativeLong));
         */

        public static readonly GrammarPhrase ThereExistsNonLiving = new GrammarPhrase("ThereExistsNonLiving", "{0}があります", WordClasses.Nouns.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase ThereExistsLiving = new GrammarPhrase("ThereExistsLiving", "{0}がいます", WordClasses.Nouns.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase Invitation = new GrammarPhrase("Invitation", "{0}ませんか", WordClasses.Verbs.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase LetsDo = new GrammarPhrase("LetsDo", "{0}ましょう", WordClasses.Verbs.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase ShallWe = new GrammarPhrase("ShallWe", "{0}ましょうか", WordClasses.Verbs.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase OfferAssistance = new GrammarPhrase("OfferAssistance", "{0}ましょうか", WordClasses.Verbs.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase ComparisonTwo = new GrammarPhrase("ComparisonTwo", "{0}のほうが{1}より{2}です", WordClasses.Nouns.Link(ConjugatorFuncs.Stem), WordClasses.Nouns.Link(ConjugatorFuncs.Stem), WordClasses.Adjectives.Link(ConjugatorFuncs.Dictionary));
        public static readonly GrammarPhrase ComparisonGroup = new GrammarPhrase("ComparisonGroup", "{0}のなかで{1}がいちばん{2}です", WordClasses.Nouns.Link(ConjugatorFuncs.Stem), WordClasses.Nouns.Link(ConjugatorFuncs.Stem), WordClasses.Adjectives.Link(ConjugatorFuncs.Dictionary));
        public static readonly GrammarPhrase FromTo = new GrammarPhrase("FromTo", "{0}から{1}まで", WordClasses.Nouns.Link(ConjugatorFuncs.Stem), WordClasses.Nouns.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase PurposeOfMovement = new GrammarPhrase("PurposeOfMovement", "{0}にいく", WordClasses.Verbs.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase TooMuch = new GrammarPhrase("TooMuch", "{0}すぎる", WordClasses.AdjectivesOrVerbs.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase WantToDo = new GrammarPhrase("WantToDo", "{0}たいです", WordClasses.Verbs.Link(ConjugatorFuncs.Stem));
        public static readonly GrammarPhrase ExampleThings = new GrammarPhrase("ExampleThings", "{0}や{1}", WordClasses.Nouns.Link(ConjugatorFuncs.Dictionary), WordClasses.Nouns.Link(ConjugatorFuncs.Dictionary));

        public static readonly GrammarPhrase Request = new GrammarPhrase("Request", "{0}ください", WordClasses.Verbs.Link(ConjugatorFuncs.Te));
        public static readonly GrammarPhrase Permission = new GrammarPhrase("Permission", "{0}もいいです", WordClasses.Verbs.Link(ConjugatorFuncs.Te));
        public static readonly GrammarPhrase Forbidden = new GrammarPhrase("Forbidden", "{0}はいけません", WordClasses.Verbs.Link(ConjugatorFuncs.Te));
        public static readonly GrammarPhrase OngoingAction = new GrammarPhrase("OngoingAction", "{0}いる", WordClasses.Verbs.Link(ConjugatorFuncs.Te));
        public static readonly GrammarPhrase Joining = new GrammarPhrase("Joining", "{0}、{1}", WordClasses.Any.Link(ConjugatorFuncs.Te), WordClasses.Any.Link(ConjugatorFuncs.Dictionary));
        public static readonly GrammarPhrase HaveNotYet = new GrammarPhrase("HaveNotYet", "まだ{0}いません", WordClasses.Verbs.Link(ConjugatorFuncs.Te));
        public static readonly GrammarPhrase RightAfterDoing = new GrammarPhrase("RightAfterDoing", "{0}から", WordClasses.Verbs.Link(ConjugatorFuncs.Te));

        public static readonly GrammarPhrase BeforeDoing = new GrammarPhrase("BeforeDoing", "{0}まえに", WordClasses.Verbs.Link(ConjugatorFuncs.PresentAffirmativeShort)); // dict
        public static readonly GrammarPhrase AfterDoing = new GrammarPhrase("AfterDoing", "{0}あとで", WordClasses.Verbs.Link(ConjugatorFuncs.PastAffirmativeShort)); // ～た
        public static readonly GrammarPhrase PleaseDoNot = new GrammarPhrase("PleaseDoNot", "{0}でください", WordClasses.Verbs.Link(ConjugatorFuncs.PresentNegativeShort)); // ～ない
        public static readonly GrammarPhrase LikeDoing = new GrammarPhrase("LikeDoing", "{0}のがすきです", WordClasses.Verbs.Link(ConjugatorFuncs.PresentAffirmativeShort)); // dict
        public static readonly GrammarPhrase GoodAtDoing = new GrammarPhrase("GoodAtDoing", "{0}のがじょうずです", WordClasses.Verbs.Link(ConjugatorFuncs.PresentAffirmativeShort)); // dict
        public static readonly GrammarPhrase BadAtDoing = new GrammarPhrase("BadAtDoing", "{0}のがへたです", WordClasses.Verbs.Link(ConjugatorFuncs.PresentAffirmativeShort)); // dict
        public static readonly GrammarPhrase PlanningToDo = new GrammarPhrase("PlanningToDo", "{0}つもりです", WordClasses.Verbs.Link(ConjugatorFuncs.PresentAffirmativeShort)); // dict
        public static readonly GrammarPhrase Because = new GrammarPhrase("Because", "{0}から", WordClasses.Any.Link(ConjugatorFuncs.PresentAffirmativeShort)); // ～だ/dict
        public static readonly GrammarPhrase BecauseNot = new GrammarPhrase("BecauseNot", "{0}から", WordClasses.Any.Link(ConjugatorFuncs.PresentNegativeShort)); // ～ない
        public static readonly GrammarPhrase BecausePolite = new GrammarPhrase("BecausePolite", "{0}から", WordClasses.Any.Link(ConjugatorFuncs.PresentAffirmativeLong)); // ～だ/dict
        public static readonly GrammarPhrase BecauseNotPolite = new GrammarPhrase("BecauseNotPolite", "{0}から", WordClasses.Any.Link(ConjugatorFuncs.PresentNegativeLong)); // ～ない
        public static readonly GrammarPhrase HaveHadExperience = new GrammarPhrase("HaveHadExperience", "{0}ことがいます", WordClasses.Verbs.Link(ConjugatorFuncs.PastAffirmativeShort)); // ～た
        public static readonly GrammarPhrase SuchThingsAs = new GrammarPhrase("SuchThingsAs", "{0}り{1}りする", WordClasses.Any.Link(ConjugatorFuncs.PastAffirmativeShort), WordClasses.Any.Link(ConjugatorFuncs.PastAffirmativeShort)); // ～た

        // sentence patterns from https://www.learn-japanese-adventure.com/japanese-grammar-cause-reason.html　applies to BecausePolite and ExplainImplicit
        public static readonly GrammarPhrase BecauseFormal = new GrammarPhrase("BecauseFormal", "{0}ので", WordClasses.Any.Link(ConjugatorFuncs.NaPresentAffirmative)); // ～だ ⇒ ～な for noun, adjective-な
        public static readonly GrammarPhrase BecauseNotFormal = new GrammarPhrase("BecauseNotFormal", "{0}ので", WordClasses.Any.Link(ConjugatorFuncs.NaPresentNegative)); // ～だ ⇒ ～な for noun, adjective-な
        public static readonly GrammarPhrase ExplainImplicit = new GrammarPhrase("ExplainImplicit", "{0}んです", WordClasses.Any.Link(ConjugatorFuncs.NaPresentAffirmative)); // ～だ ⇒ ～な for noun, adjective-な
        public static readonly GrammarPhrase ExplainImplicitWas = new GrammarPhrase("ExplainImplicitWas", "{0}んです", WordClasses.Any.Link(ConjugatorFuncs.NaPastAffirmative)); // ～だ ⇒ ～な for noun, adjective-な
        public static readonly GrammarPhrase ExplainImplicitNot = new GrammarPhrase("ExplainImplicitNot", "{0}んです", WordClasses.Any.Link(ConjugatorFuncs.NaPresentNegative)); // ～だ ⇒ ～な for noun, adjective-な
        public static readonly GrammarPhrase ExplainImplicitWasNot = new GrammarPhrase("ExplainImplicitWasNot", "{0}んです", WordClasses.Any.Link(ConjugatorFuncs.NaPastNegative)); // ～だ ⇒ ～な for noun, adjective-な

        // note: not using the 'なければ' variant of 'must'
        public static readonly GrammarPhrase Must = new GrammarPhrase("Must", "{0}くちゃいけません", WordClasses.Verbs.Link(ConjugatorFuncs.RemoveI)); // remove ～い for verbs
        public static readonly GrammarPhrase Probably = new GrammarPhrase("Probably", "{0}でしょう", WordClasses.Any.Link(ConjugatorFuncs.RemoveDaAffirmative)); // remove ～だ　for noun, adjective-な
        public static readonly GrammarPhrase ProbablyNot = new GrammarPhrase("ProbablyNot", "{0}でしょう", WordClasses.Any.Link(ConjugatorFuncs.RemoveDaNegative)); // remove ～だ　for noun, adjective-な
        public static readonly GrammarPhrase BetterToDo = new GrammarPhrase("BetterToDo", "{0}ほうがいいです", WordClasses.Verbs.Link(ConjugatorFuncs.PastAffirmativeShort)); // affirmative = past tense, negative = present tense
        public static readonly GrammarPhrase BetterNotToDo = new GrammarPhrase("BetterNotToDo", "{0}ほうがいいです", WordClasses.Verbs.Link(ConjugatorFuncs.PresentNegativeShort)); // affirmative = past tense, negative = present tense
        public static readonly GrammarPhrase Become = new GrammarPhrase("Become", "{0}なる", WordClasses.NounsOrAdjectives.Link(ConjugatorFuncs.Adverbial)); // remove ～い　add く／に for noun, adjective

        public GrammarPhrase(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
