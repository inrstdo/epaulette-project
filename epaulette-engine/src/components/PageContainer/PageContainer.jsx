import React from 'react'
import { Route, Redirect, withRouter } from 'react-router-dom'
import './PageContainer.scss'
import '../PostContent/PostContent'
import PostContent from '../PostContent/PostContent';

class PageContainer extends React.Component
{
  constructor(props) {
    super(props)
  }

  render() {
    const { location: { pathname } } = this.props

    if (pathname === '/') {
      return (
        <Redirect to={'/posts/latest'} />
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
            Tag Cloud
          </div>
          <div className='column center'>
            <Route path='/posts/:postId' component={PostContent} />
            <Route path='/tags/:search' render={(props) => {
              return (
                <div>
                  {props.match.params.search}
                </div>
              )
            }} />
          </div>
          <div className='column blue sidebar right'>
            Calendar
          </div>
        </div>
      </div>
    )
  }
}

export default withRouter(PageContainer);
