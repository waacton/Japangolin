import './App.css';
import logo from './Japangolin-round.png';

import JapangolinTest from "./components/JapangolinTest";

function App() {
  return (
      <div className="App" style={{background: "#FAFAFA", height: "100vh", display: "flex", justifyContent: "center", flexDirection: "column", alignItems: "center"}}>
          <div style={{background: "#404040", height: "25vmin", width: "25vmin", display: "flex", justifyContent: "center", alignItems: "center"}}>
              <img src={logo} className="App-logo" alt="logo" style={{height: "20vmin"}} />
          </div>

          <JapangolinTest textColor={"#404040"} />
      </div>
  );
}

export default App;
