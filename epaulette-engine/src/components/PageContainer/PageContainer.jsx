import React from 'react'
import './PageContainer.scss'
import '../PostContent/PostContent'
import PostContent from '../PostContent/PostContent';

class PageContainer extends React.Component
{
  constructor(props) {
    super(props)
  }

  render() {
    return (
      <div className="page-container">
        <div className='header blue'>
          <h1>
            Epaulette
          </h1>
        </div>
        <div>
          <div className='column blue sidebar left'>
            Tag Cloud
          </div>
          <div className='column center'>
            <PostContent />
          </div>
          <div className='column blue sidebar right'>
            Calendar
          </div>
        </div>
      </div>
    )
  }
}

export default PageContainer;
