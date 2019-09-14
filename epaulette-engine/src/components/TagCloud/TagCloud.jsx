import React from 'react'
import { Link } from 'react-router-dom'
import { PostsApi, TagsApi } from 'epaulette-service-lib'
// http://fooplot.com/#W3sidHlwZSI6MCwiZXEiOiJ4XjIvKDEveCkiLCJjb2xvciI6IiMwMDAwMDAifSx7InR5cGUiOjEwMDAsIndpbmRvdyI6WyItMy4yNSIsIjMuMjUiLCItMiIsIjIiXX1d

const getTagSize = (totalPosts, tagCount, maxSize, minSize) => {
  const normalized = tagCount / totalPosts
  const parabola = (normalized * normalized) / (1 / normalized)

  return (1 - parabola) * minSize + parabola * maxSize
}

class TagCloud extends React.Component {
  constructor(props) {
    super(props)

    this.loadData = this.loadData.bind(this)

    this.state = {
      maxFontSize: 26,
      minFontSize: 12,
      postsCount: null,
      tagCounts: null,
    }
  }

  componentDidMount() {
    this.loadData()
  }

  loadData() {
    Promise.all([PostsApi.getCount(), TagsApi.getTagCounts()]).then(data => {
      const [postsCount, tagCounts] = data

      this.setState({
        postsCount,
        tagCounts
      })
    })
  }

  render() {
    const { tagCounts } = this.state

    if (!tagCounts) {
      return (
        <div>
          Loading ...
        </div>
      )
    }

    const { maxFontSize, minFontSize, postsCount } = this.state

    return (
      <div>
        {tagCounts.map((tagCount, index) => {
          const { tag: { name }, count } = tagCount

          return (
            <span key={index} style={ { fontSize: `${getTagSize(postsCount, count, maxFontSize, minFontSize)}px` } }>
              <Link to={ `/tags/${name}` } >
                {name}
              </Link>
            </span>
          )
        })}
      </div>
    )
  }
}

export default TagCloud;