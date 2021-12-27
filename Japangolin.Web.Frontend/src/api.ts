export namespace Api {
  export async function getJapangolin() {
    return await api<JapangolinMain>("/random");
  }

  async function api<T>(url: string): Promise<T> {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data = await response.json();
    return data as T;
  }

  type JapangolinMain = {
    word: Word;
    inflection: Inflection;
    hint: Hint;
    answerKana: string;
    answerKanji: string;
  };

  type Word = {
    kana: string;
    kanji: string;
    english: string;
    class: number;
  };

  type Inflection = {
    displayName: string;
  };

  type Hint = {
    baseForm: string;
    modification: string;
  };
}

