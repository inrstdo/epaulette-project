import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter as Router } from 'react-router-dom'
import PageContainer from './components/PageContainer/PageContainer'

class App extends React.Component {
  constructor() {
    super()
  }

  render() {
    return (
      <Router>
        <PageContainer />
      </Router>
    )
  }
}

export default App

ReactDOM.render(<App />, document.getElementById('app'))
