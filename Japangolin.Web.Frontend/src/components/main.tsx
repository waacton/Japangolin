import { Box, Button, Stack, SvgIcon, SvgIconProps, TextField, Tooltip } from "@mui/material";
import Header from "./header";
import { Api } from "../api";
import { useEffect, useState } from "react";
import Filter from "./filter";
import WordOrInflection from "./wordOrInflection";
import { defaultJapangolin, Japangolin } from "../types/japangolin";
import { Detail, NoDetail } from "./detail";
import Answer from "./answer";

function SkipIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <path d="M12,14A2,2 0 0,1 14,16A2,2 0 0,1 12,18A2,2 0 0,1 10,16A2,2 0 0,1 12,14M23.46,8.86L21.87,15.75L15,14.16L18.8,11.78C17.39,9.5 14.87,8 12,8C8.05,8 4.77,10.86 4.12,14.63L2.15,14.28C2.96,9.58 7.06,6 12,6C15.58,6 18.73,7.89 20.5,10.72L23.46,8.86Z" />
    </SvgIcon>
  );
}

const showHighlight = false;
const bgcolor = showHighlight ? "yellow" : "transparent";

function Main() {
  const [japangolin, setJapangolin] = useState<Japangolin>(defaultJapangolin);
  const [loading, setLoading] = useState(false);
  const [wordSelected, setWordSelected] = useState(false);
  const [inflectionSelected, setInflectionSelected] = useState(false);
  const [answerShowing, setAnswerShowing] = useState(false);

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

  const detailComponent = () => {
    if (wordSelected) {
      return (
        <Detail
          firstDetail={japangolin.word.kana}
          secondDetail={japangolin.word.kanji}
          thirdDetail={japangolin.word.class.toString()}
        />
      );
    }

    if (inflectionSelected) {
      return (
        <Detail
          firstDetail={japangolin.hint.baseForm}
          secondDetail={japangolin.hint.modification}
          thirdDetail={japangolin.word.class.toString()}
        />
      );
    }

    return <NoDetail />;
  };

  return (
    <Box
      sx={{
        display: "grid",
        gridTemplateColumns: "2fr 2fr",
        gridTemplateRows: "repeat(6, auto)",
        bgcolor: (theme) => theme.custom.background,
        borderBottom: 1,
        borderColor: "#40404622",
      }}
    >
      <Box
        sx={{
          gridRow: 1,
          gridColumn: "1 / span 2",
          borderBottom: 1,
          borderColor: "#40404622",
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
        {detailComponent()}
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
        <TextField label={"Japanese Conjugation"} variant={"filled"} fullWidth />

        <Tooltip title={"Skip"} onClick={() => loadData()}>
          <Button variant={"outlined"} sx={{ width: 64, height: 64 }}>
            <SkipIcon />
          </Button>
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
          marginTop: 0.5,
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
