using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace News
{
    public class Source
    {
        [JsonPropertyName("id")]

        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Article
    {
        [JsonPropertyName("source")]
        public Source Source { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("url")]
        public string UrlToImage { get; set; }

        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

    }

    public class NewsResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("totalResults")]
        public string TotalResults { get; set; }

        [JsonPropertyName("artucles")]
        public List<Article> Articles { get; set; }


    }
}
