import { Japangolin } from "./types/japangolin";

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
}
