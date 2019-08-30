const apiUrl = "https://localhost:5001/"

const fetchData = (url, options) => fetch(url, options).then(data => data.json())

const api = {
  getLatestPost: function() {
    return fetchData(apiUrl + "posts/latest")
  }
}

export default api
