# Introduction
This project is an example implementation using React, Redux, Webpack and ASP.net Core.

# Getting Started
You need to install TypeScript 2.3.X in VS2017 in order to avoid red squigglies. The compiler (in our case webpack) knows about TS2.3.4 and handles it well, but VS does error checking via its own copy. To update it, download and install *version 2.3.X* from https://www.microsoft.com/en-us/download/details.aspx?id=55258 (note it'll default to 2.4.X, i don't know if that'll work). After downloading, in VS2017, go to Tools -> Options -> Text Editor -> Javascript/Typescript -> Intellisense and change the used version to 2.3.

This is a bit painful, but it's necessary whilst we're effectively using two different tooling stacks. Note that it's pretty easy to work outside VS, and many people do (VSCode is an oft-quoted alternative).