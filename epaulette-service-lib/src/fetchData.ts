const apiUrl = 'https://localhost:5001/';

export default function fetchData<T>(path : string, options? : object) : Promise<T> {
  const url : string = `${apiUrl}${path}`;
  const promise : Promise<T> = new Promise((resolve) => {
    fetch(url, options).then((data) => {
      data.json().then((jsonObject) => {
        resolve(jsonObject as T);
      });
    });
  });

  return promise;
}
