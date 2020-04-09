using System.Net.Http;

namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// 客户端携带数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpClientData<T>
    {
        public HttpClientData(HttpClient httpClient, T content)
        {
            HttpClient = httpClient;
            Content = content;
        }
        public HttpClient HttpClient { get; set; }
        public T Content { get; set; }
        public static implicit operator HttpClient(HttpClientData<T> data)
        {
            return data.HttpClient;
        }
        public static implicit operator T(HttpClientData<T> data)
        {
            return data.Content;
        }
    }
}
