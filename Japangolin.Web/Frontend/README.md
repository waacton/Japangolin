# Getting Started with Create React App

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).

---

# Notes
When to use `type` vs `interface`? I like this answer from https://stackoverflow.com/a/65948871

# Relevant in 2021
For typescrpt version: 4.3.4

> Always prefer `interface` over `type`.

When to use `type`:
* Use `type` when defining an alias for primitive types (string, boolean, number, bigint, symbol, etc)
* Use `type` when defining tuple types
* Use `type` when defining function types
* Use `type` when defining a union
* Use `type` when trying to overload functions in object types via composition
* Use `type` when needing to take advantage of mapped types

When to use `interface`:
* Use `interface` for all object types where using `type` is not required (see above)
* Use `interface` when you want to take advatange of declaration merging.

## Primitive types
The easiest difference to see between `type` and `interface` is that only `type` can be used to alias a primitive:
```ts
type Nullish = null | undefined;
type Fruit = 'apple' | 'pear' | 'orange';
type Num = number | bigint;
```
None of these examples are possible to achieve with interfaces.

💡 When providing a type alias for a primitive value, use the `type` keyword.

## Tuple types
Tuples can only be typed via the `type` keyword:
```ts
type row = [colOne: number, colTwo: string];
```

💡 Use the `type` keyword when providing types for tuples.

## Function types
Functions can be typed by both the `type` and `interface` keywords:
```ts
// via type
type Sum = (x: number, y: number) => number;

// via interface
interface Sum {
  (x: number, y: number): number;
}
```
Since the same effect can be achieved either way, the rule will be to use `type` in these scenarios since it's a little easier to read (and less verbose).

💡 Use `type` when defining function types.


## Union types
Union types can only be achieved with the `type` keyword:
```ts
type Fruit = 'apple' | 'pear' | 'orange';
type Vegetable = 'broccoli' | 'carrot' | 'lettuce';

// 'apple' | 'pear' | 'orange' | 'broccoli' | 'carrot' | 'lettuce';
type HealthyFoods = Fruit | Vegetable;
```
💡 When defining union types, use the `type` keyword

## Object types
An object in javascript is a key/value map, and an "object type" is typescript's way of typing those key/value maps. Both `interface` and `type` can be used when providing types for an object as the original question makes clear. So when do you use `type` vs `interface` for object types?

### Intersection vs Inheritance
With types and composition, I can do something like this:
```ts
interface NumLogger { 
    log: (val: number) => void;
}
type StrAndNumLogger = NumLogger & { 
  log: (val: string) => void;
}

const logger: StrAndNumLogger = {
  log: (val: string | number) => console.log(val)
}

logger.log(1)
logger.log('hi')
```
Typescript is totally happy. What about if I tried to extend that with interface:
```ts

interface StrAndNumLogger extends NumLogger { 
    log: (val: string) => void; 
};
```
The declaration of `StrAndNumLogger` gives me an [error][1]:

`Interface 'StrAndNumLogger' incorrectly extends interface 'NumLogger'`

With interfaces, the subtypes have to exactly match the types declared in the super type, otherwise TS will throw an error like the one above.

💡 When trying to overload functions in object types, you'll be better off using the `type` keyword.

### Declaration Merging
The key aspect to interfaces in typescript that distinguish them from types is that they can be extended with new functionality after they've already been declared. A common use case for this feature occurs when you want to extend the types that are exported from a node module. For example, `@types/jest` exports types that can be used when working with the jest library. However, jest also allows for extending the main `jest` type with new functions. For example, I can add a custom test like this:
```ts
jest.timedTest = async (testName, wrappedTest, timeout) =>
  test(
    testName,
    async () => {
      const start = Date.now();
      await wrappedTest(mockTrack);
      const end = Date.now();

      console.log(`elapsed time in ms: ${end - start}`);
    },
    timeout
  );
```
And then I can use it like this:
```ts
test.timedTest('this is my custom test', () => {
  expect(true).toBe(true);
});
```
And now the time elapsed for that test will be printed to the console once the test is complete. Great! There's only one problem - typescript has no clue that i've added a `timedTest` function, so it'll throw an error in the editor (the code will run fine, but TS will be angry).

To resolve this, I need to tell TS that there's a new type on top of the existing types that are already available from jest. To do that, I can do this:
```ts
declare namespace jest {
  interface It {
    timedTest: (name: string, fn: (mockTrack: Mock) => any, timeout?: number) => void;
  }
}
```
Because of how interfaces work, this type declaration will be _merged_ with the type declarations exported from `@types/jest`. So I didn't just re-declare `jest.It`; I extended `jest.It` with a new function so that TS is now aware of my custom test function.

This type of thing is not possible with the `type` keyword. If `@types/jest` had declared their types with the `type` keyword, I wouldn't have been able to extend those types with my own custom types, and therefore there would have been no good way to make TS happy about my new function. This process that is unique to the `interface` keyword is called [declaration merging](https://www.typescriptlang.org/docs/handbook/declaration-merging.html).

Declaration merging is also possible to do locally like this:
```ts
interface Person {
  name: string;
}

interface Person {
  age: number;
}

// no error
const person: Person = {
  name: 'Mark',
  age: 25
};
```
If I did the exact same thing above with the `type` keyword, I would have gotten an error since types cannot be re-declared/merged. In the real world, javascript objects are much like this `interface` example; they can be dynamically updated with new fields at runtime.

💡 Because interface declarations can be merged, interfaces more accurately represent the dynamic nature of javascript objects than types do, and they should be preferred for that reason.

### Mapped object types
With the `type` keyword, I can take advantage of [mapped types](https://www.typescriptlang.org/docs/handbook/advanced-types.html#mapped-types) like this:
```ts
type Fruit = 'apple' | 'orange' | 'banana';

type FruitCount = {
  [key in Fruit]: number;
}

const fruits: FruitCount = {
  apple: 2,
  orange: 3,
  banana: 4
};
```
This cannot be done with interfaces:
```ts
type Fruit = 'apple' | 'orange' | 'banana';

// ERROR: 
interface FruitCount {
  [key in Fruit]: number;
}
```
💡 When needing to take advantage of mapped types, use the `type` keyword

### Performance

Much of the time, a simple type alias to an object type acts very similarly to an interface.

```
interface Foo { prop: string }

type Bar = { prop: string };
```

However, and as soon as you need to compose two or more types, you have the option of extending those types with an interface, or intersecting them in a type alias, and that's when the differences start to matter.

Interfaces create a single flat object type that detects property conflicts, which are usually important to resolve! Intersections on the other hand just recursively merge properties, and in some cases produce never. Interfaces also display consistently better, whereas type aliases to intersections can't be displayed in part of other intersections. Type relationships between interfaces are also cached, as opposed to intersection types as a whole. A final noteworthy difference is that when checking against a target intersection type, every constituent is checked before checking against the "effective"/"flattened" type.

For this reason, extending types with interfaces/extends is suggested over creating intersection types.

More on [typescript wiki][2].

[1]: https://www.typescriptlang.org/play?#code/JYOwLgpgTgZghgYwgAgHIFcC2AZA9gc32mQG9kAoZK5AGwIC5kAKANzhsZCwCNoBKZAF4AfMha5gAEwDc5AL7lQkWIhQBlMFACCISRhwEiUZBAAekXQGc0WPIWJlK1Ovkat2jS5tD4BIsRIyFHLSQA
[2]: https://github.com/microsoft/TypeScript/wiki/Performance