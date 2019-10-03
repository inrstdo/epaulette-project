import React from 'react'
import PropTypes from 'prop-types'
import { Route, Redirect, withRouter } from 'react-router-dom'
import './PageContainer.scss'
import '../PostContent/PostContent'
import PostContent from '../PostContent/PostContent'
import TagCloud from '../TagCloud/TagCloud'

class PageContainer extends React.Component
{
  constructor(props) {
    super(props)
  }

  render() {
    const { location: { pathname } } = this.props

    if (pathname === '/') {
      return (
        <Redirect to={ '/posts/latest' } />
      )
    }

    return (
      <div className="page-container">
        <div className='header-container blue'>
          <h1>
            Epaulette
          </h1>
        </div>
        <div className='column-container'>
          <div className='column blue sidebar left'>
            <TagCloud />
          </div>
          <div className='column center'>
            <Route component={ PostContent } path='/posts/:postId' />
            <Route path='/tags/:search' render={ (props) => {
              return (
                <div>
                  { props.match.params.search }
                </div>
              )
            } } />
          </div>
          <div className='column blue sidebar right'>
            Calendar
          </div>
        </div>
      </div>
    )
  }
}

PageContainer.propTypes = {
  location: PropTypes.shape({
    pathname: PropTypes.string.isRequired
  }).isRequired
}

export default withRouter(PageContainer);
