// note that this is brittle
// if the server ever changes the enum order, or adds anymore, this will need to be updated accordingly
// (alternative is for the server to send the actual display text, but this is good for practice)
export enum WordClass {
  Unknown,
  Noun,
  AdjectiveNa,
  AdjectiveI,
  VerbRu,
  VerbU,
}
