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
    kana: "-",
    kanji: "-",
    english: "-",
    class: -1,
  },
  inflection: {
    displayName: "-",
  },
  hint: {
    baseForm: "-",
    modification: "-",
  },
  answerKana: "-",
  answerKanji: "-",
};
