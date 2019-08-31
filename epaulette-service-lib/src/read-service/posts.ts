const apiUrl = "https://localhost:5001/"

interface Post {
  postId: Number;
  date: Date;
  typeId: Number;
}

function fetchData(url : string, options? : object) : Promise<Post> {
  return fetch(url, options).then(data => data.json());
}

const api = {
  getLatestPost: function() : Promise<Post> {
    return fetchData(apiUrl + "posts/latest")
  }
}

export { Post, api }
