import React from 'react'
import { api } from 'epaulette-service-lib'

class PostContent extends React.Component {
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
    const { data } = this.state

    return (
      <div>
        { data }
      </div>
    )
  }
}

export default PostContent;