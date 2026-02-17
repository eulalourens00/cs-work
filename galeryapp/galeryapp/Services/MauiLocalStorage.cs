using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace galeryapp.Services
{
    public class MauiLocalStorage: ILocalStorage
    {
        public const string FavoritePhotosKey = "FavoritesPhotos";
        public void Store(string filename)
        {
            var filenames = Get();
            filenames.Add(filename);

            var json = JsonSerializer.Serialize(filenames);

            Preferences.Set(FavoritePhotosKey, json);
        }

        public List<string> Get()
        {
            if (Preferences.ContainsKey(FavoritePhotosKey)) { 
                var filenames = Preferences.Get(FavoritePhotosKey, string.Empty);
                return JsonSerializer.Deserialize<List<string>>(filenames);
            }
            return new List<string>();
        }

        
    }
}
