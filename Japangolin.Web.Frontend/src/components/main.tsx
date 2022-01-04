import { Box, Stack, Tooltip } from "@mui/material";
import Header from "./header";
import { Api } from "../api";
import { useEffect, useState } from "react";
import Filter from "./filter";
import WordOrInflection from "./wordOrInflection";
import { defaultJapangolin, Japangolin } from "../types/japangolin";
import Answer from "./answer";
import GradientIconButton from "./gradientIconButton";
import { SkipIcon } from "../utils/customIcons";
import JapaneseInput from "./japaneseInput";
import DetailCard from "./detailCard";

const showHighlight = false;
const bgcolor = showHighlight ? "yellow" : "transparent";

function Main() {
  const [japangolin, setJapangolin] = useState<Japangolin>(defaultJapangolin);
  const [loading, setLoading] = useState(false);
  const [wordSelected, setWordSelected] = useState(false);
  const [inflectionSelected, setInflectionSelected] = useState(false);
  const [answerShowing, setAnswerShowing] = useState(false);
  const [userInput, setUserInput] = useState("");
  const handleUserInput = (event: React.ChangeEvent<HTMLInputElement>) => {
    console.log(event.target.value);
    setUserInput(event.target.value);
  };

  function selectWord() {
    setWordSelected(true);
    setInflectionSelected(false);
  }

  function selectInflection() {
    setWordSelected(false);
    setInflectionSelected(true);
  }

  function resetState() {
    setWordSelected(false);
    setInflectionSelected(false);
    setAnswerShowing(false);
    setUserInput("");
  }

  useEffect(() => {
    loadData();
  }, []);

  async function loadData() {
    console.log("Fetching data...");
    resetState();
    setLoading(true);
    const data = await Api.getJapangolin();
    setJapangolin(data);
    setLoading(false);
    console.log("... data retrieved");
    console.log(data);
  }

  return (
    <Box
      sx={{
        display: "grid",
        gridTemplateColumns: "2fr 2fr",
        gridTemplateRows: "repeat(6, auto)",
        bgcolor: (theme) => theme.custom.background,
        borderBottom: 1,
        borderColor: (theme) => `${theme.custom.wactonDark}22`,
      }}
    >
      <Box
        sx={{
          gridRow: 1,
          gridColumn: "1 / span 2",
          borderBottom: 1,
          borderColor: (theme) => `${theme.custom.wactonDark}22`,
          bgcolor: "background.paper",
        }}
      >
        <Header />
      </Box>

      <Stack
        sx={{
          gridRow: 2,
          gridColumn: "1 / span 2",
          justifyContent: "center",
          alignItems: "flex-end",
          marginLeft: 2,
          marginRight: 2,
          marginTop: 1,
          bgcolor: bgcolor,
        }}
      >
        <Filter>JLPT N5</Filter>
      </Stack>

      <Stack
        direction={"column"}
        sx={{
          gridRow: 3,
          gridColumn: 1,
          marginLeft: 2,
          marginTop: 1,
          marginBottom: 1,
          marginRight: 1,
          bgcolor: bgcolor,
        }}
      >
        <WordOrInflection label={"word"} text={japangolin.word.english} selected={wordSelected} onSelect={selectWord} />
      </Stack>

      <Stack
        direction={"column"}
        sx={{
          gridRow: 4,
          gridColumn: 1,
          marginLeft: 2,
          marginTop: 1,
          marginBottom: 1,
          marginRight: 1,
          bgcolor: bgcolor,
        }}
      >
        <WordOrInflection
          label={"inflection"}
          text={japangolin.inflection.displayName}
          selected={inflectionSelected}
          onSelect={selectInflection}
        />
      </Stack>

      <Box
        sx={{
          gridRow: "3 / span 2",
          gridColumn: 2,
          marginLeft: 1,
          marginTop: 1,
          marginBottom: 1,
          marginRight: 2,
          bgcolor: bgcolor,
        }}
      >
        <DetailCard
          word={japangolin.word}
          hint={japangolin.hint}
          wordSelected={wordSelected}
          inflectionSelected={inflectionSelected}
        />
      </Box>

      <Stack
        direction={"row"}
        sx={{
          gridRow: 5,
          gridColumn: "1 / span 2",
          gap: 2,
          justifyContent: "flex-start",
          alignItems: "center",
          marginLeft: 2,
          marginTop: 1,
          marginRight: 2,
          bgcolor: bgcolor,
        }}
      >
        <JapaneseInput value={userInput} onChange={handleUserInput} />

        <Tooltip title={"Skip"}>
          <GradientIconButton icon={SkipIcon} width={56} height={56} disabled={loading} onClick={() => loadData()} />
        </Tooltip>
      </Stack>

      <Stack
        direction={"row"}
        sx={{
          gridRow: 6,
          gridColumn: "1 / span 2",
          gap: 1,
          justifyContent: "flex-start",
          alignItems: "center",
          marginLeft: 2,
          marginRight: 2,
          marginTop: 1,
          marginBottom: 2,
          bgcolor: bgcolor,
        }}
      >
        <Answer
          showAnswer={answerShowing}
          onShowAnswer={() => setAnswerShowing(true)}
        >{`${japangolin.answerKana} · ${japangolin.answerKanji}`}</Answer>
      </Stack>
    </Box>
  );
}

export default Main;
