import './App.css'
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import SignUp from './component/SignUp'
import SignIn from './component/SignIn'
import HomePage from "./component/HomePage"

function App() {
  return (
    <div className="App">
      <Router>
        <Switch>
          <Route exact path="/" component={SignUp} />
          <Route exact path="/SignIn" component={SignIn} />
          <Route exact path="/HomePage" component={HomePage} />
        </Switch>
      </Router>
    </div>
  )
}

export default App
