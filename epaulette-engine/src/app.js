import React from 'react'
import ReactDOM from 'react-dom'
import PageContainer from './components/PageContainer/PageContainer'

class App extends React.Component {
  constructor() {
    super()
  }

  render() {
    return (
      <PageContainer />
    )
  }
}

export default App

ReactDOM.render(<App />, document.getElementById('app'))
