import { Box, Button, Card, Stack, SvgIcon, SvgIconProps, TextField, Tooltip, Typography } from "@mui/material";
import Header from "./header";
import VisibilityIcon from "@mui/icons-material/Visibility";
import { Api } from "../api";
import { useEffect, useState } from "react";
import Filter from "./filter";
import WordOrInflection from "./wordOrInflection";

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
  const [wordSelected, setWordSelected] = useState(false);
  const [inflectionSelected, setInflectionSelected] = useState(false);

  const selectWord = () => {
    setWordSelected(true);
    setInflectionSelected(false);
  };

  const selectInflection = () => {
    setWordSelected(false);
    setInflectionSelected(true);
  };

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
    <Box
      sx={{
        display: "grid",
        gridTemplateColumns: "2fr 2fr",
        gridTemplateRows: "repeat(6, auto)",
        bgcolor: "#FAFAFA",
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
        <Filter text={"JLPT N5"} />
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
        <WordOrInflection label={"word"} text={wordEnglish} selected={wordSelected} onSelect={selectWord} />
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
          text={inflection}
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
        <Card variant={"elevation"} sx={{ height: "100%" }}>
          <Stack sx={{ justifyContent: "center", alignItems: "center", height: "100%" }}>
            <Typography sx={{ fontWeight: "light", fontSize: "0.75rem", opacity: 0.6 }}>
              Select a word or inflection to see a hint
            </Typography>
          </Stack>
        </Card>
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
        <Tooltip title={"View Answer"}>
          <Button variant={"outlined"} sx={{ width: 32, height: 32, minWidth: 32 }}>
            <VisibilityIcon sx={{ width: 16, height: 16 }} />
          </Button>
        </Tooltip>

        <Typography sx={{ fontSize: "0.75rem", opacity: 0.6 }}>Click to reveal the answer</Typography>
      </Stack>
    </Box>
  );
}

export default Main;
