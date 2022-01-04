import { Box, Snackbar, Stack, Tooltip } from "@mui/material";
import Header from "./header";
import { Api } from "../api";
import { useCallback, useEffect, useState } from "react";
import Filter from "./filter";
import WordOrInflection from "./wordOrInflection";
import { defaultJapangolin, Japangolin } from "../types/japangolin";
import Answer from "./answer";
import GradientIconButton from "./gradientIconButton";
import { SkipIcon } from "../utils/customIcons";
import JapaneseInput from "./japaneseInput";
import DetailCard from "./detailCard";
import { pascalCase } from "../utils/stringUtils";

const showHighlight = false;
const bgcolor = showHighlight ? "yellow" : "transparent";

function Main() {
  const shouldLog = true;

  const [japangolin, setJapangolin] = useState<Japangolin>(defaultJapangolin);
  const [loading, setLoading] = useState(false);
  const [wordSelected, setWordSelected] = useState(false);
  const [inflectionSelected, setInflectionSelected] = useState(false);
  const [answerShowing, setAnswerShowing] = useState(false);
  const [snackbarShowing, setSnackbarShowing] = useState(false);
  const [userInput, setUserInput] = useState("");

  /*
  this is a lot of react-guff for a simple one-time "component did mount" action
  mostly using useCallback to appease eslint's "exhaustive-deps" warning for useEffect, as it's probably good practice
  ----------
  useCallback is typically a last resort (https://reactjs.org/docs/hooks-faq.html#is-it-safe-to-omit-functions-from-the-list-of-dependencies)
  ensures the fetchData function doesn't change on every render, unless its own dependencies change (i.e. shouldLog in this contrived example)
  the recommended fix is to define the function inside useEffect itself
  but using useCallback instead so that the function can be called from elsewhere (e.g. clicking skip button)
   */
  const fetchData = useCallback(async () => {
    if (shouldLog) console.log("Fetching data...");
    resetState();
    setLoading(true);
    const data = await Api.getJapangolin();
    setJapangolin(data);
    setLoading(false);
    if (shouldLog) console.log("... data retrieved");
    if (shouldLog) console.log(data);
  }, [shouldLog]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

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

  async function handleUserInput(event: React.ChangeEvent<HTMLInputElement>) {
    const text = event.target.value;
    setUserInput(text);

    // don't use `userInput` state here as it does not update asynchronously
    // and it seems overkill to handle it as a `useEffect` "side-effect" when it's really a direct effect
    if (text == null) {
      return;
    }

    const isCorrect = text === japangolin.answerKana || text === japangolin.answerKanji;
    if (!isCorrect) {
      return;
    }

    setSnackbarShowing(true);
    await fetchData();
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
          text={pascalCase(japangolin.inflection.displayName, " · ")}
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
          <GradientIconButton icon={SkipIcon} width={56} height={56} disabled={loading} onClick={() => fetchData()} />
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

      <Snackbar
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
        open={snackbarShowing}
        autoHideDuration={3000}
        onClose={() => setSnackbarShowing(false)}
        message="🏆 Correct!"
      />
    </Box>
  );
}

export default Main;
