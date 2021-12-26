import { useEffect, useState } from "react";

type JapangolinTestProps = {
    textColor: string;
};

function JapangolinTest(props: JapangolinTestProps) {
    const [wordKana, setWordKana] = useState("[n/a]");
    const [wordKanji, setWordKanji] = useState("[n/a]");
    const [wordEnglish, setWordEnglish] = useState("[n/a]");
    const [wordClass, setWordClass] = useState(-1);
    const [inflection, setInflection] = useState("[n/a]");
    const [hintBaseForm, setHintBaseForm] = useState("[n/a]");
    const [hintModification, setHintModification] = useState("[n/a]");
    const [answerKana, setAnswerKana] = useState("[n/a]");
    const [answerKanji, setAnswerKanji] = useState("[n/a]");
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        loadData()
    }, [])

    const loadData = async () => {
        setLoading(true);

        const response = await fetch('/random');
        const data = await response.json();
        setWordKana(data.word.kana);
        setWordKanji(data.word.kanji);
        setWordEnglish(data.word.english);
        setWordClass(data.word.class);
        setInflection(data.inflection.displayName);
        setHintBaseForm(data.hint.baseForm);
        setHintModification(data.hint.modification);
        setAnswerKana(data.answerKana);
        setAnswerKanji(data.answerKanji);
        setLoading(false);
    }

    return (
        <div>
            <h1 style={{color: props.textColor}}>Japangolin!</h1>
            <h3 style={{color: props.textColor}}>Prop (text colour): {props.textColor}</h3>
            <h3 style={{color: props.textColor}}>Word kana: {wordKana}</h3>
            <h3 style={{color: props.textColor}}>Word kanji: {wordKanji}</h3>
            <h3 style={{color: props.textColor}}>Word english: {wordEnglish}</h3>
            <h3 style={{color: props.textColor}}>Word class: {wordClass}</h3>
            <h3 style={{color: props.textColor}}>Inflection: {inflection}</h3>
            <h3 style={{color: props.textColor}}>Hint base form: {hintBaseForm}</h3>
            <h3 style={{color: props.textColor}}>Hint modification: {hintModification}</h3>
            <h3 style={{color: props.textColor}}>Answer kana: {answerKana}</h3>
            <h3 style={{color: props.textColor}}>Answer kanji: {answerKanji}</h3>
            <button onClick={() => loadData()} disabled={loading}>Update</button>
        </div>
    )
}

export default JapangolinTest