import fetchData from '../fetchData';

interface Post {
  postId: number;
  date: Date;
  typeId: number;
}

interface PostNeighbors {
  current: Post | null;
  next: Post | null;
  prev: Post | null;
}

function getLatestPost() : Promise<PostNeighbors> {
  return fetchData('posts/latest');
}

function getPost(postId : number) : Promise<PostNeighbors> {
  return fetchData(`posts/post/${postId}`);
}

const api = {
  getLatestPost,
  getPost
};

export { Post, PostNeighbors, api };
