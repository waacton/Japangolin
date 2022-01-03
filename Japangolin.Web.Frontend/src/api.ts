export namespace Api {
  export async function getJapangolin() {
    return await api<Japangolin>("/random");
  }

  async function api<T>(url: string): Promise<T> {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data = await response.json();
    return data as T;
  }

  export interface Japangolin {
    word: Word;
    inflection: Inflection;
    hint: Hint;
    answerKana: string;
    answerKanji: string;
  }

  interface Word {
    kana: string;
    kanji: string;
    english: string;
    class: number;
  }

  interface Inflection {
    displayName: string;
  }

  interface Hint {
    baseForm: string;
    modification: string;
  }
}
