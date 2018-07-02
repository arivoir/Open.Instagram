using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Open.IO;
using Open.Net.Http;
using Open.OAuth2;

namespace Open.Instagram
{
    public class InstagramClient : OAuth2Client
    {
        #region ** fields

        private static string OAUTH2 = "https://instagram.com/oauth/authorize/";
        private string ApiServiceUri = "https://api.instagram.com/v1/";
        private string _accessToken = null;

        public InstagramClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        #endregion

        #region ** authentication

        public static string GetRequestUrl(string clientId, string scope, string callbackUrl)
        {
            return OAuth2Client.GetRequestUrl(OAUTH2, clientId, scope, callbackUrl, response_type: "token");
        }

        public static async Task<OAuth2Token> ExchangeCodeForAccessTokenAsync(string code, string clientId, string clientSecret, string callbackUrl)
        {
            return await OAuth2Client.ExchangeCodeForAccessTokenAsync(OAUTH2, code, clientId, clientSecret, callbackUrl);
        }

        #endregion

        #region ** public methods

        public async Task<User> GetUser(CancellationToken cancellationToken)
        {
            var uri = BuildApiUri("users/self");
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jResponse = await response.Content.ReadJsonAsync<UserResponse>();
                CheckForErrors(jResponse.Meta);
                return jResponse.Data;
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<ItemsResponse> GetMediaAsync(string user = "self", string maxId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(maxId))
            {
                parameters["max_id"] = maxId;
            }
            var uri = BuildApiUri("users/" + user + "/media/recent", parameters);
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jResponse = await response.Content.ReadJsonAsync<ItemsResponse>();
                CheckForErrors(jResponse.Meta);
                return jResponse;
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<Item[]> GetItemsAsync(CancellationToken cancellationToken)
        {
            var uri = BuildApiUri("users/self/feed");
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jResponse = await response.Content.ReadJsonAsync<ItemsResponse>();
                CheckForErrors(jResponse.Meta);
                return jResponse.Data;
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<Stream> DownloadFileAsync(Uri url, CancellationToken cancellationToken)
        {
            var client = CreateClient();
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new StreamWithLength(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentLength);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<Comment[]> GetCommentsAsync(string photoId, CancellationToken cancellationToken)
        {
            var uri = BuildApiUri(string.Format("media/{0}/comments", photoId));
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jResponse = await response.Content.ReadJsonAsync<CommentsResponse>();
                CheckForErrors(jResponse.Meta);
                return jResponse.Data;
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<User[]> GetLikesAsync(string photoId, CancellationToken cancellationToken)
        {
            var uri = BuildApiUri(string.Format("media/{0}/likes", photoId));
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jResponse = await response.Content.ReadJsonAsync<LinksResponse>();
                CheckForErrors(jResponse.Meta);
                return jResponse.Data;
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task AddCommentAsync(string photoId, string message, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                { "text" , message }
            };
            var uri = BuildApiUri(string.Format("media/{0}/comments", photoId), parameters);
            var client = CreateClient();
            var response = await client.PostAsync(uri, new StreamContent(new MemoryStream()), cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task AddLikeAsync(string photoId, CancellationToken cancellationToken)
        {
            var uri = BuildApiUri(string.Format("media/{0}/likes", photoId));
            var client = CreateClient();
            var response = await client.PostAsync(uri, new StreamContent(new MemoryStream()), cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task RemoveLikeAsync(string photoId, CancellationToken cancellationToken)
        {
            var uri = BuildApiUri(string.Format("media/{0}/likes", photoId));
            var client = CreateClient();
            var response = await client.DeleteAsync(uri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response.Content);
            }
        }

        #endregion

        #region ** private stuff

        /// <summary>
        /// Build the API service URI.
        /// </summary>
        /// <param name="path">The relative path requested.</param>
        /// <returns>The request URI.</returns>
        private Uri BuildApiUri(string path, IDictionary<string, string> parameters = null)
        {
            UriBuilder builder = new UriBuilder(ApiServiceUri);
            builder.Path += path;
            builder.Query = (parameters != null && parameters.Count() > 0 ? string.Join("&", parameters.Select(pair => pair.Key + "=" + Uri.EscapeDataString(pair.Value)).ToArray()) + "&" : "") + "access_token=" + Uri.EscapeUriString(this._accessToken);
            return builder.Uri;
        }

        private static HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }

        private void CheckForErrors(Meta meta)
        {
            if (meta == null)
            {
                throw new InstagramException();
            }
            else if (meta.Code != 200)
            {
                throw new InstagramException(meta);
            }
        }

        private async Task<Exception> ProcessException(HttpContent httpContent)
        {
            var error = await httpContent.ReadJsonAsync<InstagramError>();
            return new InstagramException(error.Meta);
        }

        #endregion
    }
}
