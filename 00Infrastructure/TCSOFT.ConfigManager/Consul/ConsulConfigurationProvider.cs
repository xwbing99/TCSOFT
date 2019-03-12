using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TCSOFT.ConfigManager
{
    /// <summary>
    /// Consul配置器
    /// </summary>
    public class ConsulConfigurationProvider : ConfigurationProvider
    {
        private const string ConsulIndexHeader = "X-Consul-Index";
        private readonly string _path;
        private readonly HttpClient _httpClient;
        private readonly IReadOnlyList<Uri> _consulUrls;
        private readonly Task _configurationListeningTask;
        private int _consulUrlIndex;
        private int _failureCount;
        private int _consulConfigurationIndex;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="consulUrls">Consul配置中心地址</param>
        /// <param name="path">配置项路径</param>
        public ConsulConfigurationProvider(IEnumerable<Uri> consulUrls, string path)
        {
            _path = path;
            _consulUrls = consulUrls.Select(u => new Uri(u, $"v1/kv/{path}")).ToList();
            if (_consulUrls.Count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(consulUrls));
            }

            _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }, true);
            _configurationListeningTask = new Task(ListenToConfigurationChanges);
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <returns></returns>
        private async Task LoadAsync()
        {
            Data = await ExecuteQueryAsync();
            if (_configurationListeningTask.Status == TaskStatus.Created)
                _configurationListeningTask.Start();
        }

        /// <summary>
        /// 配置变更侦听
        /// </summary>
        private async void ListenToConfigurationChanges()
        {
            while (true)
            {
                try
                {
                    if (_failureCount > _consulUrls.Count)
                    {
                        _failureCount = 0;
                        await Task.Delay(TimeSpan.FromMinutes(1));
                    }

                    Data = await ExecuteQueryAsync(true);
                    OnReload();
                    _failureCount = 0;
                }
                catch (TaskCanceledException)
                {
                    _failureCount = 0;
                }
                catch
                {
                    _consulUrlIndex = (_consulUrlIndex + 1) % _consulUrls.Count;
                    _failureCount++;
                }
            }
        }

        /// <summary>
        /// 配置获取
        /// </summary>
        /// <param name="isBlocking">是否阻塞</param>
        /// <returns></returns>
        private async Task<IDictionary<string, string>> ExecuteQueryAsync(bool isBlocking = false)
        {
            var requestUri = isBlocking ? $"?recurse=true&index={_consulConfigurationIndex}" : "?recurse=true";
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_consulUrls[_consulUrlIndex], requestUri)))
            {
                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    if (response.Headers.Contains(ConsulIndexHeader))
                    {
                        var indexValue = response.Headers.GetValues(ConsulIndexHeader).FirstOrDefault();
                        int.TryParse(indexValue, out _consulConfigurationIndex);
                    }
                    var tokens = JToken.Parse(await response.Content.ReadAsStringAsync());
                    return tokens
                                .Select(k => new KeyValuePair<string, JToken>
                                (
                                    k.Value<string>("Key").Substring(0),
                                    k.Value<string>("Value") != null ? JToken.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(k.Value<string>("Value")))) : null
                                ))
                                .Where(v => !string.IsNullOrWhiteSpace(v.Key))
                                .SelectMany(Flatten)
                                .ToDictionary(v => ConfigurationPath.Combine(v.Key.Split('/')), v => v.Value, StringComparer.OrdinalIgnoreCase);
                }
            }
        }

        /// <summary>
        /// 配置扁平化
        /// </summary>
        /// <param name="tuple">树状配置对象</param>
        /// <returns></returns>
        private static IEnumerable<KeyValuePair<string, string>> Flatten(KeyValuePair<string, JToken> tuple)
        {
            if (!(tuple.Value is JObject value))
                yield break;
            foreach (var property in value)
            {
                //$"{tuple.Key}/{property.Key}"
                var propertyKey = $"{tuple.Key}/{property.Key}";
                switch (property.Value.Type)
                {
                    case JTokenType.Object:
                        foreach (var item in Flatten(new KeyValuePair<string, JToken>(propertyKey, property.Value)))
                            yield return item;
                        break;
                    case JTokenType.Array:
                        break;
                    default:
                        yield return new KeyValuePair<string, string>(propertyKey, property.Value.Value<string>());
                        break;
                }
            }
        }
    }
}
