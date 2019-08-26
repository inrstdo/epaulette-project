import React from 'react'
import ReactDOM from 'react-dom'
import styles from './app.module'

const App = () => {
  return (
    <div className={ styles.red }>
      <p>
        Epaulette!
      </p>
    </div>
  )
}

export default App

ReactDOM.render(<App />, document.getElementById('app'))
