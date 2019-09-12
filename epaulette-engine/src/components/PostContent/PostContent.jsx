import React from 'react'
import { Link } from 'react-router-dom'
import { api } from 'epaulette-service-lib'

const LATEST_POST_ID = 'latest'

class PostContent extends React.Component {
  constructor(props) {
    super(props)

    this.loadData = this.loadData.bind(this)

    this.state = {
      current: null,
      next: null,
      prev: null
    }
  }

  componentDidMount() {
    const { match: { params: { postId } } } = this.props

    this.loadData(postId)
  }

  componentWillReceiveProps(nextProps) {
    const { match: { params: { postId: nextPostId } } } = nextProps
    const { match: { params: { postId: currentPostId } } } = this.props

    if (nextPostId !== currentPostId) {
      this.loadData(nextPostId)
    }
  }

  loadData(postId) {
    const updateState = (dataObject) => {
      const { current, next, prev } = dataObject

      this.setState({
        current,
        next,
        prev
      })
    }

    if (postId === LATEST_POST_ID) {
      api.getLatestPost().then(dataObject => {
        updateState(dataObject)
      })
    }
    else {
      api.getPost(postId).then(dataObject => {
        updateState(dataObject)
      })
    }
  }

  render() {
    const { current, next, prev } = this.state

    if (!current) {
      return (
        <div>
          Loading ...
        </div>
      )
    }

    return (
      <div>
        <div>
        { current.date }
        </div>
        <div>
          {next && <Link to={`/posts/${next.postId}`}>
            Next
          </Link>}
          {prev && <Link to={`/posts/${prev.postId}`}>
            Prev
          </Link>}
        </div>
      </div>
    )
  }
}

export default PostContent;