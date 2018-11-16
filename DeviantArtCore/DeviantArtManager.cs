using DeviantArtCore.Authentification;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DeviantArtCore
{
    public class DeviantArtManager : IDeviantArtManager
    {
        #region Constants
        private const string ClientIdParameter = "client_id";
        private const int ClientId = 8817;
        private const string ClientSecretParameter = "client_secret";
        private const string ClientSecret = "15fdbb7a1383bc12f5f0a8bf9e8aa09a";
        private const string RedirectUriParameter = "redirect_uri";
        private const string RedirectUri = "http://fav.me/dcqpqz5";
        private const string GrantTypeParameter = "grant_type";
        private const string GrantType = "client_credentials";
        private const string ResponseTypeParameter = "response_type";
        private const string ResponseType = "code";
        private const string AccessTokenParameter = "access_token";
        private const string LimitParameter = "limit";
        private const int Limit = 20;
        private const string OffsetParameter = "offset";
        private const string TimeRangeParameter = "timerange";
        private const string TimeRangeAllTime = "alltime";
        private const string TimeRangeOneMonth = "1month";
        private const string TimeRange8hr = "8hr";
        private const string TimeRange24hr = "24hr";
        private const string TimeRange3days = "3days";
        private const string TimeRangeOneWeek = "1week";
        private const int TokenPeriodLife = 3600;
        private const string SuccessApiStutus = "success";

        private const string AuthorizeUri = "https://www.deviantart.com/oauth2/authorize";
        private const string AccessTokenUri = "https://www.deviantart.com/oauth2/token";
        private const string HotDeviantItemsUri = "https://www.deviantart.com/api/v1/oauth2/browse/hot";
        private const string NewestDeviantItemsUri = "https://www.deviantart.com/api/v1/oauth2/browse/newest";
        private const string UndiscoveredItemsUri = "https://www.deviantart.com/api/v1/oauth2/browse/undiscovered";
        private const string DailyDeviationsItemsUri = "https://www.deviantart.com/api/v1/oauth2/browse/dailydeviations";
        private const string PopularItemsUri = "https://www.deviantart.com/api/v1/oauth2/browse/popular";
        private const string PlaceboUri = "https://www.deviantart.com/api/v1/oauth2/placebo";
        #endregion

        private string DownloadDeviationUri = "https://www.deviantart.com/api/v1/oauth2/deviation/download/";
        private int _clientId;
        private string _clientSecreteKey;
        private HttpClient client;
        private PublicAccessToken _pat;

        public DeviantArtManager()
        {

        }

        public DeviantArtManager(string clientSecreteKey, int clientId)
        {
            _clientSecreteKey = clientSecreteKey;
            _clientId = clientId;
        }

        public async Task<bool> CheckApiStatus()
        {
            var returnValue = false;

            UriBuilder builder = new UriBuilder(PlaceboUri)
            {
                Query = $"mature_content={true}"
            };

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);

                var deserializedStatus = DeserializeApiResponce<ApiStatus>(response);

                if (deserializedStatus.Status == SuccessApiStutus)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        public async Task GetAccessTokenAsync(int clientId, string clientSecreteKey)
        {
            UriBuilder builder = new UriBuilder(AccessTokenUri)
            {
                Query = $"{GrantTypeParameter}={GrantType}&{ClientIdParameter}={clientId}&{ClientSecretParameter}={clientSecreteKey}"
            };

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);
                var deserializedToken = DeserializeApiResponce<PublicAccessToken>(response);
                _pat = deserializedToken;
            }
        }

        public async Task<DownloadableImage> GetImageSourceToDownloadAsync(string deviationId)
        {
            DownloadDeviationUri += deviationId;
            UriBuilder builder = new UriBuilder(DownloadDeviationUri)
            {
                Query = $"mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}"
            };

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);
                var deserializedImage = DeserializeApiResponce<DownloadableImage>(response);
                return deserializedImage;
            }
        }

        public async Task<DeviantItemCollection> GetHotestItemsAsync(int offset)
        {
            await CheckPublicAccessToken();

            UriBuilder builder = new UriBuilder(HotDeviantItemsUri)
            {
                Query = $"{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}"
            };

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);
                var deserializedCollection = DeserializeApiResponce<DeviantItemCollection>(response);
                return deserializedCollection;
            }
        }

        public async Task<DeviantItemCollection> GetNewestItemsAsync(int offset)
        {
            await CheckPublicAccessToken();

            UriBuilder builder = new UriBuilder(NewestDeviantItemsUri)
            {
                Query = $"{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}"
            };

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);
                var deserializedCollection = DeserializeApiResponce<DeviantItemCollection>(response);
                return deserializedCollection;
            }
        }

        public async Task<DeviantItemCollection> GetUndiscoveredItemsAsync(int offset)
        {
            await CheckPublicAccessToken();

            UriBuilder builder = new UriBuilder(UndiscoveredItemsUri)
            {
                Query = $"{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}"
            };

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);
                var deserializedCollection = DeserializeApiResponce<DeviantItemCollection>(response);
                return deserializedCollection;
            }
        }

        public async Task<DeviantItemCollection> GetDailyDeviationsItemsAsync()
        {
            await CheckPublicAccessToken();

            UriBuilder builder = new UriBuilder(DailyDeviationsItemsUri)
            {
                Query = $"mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}"
            };

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);
                var deserializedCollection = DeserializeApiResponce<DeviantItemCollection>(response);
                return deserializedCollection;
            }
        }

        public async Task<DeviantItemCollection> GetPopularItemsAsync(PopularTimeRange timeRange, int offset)
        {
            await CheckPublicAccessToken();

            UriBuilder builder = new UriBuilder(PopularItemsUri);

            switch (timeRange)
            {
                case PopularTimeRange.hr8:
                    builder.Query = $"{TimeRangeParameter}={TimeRange8hr}&{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}";
                    break;
                case PopularTimeRange.hr24:
                    builder.Query = $"{TimeRangeParameter}={TimeRange24hr}&{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}";
                    break;
                case PopularTimeRange.days3:
                    builder.Query = $"{TimeRangeParameter}={TimeRange3days}&{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}";
                    break;
                case PopularTimeRange.oneMonth:
                    builder.Query = $"{TimeRangeParameter}={TimeRangeOneMonth}&{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}";
                    break;
                case PopularTimeRange.oneWeek:
                    builder.Query = $"{TimeRangeParameter}={TimeRangeOneWeek}&{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}";
                    break;
                default:
                    builder.Query = $"{TimeRangeParameter}={TimeRangeAllTime}&{OffsetParameter}={offset}&{LimitParameter}={Limit}&mature_content={true}&{AccessTokenParameter}={_pat.AccessToken}";
                    break;
            }

            using (client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.Uri);
                var deserializedCollection = DeserializeApiResponce<DeviantItemCollection>(response);
                return deserializedCollection;
            }
        }

        private async Task CheckPublicAccessToken()
        {
            if (_pat == null)
            {
                if (_clientId != 0 && !String.IsNullOrEmpty(_clientSecreteKey))
                    await GetAccessTokenAsync(_clientId, _clientSecreteKey);
                await GetAccessTokenAsync(ClientId, ClientSecret);
            }
        }

        private T DeserializeApiResponce<T>(string responce) where T : class
        {
            T deserializedObject;

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(responce)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                deserializedObject = ser.ReadObject(ms) as T;
                return deserializedObject;
            }
        }
    }
}
