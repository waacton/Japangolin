export interface Japangolin {
  word: Word;
  inflection: Inflection;
  hint: Hint;
  answerKana: string;
  answerKanji: string;
}

export interface Word {
  kana: string;
  kanji: string;
  english: string;
  class: number;
}

interface Inflection {
  displayName: string;
}

export interface Hint {
  baseForm: string;
  modification: string;
}

export const defaultJapangolin: Japangolin = {
  word: {
    kana: "[kana]",
    kanji: "[kanji]",
    english: "[english]",
    class: -1,
  },
  inflection: {
    displayName: "[inflection]",
  },
  hint: {
    baseForm: "[base form]",
    modification: "[modification]",
  },
  answerKana: "[answer kana]",
  answerKanji: "[answer kanji]",
};
