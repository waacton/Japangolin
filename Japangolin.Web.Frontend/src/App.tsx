import './App.css';

import HelloWorld from "./components/HelloWorld";

function App() {
  return (
      <div className="App">
          <HelloWorld value={"Here is a prop value"} />
      </div>
  );
}

export default App;
