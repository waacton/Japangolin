using System.Collections.Generic;

namespace Wacton.Japangolin.Conjugation
{
    // TODO: needs some more love - figure out a good way to handle all this complexity
    public class ModifierGrammar : ModifierBase
    {
        public override string Variation => "Grammar";
        public override bool IsHighLevel => true;

        // TODO: allow grammars to take different tense / polarity / formality if the grammar allows (as it stands is enough for current practice)
        //       e.g. ～んです works with present, past, affirmative, negative
        public static readonly ModifierGrammar ThereExistsNonLiving = new ModifierGrammar("ThereExistsNonLiving", "{0}があります", GetConj(WordClasses.Nouns, Stem));
        public static readonly ModifierGrammar ThereExistsLiving = new ModifierGrammar("ThereExistsLiving", "{0}がいます", GetConj(WordClasses.Nouns, Stem));
        public static readonly ModifierGrammar Invitation = new ModifierGrammar("Invitation", "{0}ませんか", GetConj(WordClasses.Verbs, Stem));
        public static readonly ModifierGrammar LetsDo = new ModifierGrammar("LetsDo", "{0}ましょう", GetConj(WordClasses.Verbs, Stem));
        public static readonly ModifierGrammar ShallWe = new ModifierGrammar("ShallWe", "{0}ましょうか", GetConj(WordClasses.Verbs, Stem));
        public static readonly ModifierGrammar ComparisonTwo = new ModifierGrammar("ComparisonTwo", "{0}のほうが{1}より{2}です", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Adjectives, Dictionary));
        public static readonly ModifierGrammar ComparisonGroup = new ModifierGrammar("ComparisonGroup", "{0}のなかで{1}がいちばん{2}です", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Adjectives, Dictionary));
        public static readonly ModifierGrammar FromTo = new ModifierGrammar("FromTo", "{0}から{1}まで", GetConj(WordClasses.Nouns, Stem), GetConj(WordClasses.Nouns, Stem));
        public static readonly ModifierGrammar PurposeOfMovement = new ModifierGrammar("PurposeOfMovement", "{0}にいく", GetConj(WordClasses.Verbs, Stem));
        public static readonly ModifierGrammar TooMuch = new ModifierGrammar("TooMuch", "{0}すぎる", GetConj(WordClasses.AdjectivesOrVerbs, Stem));
        public static readonly ModifierGrammar WantToDo = new ModifierGrammar("WantToDo", "{0}たいです", GetConj(WordClasses.Verbs, Stem));

        public static readonly ModifierGrammar Request = new ModifierGrammar("Request", "{0}ください", GetConj(WordClasses.Verbs, Te));
        public static readonly ModifierGrammar Permission = new ModifierGrammar("Permission", "{0}もいいです", GetConj(WordClasses.Verbs, Te));
        public static readonly ModifierGrammar Forbidden = new ModifierGrammar("Forbidden", "{0}はいけません", GetConj(WordClasses.Verbs, Te));
        public static readonly ModifierGrammar OngoingAction = new ModifierGrammar("OngoingAction", "{0}いる", GetConj(WordClasses.Verbs, Te));
        public static readonly ModifierGrammar Joining = new ModifierGrammar("Joining", "{0}、{1}", GetConj(WordClasses.Any, Te), GetConj(WordClasses.Any, Dictionary));
        public static readonly ModifierGrammar HaveNotYet = new ModifierGrammar("HaveNotYet", "まだ{0}いません", GetConj(WordClasses.Verbs, Te));
        public static readonly ModifierGrammar ImmediatelyAfterDoing = new ModifierGrammar("RightAfterDoing", "{0}から", GetConj(WordClasses.Verbs, Te));

        public static readonly ModifierGrammar BeforeDoing = new ModifierGrammar("BeforeDoing", "{0}まえに", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly ModifierGrammar AfterDoing = new ModifierGrammar("AfterDoing", "{0}あとで", GetConj(WordClasses.Verbs, PastAffirmativeShort)); // ～た
        public static readonly ModifierGrammar PleaseDoNot = new ModifierGrammar("PleaseDoNot", "{0}でください", GetConj(WordClasses.Verbs, PresentNegativeShort)); // ～ない
        public static readonly ModifierGrammar LikeDoing = new ModifierGrammar("LikeDoing", "{0}のがすきです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly ModifierGrammar GoodAtDoing = new ModifierGrammar("GoodAtDoing", "{0}のがじょうずです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly ModifierGrammar BadAtDoing = new ModifierGrammar("BadAtDoing", "{0}のがへたです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly ModifierGrammar PlanningToDo = new ModifierGrammar("PlanningToDo", "{0}つもりです", GetConj(WordClasses.Verbs, PresentAffirmativeShort)); // ～だ/dict
        public static readonly ModifierGrammar Because = new ModifierGrammar("Because", "{0}から", GetConj(WordClasses.Any, PresentAffirmativeShort)); // ～だ/dict
        public static readonly ModifierGrammar HaveHadExperience = new ModifierGrammar("HaveHadExperience", "{0}ことがいます", GetConj(WordClasses.Verbs, PastAffirmativeShort)); // ～た
        public static readonly ModifierGrammar SuchThingsAs = new ModifierGrammar("SuchThingsAs", "{0}り{1}りする", GetConj(WordClasses.Any, PastAffirmativeShort), GetConj(WordClasses.Any, PastAffirmativeShort)); // ～た

        // TODO: build on top of existing conjugation functions
        // sentence patterns from https://www.learn-japanese-adventure.com/japanese-grammar-cause-reason.html
        // applies to BecausePolite and ExplainImplicit

        public static readonly ModifierGrammar BecausePolite = new ModifierGrammar("BecausePolite", "{0}ので", GetConj(WordClasses.Any, Na)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/node-genki-i-chapter-12/
        public static readonly ModifierGrammar ExplainImplicit = new ModifierGrammar("ExplainImplicit", "{0}んです", GetConj(WordClasses.Any, Na)); // https://wp.stolaf.edu/japanese/grammar-index/genki-i-ii-grammar-index/n-desu-genki-i-chapter-12/

        //public static readonly Grammar Must = new Grammar("Must", "{0}", ShortPastAffirmative); // short negative -い...
        //public static readonly Grammar Probably = new Grammar("Probably", "{0}", ShortPastAffirmative); // replaces だ　for noun, adjective-な
        //public static readonly Grammar BetterToDo = new Grammar("BetterToDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar BetterToNotDo = new Grammar("BetterToNotDo", "{0}", ShortPastAffirmative); // affirmative = past tense, negative = present tense...
        //public static readonly Grammar Become = new Grammar("Become", "{0}", ShortPastAffirmative); // ななる　vs. くなる...

        public ModifierGrammar(string displayName, string format, params WordClassConjugator[] conjugators)
            : base(displayName, format, conjugators)
        {
        }
    }
}
