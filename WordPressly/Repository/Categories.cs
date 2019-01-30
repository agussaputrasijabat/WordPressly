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
    public class Categories
    {
        private const string _methodPath = "wp-json/wp/v2/categories";
        private readonly static string _cachedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "_categories.json");

        internal static List<Category> LocalCategories = new List<Category>();

        public Categories()
        {
            if (File.Exists(_cachedPath))
                LocalCategories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(_cachedPath));
        }

        public async Task<IEnumerable<Category>> Query(CategoriesQueryBuilder QueryBuilder)
        {
            return await HttpHelper.GetRequests<IEnumerable<Category>>(_methodPath + "/" + QueryBuilder.BuildQueryURL());
        }

        public Category GetFromLocalById(int Id)
        {
            return LocalCategories.FirstOrDefault(c => c.Id == Id);
        }

        public async Task<Category> GetById(int Id)
        {
            return await HttpHelper.GetRequests<Category>(_methodPath + "/" + Id);
        }

        public async Task<IEnumerable<Category>> ListAsync(int Page = 1, int PerPage = 20, string Filter = "", DataSource DataSource = DataSource.Cloud)
        {
            IEnumerable<Category> categories = null;
            Filter = Filter.ToLower();

            if (DataSource == DataSource.Cloud)
            {
                CategoriesQueryBuilder QueryBuilder = new CategoriesQueryBuilder { Page = Page, PerPage = PerPage, Search = Filter.ToLower() };
                
                categories = await Query(QueryBuilder);

                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        var LocalCategory = LocalCategories.FirstOrDefault(c => c.Id == category.Id);
                        if (LocalCategory is Category)
                        {
                            LocalCategories.Remove(LocalCategory);
                            LocalCategories.Add(category);
                        }
                        else
                        {
                            LocalCategories.Add(category);
                        }
                    }

                    File.WriteAllText(_cachedPath, JsonConvert.SerializeObject(LocalCategories));
                }
            }
            else
            {
                categories = LocalCategories.AsEnumerable();

                if (!string.IsNullOrEmpty(Filter)) categories = categories.Where(cat => cat.Name.ToLower().Contains(Filter) || cat.Description.ToLower().Contains(Filter));
                categories = categories.Skip((Page - 1) * PerPage).Take(PerPage);
            }

            return categories;
        }

        public void DeleteLocalCache()
        {
            if (File.Exists(_cachedPath))
                File.Delete(_cachedPath);

            LocalCategories = new List<Category>();
        }
    }
}
