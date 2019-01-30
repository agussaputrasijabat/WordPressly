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
    public class Media
    {
        private const string _methodPath = "wp-json/wp/v2/media";
        private readonly static string _cachedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "_mediaitem.json");
        internal static List<MediaItem> LocalMediaItems = new List<MediaItem>();

        public Media()
        {
            if (File.Exists(_cachedPath))
                LocalMediaItems = JsonConvert.DeserializeObject<List<MediaItem>>(File.ReadAllText(_cachedPath));
        }

        public async Task<IEnumerable<MediaItem>> Query(MediaQueryBuilder QueryBuilder)
        {
            return await HttpHelper.GetRequests<IEnumerable<MediaItem>>(_methodPath + "/" + QueryBuilder.BuildQueryURL());
        }

        public async Task<IEnumerable<MediaItem>> ListAsync(int Page = 1, int PerPage = 20, string Filter = "", int[] Include = null, DataSource DataSource = DataSource.Cloud)
        {
            IEnumerable<MediaItem> mediaitems = null;
            Filter = Filter.ToLower();

            if (DataSource == DataSource.Cloud)
            {
                MediaQueryBuilder QueryBuilder = new MediaQueryBuilder { Page = Page, PerPage = PerPage, Search = Filter.ToLower(), Include = Include };

                mediaitems = await Query(QueryBuilder);

                if (mediaitems != null)
                {
                    foreach (var mediaitem in mediaitems)
                    {
                        var LocalMedia = LocalMediaItems.FirstOrDefault(p => p.Id == mediaitem.Id);
                        if (LocalMedia is MediaItem)
                        {
                            LocalMediaItems.Remove(LocalMedia);
                            LocalMediaItems.Add(mediaitem);
                        }
                        else
                        {
                            LocalMediaItems.Add(mediaitem);
                        }
                    }

                    try { File.WriteAllText(_cachedPath, JsonConvert.SerializeObject(LocalMediaItems)); } catch { }
                }
            }
            else
            {
                mediaitems = LocalMediaItems.AsEnumerable();
                if (Include != null) mediaitems = mediaitems.Where(u => Include.Any(x => x == u.Id));
                if (!string.IsNullOrEmpty(Filter)) mediaitems = mediaitems.Where(m => m.Title.Rendered.ToLower().Contains(Filter) || m.SourceUrl.ToLower().Contains(Filter));
                mediaitems = mediaitems.Skip((Page - 1) * PerPage).Take(PerPage);
            }

            return mediaitems;
        }

        public async Task<MediaItem> GetById(int Id, DataSource DataSource = DataSource.Cloud)
        {
            MediaItem Media = null;
            if (DataSource == DataSource.Cloud)
            {
                Media = await HttpHelper.GetRequests<MediaItem>(_methodPath + "/" + Id);
                if (Media is MediaItem)
                {
                    var MediaItem = LocalMediaItems.FirstOrDefault(p => p.Id == Id);
                    if (MediaItem is MediaItem)
                    {
                        LocalMediaItems.Remove(MediaItem);
                        LocalMediaItems.Add(Media);
                    }
                    else LocalMediaItems.Add(Media);
                }
            }
            else
            {
                Media = LocalMediaItems.FirstOrDefault(p => p.Id == Id);
            }

            return Media;
        }

        public void DeleteLocalCache()
        {
            if (File.Exists(_cachedPath))
                File.Delete(_cachedPath);

            LocalMediaItems = new List<MediaItem>();
        }
    }
}
