// looks for any character from
// CJK Radicals Supplement (\u2E80) to CJK Unified Ideographs Extension G (\u3134F)
// see https://unicode-table.com/en/blocks/ for details
export function containsNonLatinCharacters(text: string) {
  return /[\u{2E80}-\u{3134F}]/u.test(text);
}