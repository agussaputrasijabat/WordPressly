using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

using WordPressly.Utility;
using WordPressly.Models;
using WordPressly.Abstractions;

namespace WordPressly.Repository
{
    public class Posts
    {
        private const string _methodPath = "wp/v2/posts";
        private readonly static string _cachedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "_posts.json");

        internal static List<Post> LocalPosts = new List<Post>();

        public Posts()
        {
            if (File.Exists(_cachedPath))
                LocalPosts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(_cachedPath));
        }

        public async Task<IEnumerable<Post>> Query(PostsQueryBuilder QueryBuilder)
        {
            return await HttpHelper.GetRequests<IEnumerable<Post>>(_methodPath + "/" + QueryBuilder.BuildQueryURL());
        }

        public async Task<IEnumerable<Post>> ListAsync(int Page = 1, int PerPage = 20, string Filter = "", int[] Categories = null, DataSource DataSource = DataSource.Cloud)
        {
            IEnumerable<Post> posts = null;
            Filter = Filter.ToLower();

            if (DataSource == DataSource.Cloud)
            {
                PostsQueryBuilder QueryBuilder = new PostsQueryBuilder { Page = Page, PerPage = PerPage, Search = Filter.ToLower(), Categories = Categories };

                posts = await Query(QueryBuilder);

                if (posts != null)
                {
                    foreach (var post in posts)
                    {
                        var LocalPost = LocalPosts.FirstOrDefault(p => p.Id == post.Id);
                        if (LocalPost is Post)
                        {
                            LocalPosts.Remove(LocalPost);
                            LocalPosts.Add(post);
                        }
                        else
                        {
                            LocalPosts.Add(post);
                        }
                    }

                    File.WriteAllText(_cachedPath, JsonConvert.SerializeObject(LocalPosts));
                }
            }
            else
            {
                posts = LocalPosts.OrderByDescending(pos => pos.Date).AsEnumerable();
                if (Categories != null) posts = posts.Where(u => u.Categories.Contains(Categories.FirstOrDefault()));
                if (!string.IsNullOrEmpty(Filter)) posts = posts.Where(pos => pos.Title.Rendered.ToLower().Contains(Filter) || pos.Content.Rendered.ToLower().Contains(Filter));
                posts = posts.Skip((Page - 1) * PerPage).Take(PerPage);
            }

            return posts;
        }

        public async Task<Post> GetById(int Id, DataSource DataSource = DataSource.Cloud)
        {
            Post Post = null;
            if (DataSource == DataSource.Cloud)
            {
                Post = await HttpHelper.GetRequests<Post>(_methodPath + "/" + Id);
                if (Post is Post)
                {
                    var LocalPost = LocalPosts.FirstOrDefault(p => p.Id == Post.Id);
                    if (LocalPost is Post)
                    {
                        LocalPosts.Remove(LocalPost);
                        LocalPosts.Add(Post);
                    }
                    else LocalPosts.Add(Post);
                }
            }
            else
            {
                Post = LocalPosts.FirstOrDefault(p => p.Id == Id);
            }

            return Post;
        }

        public void DeleteLocalCache()
        {
            if (File.Exists(_cachedPath))
                File.Delete(_cachedPath);

            LocalPosts = new List<Post>();
        }
    }
}
