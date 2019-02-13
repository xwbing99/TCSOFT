using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace TCSOFT.Consul
{
    /// <summary>
    /// Consul配置扩展类
    /// </summary>
    public static class ConsulConfigurationExtensions
    {
        /// <summary>
        /// 添加Consul配置项
        /// </summary>
        /// <param name="configurationBuilder">配置构造器</param>
        /// <param name="consulUrls">Consul地址</param>
        /// <param name="consulPath">配置项路径</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder configurationBuilder
                                                        , IEnumerable<Uri> consulUrls
                                                        , string consulPath)
        {
            return configurationBuilder.Add(new ConsulConfigurationSource(consulUrls, consulPath));
        }

        /// <summary>
        /// 添加Consul配置项
        /// </summary>
        /// <param name="configurationBuilder">配置构造器</param>
        /// <param name="consulUrls">Consul地址</param>
        /// <param name="consulPath">配置项路径</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder configurationBuilder
                                                        , IEnumerable<string> consulUrls
                                                        , string consulPath)
        {
            return configurationBuilder.AddConsul(consulUrls.Select(u => new Uri(u)), consulPath);
        }

        /// <summary>
        /// 更新重注册标志
        /// </summary>
        /// <param name="options">配置项</param>
        public static async System.Threading.Tasks.Task UpdateConsulConfigAsync(IConfiguration configuration, IOptions<ConsulRegisterOptions> options)
        {
            string configString = Newtonsoft.Json.JsonConvert.SerializeObject(options.Value, Newtonsoft.Json.Formatting.Indented);
            
            MemoryStream ms = new MemoryStream();
            //将资料写入MemoryStream
            byte[] bstr = Encoding.Default.GetBytes(configString);
            ms.Write(bstr);

            //一定要在这设定Position
            ms.Position = 0;
            //将MemoryStream转成HttpContent
            HttpContent content = new StreamContent(ms);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();

            //由HttpClient发出Put Method
            HttpResponseMessage response = await client.PutAsync($"{ configuration["ConfigCenter:Uri"] }/v1/kv/{ configuration["ConfigCenter:path"] }", content);
        }
    }
}
