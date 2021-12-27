import "./App.css";
import logo from "./Japangolin-swirl.png";

import Japangolin from "./components/japangolin";

function App() {
  return (
    <div
      className="App"
      style={{ background: "#FAFAFA", height: "100vh", display: "flex", justifyContent: "center", flexDirection: "column", alignItems: "center" }}
    >
      <div style={{ background: "#404046", height: "25vmin", width: "25vmin", display: "flex", justifyContent: "center", alignItems: "center" }}>
        <img src={logo} className="App-logo" alt="logo" style={{ height: "20vmin" }} />
      </div>

      <Japangolin textColor={"#404046"} />
    </div>
  );
}

export default App;
