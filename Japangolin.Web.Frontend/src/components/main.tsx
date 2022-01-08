import { Alert, AlertTitle, Box, LinearProgress, Snackbar, Stack, Tooltip } from "@mui/material";
import Header from "./header";
import { Api } from "../api";
import React, { useCallback, useEffect, useState } from "react";
import Filter from "./filter";
import WordOrInflection from "./wordOrInflection";
import { defaultJapangolin, Japangolin } from "../types/japangolin";
import Answer from "./answer";
import GradientIconButton from "./gradientIconButton";
import { SkipNext } from "@mui/icons-material";
import JapaneseInput from "./japaneseInput";
import DetailCard from "./detailCard";
import { pascalCase } from "../utils/stringUtils";

const showHighlight = false;
const bgcolor = showHighlight ? "yellow" : "transparent";

function Main() {
  const shouldLog = false;

  const [japangolin, setJapangolin] = useState<Japangolin>(defaultJapangolin);
  const [loading, setLoading] = useState(false);
  const [jlptN5Filtered, setJlptN5Filtered] = useState(true);
  const [wordSelected, setWordSelected] = useState(false);
  const [inflectionSelected, setInflectionSelected] = useState(false);
  const [answerShowing, setAnswerShowing] = useState(false);
  const [userInput, setUserInput] = useState("");
  const [snackbarShowing, setSnackbarShowing] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | undefined>();

  /*
  this is a lot of react-guff for a simple one-time "component did mount" action
  mostly using useCallback to appease eslint's "exhaustive-deps" warning for useEffect, as it's probably good practice
  ----------
  useCallback is typically a last resort (https://reactjs.org/docs/hooks-faq.html#is-it-safe-to-omit-functions-from-the-list-of-dependencies)
  it ensures the fetch data function doesn't change on every render, unless its own dependencies change
  (e.g. if shouldLog was modified: a) the function would update b) the initial data fetch would re-trigger as the function itself is a dependency to useEffect)
  ----------
  the recommended fix is to define the function inside useEffect itself
  but using useCallback instead so that the function can be called from elsewhere (e.g. clicking skip button)
   */
  const memoizedFetchDataFunc = useCallback(
    async (jlptN5: boolean) => {
      if (shouldLog) console.log(`fetching data (JLPT N5 = ${jlptN5}) ...`);
      setLoading(true);

      let data: Japangolin = defaultJapangolin;
      try {
        data = await Api.getJapangolin(jlptN5);
        if (shouldLog) console.log("... data retrieved");
        if (shouldLog) console.log(data);
      } catch (e: unknown) {
        if (shouldLog) console.log(e);

        let message: string;
        if (e instanceof Error) {
          message = e.message;
        } else if (typeof e === "string") {
          message = e;
        } else {
          message = "Something unexpected happened...";
        }

        setErrorMessage(message);
      }

      setJapangolin(data);
      resetState();
      setLoading(false);
    },
    [shouldLog]
  );

  // initial render data fetching
  useEffect(() => {
    memoizedFetchDataFunc(true);
  }, [memoizedFetchDataFunc]);

  // general purpose data fetching
  async function fetchData() {
    await memoizedFetchDataFunc(jlptN5Filtered);
  }

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
    setUserInput(event.target.value);
  }

  async function handleUserKeyUp(event: React.KeyboardEvent<HTMLInputElement>) {
    const text = (event.target as HTMLInputElement).value; // shame the generic event typing doesn't help

    // don't use `userInput` state here as it does not update asynchronously
    // and it seems overkill to handle it as a `useEffect` "side-effect" when it's really a direct effect
    if (!text) {
      return;
    }

    const isCorrect = text === japangolin.answerKana || text === japangolin.answerKanji;
    if (!isCorrect) {
      return;
    }

    setSnackbarShowing(true);
    await fetchData();
  }

  const isKanjiDifferent = japangolin.answerKanji !== japangolin.answerKana;
  const answerText = isKanjiDifferent ? `${japangolin.answerKana} · ${japangolin.answerKanji}` : japangolin.answerKana;
  const disabled: boolean = loading || errorMessage !== undefined;

  return (
    <Box>
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
          <Filter
            disabled={disabled}
            checked={jlptN5Filtered}
            onChange={(event) => setJlptN5Filtered((event.target as HTMLInputElement).checked)} // nasty
          >
            JLPT N5
          </Filter>
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
          <WordOrInflection
            label={"word"}
            text={japangolin.word.english}
            selected={wordSelected}
            onSelect={selectWord}
            disabled={disabled}
          />
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
            disabled={disabled}
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
            disabled={disabled}
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
          <JapaneseInput value={userInput} onChange={handleUserInput} onKeyUp={handleUserKeyUp} disabled={disabled} />

          <Tooltip title={"Skip"}>
            <Box>
              {/* see discussion in tooltipWithForwardRef.tsx for why <Box> is needed */}
              <GradientIconButton
                icon={SkipNext}
                width={56}
                height={56}
                disabled={disabled}
                onClick={() => fetchData()}
              />
            </Box>
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
          <Answer showAnswer={answerShowing} onShowAnswer={() => setAnswerShowing(true)} disabled={disabled}>
            {answerText}
          </Answer>
        </Stack>
      </Box>

      <LinearProgress sx={{ visibility: loading ? "visible" : "collapse" }} />

      <Alert severity="error" sx={{ margin: 2, visibility: errorMessage !== undefined ? "visible" : "collapse" }}>
        <AlertTitle>{`Error: ${errorMessage}`}</AlertTitle>
        There might be an issue with the server. Try refreshing the page and crossing your fingers 🤞
      </Alert>

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
