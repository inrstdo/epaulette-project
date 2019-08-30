import React from 'react'
import ReactDOM from 'react-dom'
import styles from './app.module'
import api from './api/read-service-posts'

class App extends React.Component {
  constructor() {
    super()

    this.loadData = this.loadData.bind(this)

    this.state = {
      data: "Epaulette!"
    }
  }

  componentDidMount() {
    this.loadData()
  }

  loadData() {
    api.getLatestPost().then(dataObject => {
      this.setState({
        data: JSON.stringify(dataObject)
      })
    })
  }

  render() {
    const { red } = styles;
    const { data } = this.state;

    return (
      <div className={ red }>
        <p>
          { data }
        </p>
      </div>
    )
  }
}

export default App

ReactDOM.render(<App />, document.getElementById('app'))
