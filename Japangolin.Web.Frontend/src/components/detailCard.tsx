import { Card, Stack } from "@mui/material";
import { Hint, Word } from "../types/japangolin";
import { WordClass } from "../types/wordClass";
import { Detail, NoDetail } from "./detail";
import { pascalCase } from "../utils/stringUtils";

interface Props {
  word: Word;
  hint: Hint;
  wordSelected?: boolean;
  inflectionSelected?: boolean;
}

function DetailCard(props: Props) {
  const wordClass = WordClass[props.word.class];
  const wordClassText = wordClass ? pascalCase(WordClass[props.word.class], "-") : "[unknown enum]";

  function detail() {
    if (!props.wordSelected && !props.inflectionSelected) {
      return <NoDetail />;
    }

    const firstDetail = props.wordSelected ? props.word.kana : props.hint.baseForm;
    const secondDetail = props.wordSelected ? props.word.kanji : props.hint.modification;
    return <Detail firstDetail={firstDetail} secondDetail={secondDetail} thirdDetail={wordClassText} />;
  }

  return (
    <Card variant={"elevation"} sx={{ height: "100%", textAlign: "center" }}>
      <Stack sx={{ justifyContent: "center", alignItems: "center", height: "100%" }}>{detail()}</Stack>
    </Card>
  );
}

export default DetailCard;
