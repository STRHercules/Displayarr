using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Displayarr
{
    public class PlexClient
    {
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly HttpClient _http;

        public PlexClient(string baseUrl, string token)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _token = token;
            _http = new HttpClient();
        }

        public async Task<List<string>> GetRecentlyAddedPosterUrlsAsync()
        {
            var url = $"{_baseUrl}/library/recentlyAdded?X-Plex-Token={_token}";
            var stream = await _http.GetStreamAsync(url);
            var xml = XDocument.Load(stream);

            var urls = new List<string>();
            foreach (var elem in xml.Descendants("Video"))
            {
                var thumb = elem.Attribute("thumb")?.Value;
                if (thumb != null)
                {
                    urls.Add($"{_baseUrl}{thumb}?X-Plex-Token={_token}");
                }
            }
            return urls;
        }
    }
}
