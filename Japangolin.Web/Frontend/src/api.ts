import { Japangolin } from "./types/japangolin";

export namespace Api {
  export async function getJapangolin(jlptN5: boolean) {
    return await api<Japangolin>(`/random?jlptN5=${jlptN5}`);
  }

  async function api<T>(url: string): Promise<T> {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data = await response.json();
    return data as T;
  }
}
