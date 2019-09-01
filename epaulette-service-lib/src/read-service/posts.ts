import fetchData from '../fetchData';

interface Post {
  postId: number;
  date: Date;
  typeId: number;
}

function getLatestPost() : Promise<Post> {
  return fetchData('posts/latest');
}

const api = {
  getLatestPost,
};

export { Post, api };
