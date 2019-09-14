import {
  Post as PostsApiPost,
  PostNeighbors as PostsApiPostNeighbors,
  api as PostsApi,
} from './src/read-service/posts';
import {
  Tag as TagsApiTag,
  Post as TagsApiPost,
  TagCounts as TagsApiTagCounts,
  TagSearch as TagsApiTagSearch,
  api as TagsApi,
} from './src/read-service/tags';

export {
  PostsApiPost,
  PostsApiPostNeighbors,
  PostsApi,
  TagsApiTag,
  TagsApiPost,
  TagsApiTagCounts,
  TagsApiTagSearch,
  TagsApi,
};
