using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Halbot.Controllers;
using Halbot.Data.Models;
using Halbot.Data.Records;
using Newtonsoft.Json;

namespace Halbot.BusinessLayer.GarminConnect
{
    public class GarminContext
    {
        public int Attempts { get; set; } = 1;

        private const int DelayAfterFailAuth = 300;
        private readonly HttpClient _httpClient;
        private readonly BasicAuthParameters _authParameters;
        private readonly Regex _csrfRegex = new Regex(@"name=""_csrf""\s+value=""(\w+)""", RegexOptions.Compiled);
        private readonly Regex _responseUrlRegex = new Regex(@"""(https:[^""]+?ticket=[^""]+)""", RegexOptions.Compiled);
        private readonly string _activitiesUrl = "/proxy/activitylist-service/activities/search/activities";

        public GarminContext(string userName, string password)
        {
            _httpClient = new HttpClient();
            _authParameters = new BasicAuthParameters(userName, password);
        }

        public bool TryGetNewActivities(DateTime startDate, DateTime endDate, string type, out List<ActivityRecord> records)
        {
            var json = GetJsonAsync(startDate, endDate, type).Result;
            var activities = JsonConvert.DeserializeObject<List<FlatGarminJson>>(json);

            if (activities != null)
            {
                records = activities.Select(a => new ActivityRecord
                {
                    Id = a.ActivityId,    // crash if there was no value
                    DataType = ActivityDataType.FlatGarmin,
                    SerializedData = JsonConvert.SerializeObject(a) // this is wonky,  
                }).ToList();

                return true;
            }

            records = new List<ActivityRecord>();
            return false;
        }

        public async Task<string> GetJsonAsync(DateTime startDate, DateTime endDate, string type)
        {
            var start = 0;
            var limit = 20;

            string activitySlug = !string.IsNullOrEmpty(type) ? "&activityType=" + type : "";
            var queryString  = $"?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}&start={start}&limit={limit}{activitySlug}";
            var response = await MakeHttpRequest($"{_activitiesUrl}{queryString}", HttpMethod.Get);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> MakeHttpRequest(string url, HttpMethod method, HttpContent? content = null)
        {
            var force = false;
            Exception? exception = null;

            for (var i = 0; i < Attempts; i++)
            {
                try
                {
                    await ReLoginIfExpired(force);

                    var httpRequestMessage = new HttpRequestMessage(method, $"{_authParameters.BaseUrl}{url}");
                    httpRequestMessage.Headers.Add("Cookie", _authParameters.Cookies);
                    httpRequestMessage.Content = content;

                    var response = await _httpClient.SendAsync(httpRequestMessage);

                    RaiseForStatus(response);

                    return response;
                }
                catch (GarminConnectRequestException ex)
                {
                    exception = ex;
                    if (ex.Status == HttpStatusCode.Forbidden)
                    {
                        await Task.Delay(DelayAfterFailAuth);
                        force = true;
                        continue;
                    }

                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }

            throw new GarminConnectAuthenticationException($"Authentication failed after {Attempts} attempts", exception);
        }

        private async Task ReLoginIfExpired(bool force = false)
        {
            if (force || _authParameters.NeedReLogin)
            {
                _authParameters.Cookies = await Login();
            }
        }

        private async Task<string> Login()
        {
            var (authUrl, cookies) = await GetAuthCookies();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, authUrl);

            httpRequestMessage.Headers.Add("Cookie", cookies);
            var response = await _httpClient.SendAsync(httpRequestMessage);

            RaiseForStatus(response);

            var html = await response.Content.ReadAsStringAsync();

            return cookies;
        }

        private async Task<(string authUrl, string cookies)> GetAuthCookies()
        {
            var queryParams = _authParameters.GetQueryParameters();
            var formParams = _authParameters.GetFormParameters();
            var headers = _authParameters.GetHeaders();

            var queryString = HttpUtility.ParseQueryString("");
            foreach (var (key, value) in queryParams)
            {
                queryString.Add(key, value);
            }

            var signinUrl = $"{_authParameters.SigninUrl}?{queryString}";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, signinUrl);
            foreach (var (key, value) in headers)
            {
                httpRequestMessage.Headers.Add(key, value);
            }

            var responseMessage = await _httpClient.SendAsync(httpRequestMessage);
            RaiseForStatus(responseMessage);

            var htmlAuth = await responseMessage.Content.ReadAsStringAsync();
            var csrf = _csrfRegex.Match(htmlAuth).Groups[1].Value;

            httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, signinUrl);
            foreach (var (key, value) in headers)
            {
                httpRequestMessage.Headers.Add(key, value);
            }

            httpRequestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string>(formParams) { { "_csrf", csrf } });
            var response = await _httpClient.SendAsync(httpRequestMessage);
            RaiseForStatus(response);

            var html = await response.Content.ReadAsStringAsync();

            var responseUrlMatch = _responseUrlRegex.Match(html);
            if (!responseUrlMatch.Success)
            {
                throw new GarminConnectAuthenticationException();
            }

            var responseUrl = responseUrlMatch.Groups[1].Value.Replace("\\", string.Empty);

            var cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            var sb = new StringBuilder();
            foreach (var cookie in cookies)
            {
                sb.Append($"{cookie};");
            }

            return (responseUrl, sb.ToString());
        }

        private static void RaiseForStatus(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.TooManyRequests:
                    throw new GarminConnectTooManyRequestsException();
                case HttpStatusCode.NoContent:
                case HttpStatusCode.OK:
                    return;
                default:
                    {
                        var message = $"{response.RequestMessage?.Method.Method}: {response.RequestMessage?.RequestUri}";
                        throw new GarminConnectRequestException(message, response.StatusCode);
                    }
            }
        }
    }
}