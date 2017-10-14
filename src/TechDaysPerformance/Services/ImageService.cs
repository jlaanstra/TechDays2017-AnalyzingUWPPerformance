using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TechdaysPerformance.Services
{
    public class ImageService
    {
        public async Task<IEnumerable<string>> GetImagesAsync(string searchString)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Secrets.NothingToSeeHere);
            var response = await client.GetStringAsync($"https://api.cognitive.microsoft.com/bing/v7.0/images/search?q={searchString}&count=150&size=Large");
            var json = JsonConvert.DeserializeObject<RootObject>(response);

            return json.value.Select(x => x.contentUrl);
        }
    }

    public class Instrumentation
    {
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class InsightsMetadata
    {
        public int recipeSourcesCount { get; set; }
    }

    public class Value
    {
        public string webSearchUrl { get; set; }
        public string name { get; set; }
        public string thumbnailUrl { get; set; }
        public DateTime datePublished { get; set; }
        public string contentUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string contentSize { get; set; }
        public string encodingFormat { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string imageInsightsToken { get; set; }
        public InsightsMetadata insightsMetadata { get; set; }
        public string imageId { get; set; }
        public string accentColor { get; set; }
    }

    public class Thumbnail2
    {
        public string thumbnailUrl { get; set; }
    }

    public class QueryExpansion
    {
        public string text { get; set; }
        public string displayText { get; set; }
        public string webSearchUrl { get; set; }
        public string searchLink { get; set; }
        public Thumbnail2 thumbnail { get; set; }
    }

    public class Thumbnail3
    {
        public string thumbnailUrl { get; set; }
    }

    public class Suggestion
    {
        public string text { get; set; }
        public string displayText { get; set; }
        public string webSearchUrl { get; set; }
        public string searchLink { get; set; }
        public Thumbnail3 thumbnail { get; set; }
    }

    public class PivotSuggestion
    {
        public string pivot { get; set; }
        public List<Suggestion> suggestions { get; set; }
    }

    public class Thumbnail4
    {
        public string url { get; set; }
    }

    public class SimilarTerm
    {
        public string text { get; set; }
        public string displayText { get; set; }
        public string webSearchUrl { get; set; }
        public Thumbnail4 thumbnail { get; set; }
    }

    public class Thumbnail5
    {
        public string thumbnailUrl { get; set; }
    }

    public class RelatedSearch
    {
        public string text { get; set; }
        public string displayText { get; set; }
        public string webSearchUrl { get; set; }
        public string searchLink { get; set; }
        public Thumbnail5 thumbnail { get; set; }
    }

    public class RootObject
    {
        public string _type { get; set; }
        public Instrumentation instrumentation { get; set; }
        public string readLink { get; set; }
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public int nextOffset { get; set; }
        public List<Value> value { get; set; }
        public List<QueryExpansion> queryExpansions { get; set; }
        public List<PivotSuggestion> pivotSuggestions { get; set; }
        public List<SimilarTerm> similarTerms { get; set; }
        public List<RelatedSearch> relatedSearches { get; set; }
    }
}
