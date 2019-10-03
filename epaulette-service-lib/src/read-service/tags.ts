import fetchData from '../fetchData';

interface Tag {
  tagId: number;
  name: string;
}

interface Post {
  postId: number;
  date: Date;
  typeId: number;
}

interface TagCounts {
  tag: Tag;
  count: number;
}

interface TagSearch {
  post: Post;
  title: string;
  contentBlurg: string;
}

function getTagCounts() : Promise<TagCounts[]> {
  return fetchData('tags/counts');
}

function getTagSearchById(tagId : number) : Promise<TagSearch[]> {
  return fetchData(`tags/search/id/${tagId}`);
}

function getTagSearchByName(tagName : string) : Promise<TagSearch[]> {
  return fetchData(`tags/search/name/${tagName}`);
}

const api = {
  getTagCounts,
  getTagSearchById,
  getTagSearchByName,
};

export { Tag, Post, TagCounts, TagSearch, api };
