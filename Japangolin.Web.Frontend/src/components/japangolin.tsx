import { useEffect, useState } from "react";
import { Api } from "../api";
import { Button, Typography } from "@mui/material";
import Icon from "@mui/material/Icon";
import StarIcon from "@mui/icons-material/Star";

type JapangolinProps = {
  textColor: string;
};

function Japangolin(props: JapangolinProps) {
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
    loadData();
  }, []);

  const loadData = async () => {
    console.log("Fetching data...");
    setLoading(true);
    const data = await Api.getJapangolin();
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
    console.log("... data retrieved");
    console.log(data);
  };

  return (
    <div>
      <Icon color={"secondary"}>star</Icon>
      <StarIcon color={"primary"} />
      <Typography style={{ color: props.textColor }}>Japangolin!</Typography>
      <Typography style={{ color: props.textColor }}>Prop (text colour): {props.textColor}</Typography>
      <Typography style={{ color: props.textColor }}>Word kana: {wordKana}</Typography>
      <Typography style={{ color: props.textColor }}>Word kanji: {wordKanji}</Typography>
      <Typography style={{ color: props.textColor }}>Word english: {wordEnglish}</Typography>
      <Typography style={{ color: props.textColor }}>Word class: {wordClass}</Typography>
      <Typography style={{ color: props.textColor }}>Inflection: {inflection}</Typography>
      <Typography style={{ color: props.textColor }}>Hint base form: {hintBaseForm}</Typography>
      <Typography style={{ color: props.textColor }}>Hint modification: {hintModification}</Typography>
      <Typography style={{ color: props.textColor }}>Answer kana: {answerKana}</Typography>
      <Typography style={{ color: props.textColor }}>Answer kanji: {answerKanji}</Typography>
      <Button variant={"contained"} onClick={() => loadData()} disabled={loading}>
        Update
      </Button>
    </div>
  );
}

export default Japangolin;
