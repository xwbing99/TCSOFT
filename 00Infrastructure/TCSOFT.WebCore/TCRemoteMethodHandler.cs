using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TCSOFT.WebCore
{
    public static class TCRemoteMethodHandler
    {
        /// <summary>
        /// API调用 GET方法(WebRequest方式）
        /// </summary>
        /// <param name="url">API地址</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// API调用 PUT方法(WebRequest方式）
        /// </summary>
        /// <param name="url">API地址</param>
        /// <param name="body">数据</param>
        /// <returns></returns>
        public static string HttpPut(string url, string body)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            byte[] buffer = encoding.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// API调用 POST方法(WebRequest方式）
        /// </summary>
        /// <param name="url">API地址</param>
        /// <param name="body">数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, string body)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            byte[] buffer = encoding.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
