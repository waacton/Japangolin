namespace Wacton.Japangolin.Conjugation
{
    public class Infos
    {
        public static string Dict() => "[dict]";

        public static string NounStem() => Dict();
        public static string NounFormTe() => NounStem() + "　＋で";

        public static string AdjNaStem() => Dict() + "　ーな";
        public static string AdjNaFormTe() => AdjNaStem() + "　＋で";

        public static string AdjIStem() => Dict() + "　ーい";
        public static string AdjIFormTe() => AdjIStem() + "　＋くて";

        public static string VerbRuStem() => Dict() + "　ーる";
        public static string VerbRuFormTe() => VerbRuStem() + "　ーる　＋て";
        public static string VerbRuFormTa() => VerbRuStem() + "　ーる　＋た";

        public static string VerbUStemI() => Dict() + "《う→い》";
        public static string VerbUStemA() => Dict() + "《う→あ》";
        public static string VerbUFormTe() => Dict() + "　ー《う》　＋《て》"; // TODO: need to be more specific about how to handle て-form?
        public static string VerbUFormTa() => Dict() + "　ー《う》　＋《た》"; // TODO: need to be more specific about how to handle た-form?
    }
}
