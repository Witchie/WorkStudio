using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net;

namespace GreenWhale.BootLoader.Extensions.Nuget
{
    /// <summary>
    /// HttpClient 扩展
    /// </summary>
    public static class HttpClientExtension
    {
        /// <summary>
        /// 获取JSON对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static async Task<T> GetJson<T>(this HttpClient httpClient, Uri uri)
        {
           return await httpClient.GetJson<T>(uri.ToString());
        }
        /// <summary>
        /// 获取JSON对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="requestUrl">请求的地址</param>
        /// <returns></returns>
        private static async Task<T> GetJson<T>(this HttpClient httpClient,string requestUrl)
        {
            if (string.IsNullOrWhiteSpace(requestUrl))
            {
                throw new ArgumentException("message", nameof(requestUrl));
            }

            var content = await httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<T>(content);
        }
        /// <summary>
        /// 获取最新发布的NUGET工具包
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestFullUrl">要请求的服务器地址</param>
        /// <returns></returns>
        public static async Task<HttpClientData<NugetTool[]>> WithGetTools(this HttpClient httpClient,string requestFullUrl= "https://dist.nuget.org/tools.json")
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(requestFullUrl))
            {
                throw new ArgumentException("message", nameof(requestFullUrl));
            }

            var res= await  httpClient.GetJson<NugetTools>(requestFullUrl);
            return new HttpClientData<NugetTool[]>(httpClient, res);
        }
        /// <summary>
        /// 合并多个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static HttpClientData<T> With<T>(HttpClient httpClient, T Content)
        {
            return new HttpClientData<T>(httpClient, Content);
        }
        /// <summary>
        /// 确定目录索引 URL 
        /// <![CDATA[https://docs.microsoft.com/zh-cn/nuget/guides/api/query-for-all-published-packages]]>
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestFullUrl"></param>
        /// <returns></returns>
        private static async Task<ServiceList> GetIndexList(this HttpClient httpClient, string requestFullUrl = "https://api.nuget.org/v3/index.json")
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(requestFullUrl))
            {
                throw new ArgumentException("message", nameof(requestFullUrl));
            }

            var res = await httpClient.GetJson<ServiceList>(requestFullUrl);
            return res;
        }
        /// <summary>
        /// 确定目录索引 URL 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestFullUrl"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<ServiceList>> WithIndexList(this HttpClient httpClient, string requestFullUrl = "https://api.nuget.org/v3/index.json")
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(requestFullUrl))
            {
                throw new ArgumentException("message", nameof(requestFullUrl));
            }

            var res= await  httpClient.GetIndexList(requestFullUrl);
            return new HttpClientData<ServiceList>(httpClient,res);
        }
        /// <summary>
        /// 查询包
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="queryContent"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="prerelease"></param>
        /// <param name="nugetVersion"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<NugetPackets>> WithQueryPacket(this Task<HttpClientData<ServiceList>> httpClientData, string queryContent = null, int? skip = null, int? take = 10, bool? prerelease = false, string nugetVersion = "2.0.0")
        {
            if (httpClientData is null)
            {
                throw new ArgumentNullException(nameof(httpClientData));
            }

            if (string.IsNullOrEmpty(queryContent))
            {
                throw new ArgumentException("message", nameof(queryContent));
            }

            if (skip is null)
            {
                throw new ArgumentNullException(nameof(skip));
            }

            if (take is null)
            {
                throw new ArgumentNullException(nameof(take));
            }

            if (prerelease is null)
            {
                throw new ArgumentNullException(nameof(prerelease));
            }

            if (string.IsNullOrEmpty(nugetVersion))
            {
                throw new ArgumentException("message", nameof(nugetVersion));
            }

            var client = await httpClientData;
            var content =await client.HttpClient.QueryPacket(client.Content, queryContent, skip, take, prerelease, nugetVersion);
            return new HttpClientData<NugetPackets>(client, content);
        }
        /// <summary>
        /// 查询包版本
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<ContentVersions>> WithPakckageVersion(this Task<HttpClientData<ServiceList>> httpClientData, string id)
        {
            if (httpClientData is null)
            {
                throw new ArgumentNullException(nameof(httpClientData));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            var client = await httpClientData;
            var res=await client.HttpClient.PakckageVersion(id, client);
            return new HttpClientData<ContentVersions>(client, res);
        }
        /// <summary>
        /// 保存包
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<HttpClient> OnSavePackage(this Task<HttpClientData<Stream>> httpClientData,string fileName,string directory)
        {
            if (httpClientData is null)
            {
                throw new ArgumentNullException(nameof(httpClientData));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }

            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("message", nameof(directory));
            }

            var data = await httpClientData;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var path = Path.Combine(directory, fileName);
            using (FileStream file=File.Create(path))
            {
                 await  data.Content.CopyToAsync(file);
                file.Close();
            }
            return data;
        }
        /// <summary>
        /// 保存清单文件
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="fileName"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static async Task<HttpClient> OnSaveManifest(this Task<HttpClientData<string>> httpClientData, string fileName, string directory)
        {
            if (httpClientData is null)
            {
                throw new ArgumentNullException(nameof(httpClientData));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }

            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("message", nameof(directory));
            }

            if (!fileName.ToLower().EndsWith("nuspec"))
            {
                throw new ArgumentException($"文件{fileName}必须以.nuspec结尾");
            }
            var cleint = await httpClientData;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var path = Path.Combine(directory, fileName);
            File.WriteAllText(path,cleint);
            return cleint;
        }
        /// <summary>
        /// 查询包
        /// </summary>
        /// <param name="queryContent"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="prerelease"></param>
        /// <param name="nugetVersion"></param>
        /// <returns></returns>
        private static async Task<NugetPackets> QueryPacket(this HttpClient httpClient, string requestFullUrl= "https://azuresearch-usnc.nuget.org/query", string queryContent=null,int? skip=null,int? take=10,bool? prerelease=false,string nugetVersion="2.0.0")
        {
            Hashtable hashtable = new System.Collections.Hashtable();
            if (queryContent!=null)
            {
                hashtable.Add("q", queryContent);
            }
            if (skip != null)
            {
                hashtable.Add("skip", skip);
            }
            if (take != null)
            {
                hashtable.Add("take", take);
            }
            if (prerelease!=null)
            {
                hashtable.Add("prerelease", prerelease==true?"true":"false");
            }
            if (nugetVersion!=null)
            {
                hashtable.Add("semVerLevel", nugetVersion);
            }
            var path = requestFullUrl + hashtable.ToParameter();
            var res = await httpClient.GetJson<NugetPackets>(path);
            return res;
        }
        /// <summary>
        /// 查询包信息
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="serviceList"></param>
        /// <param name="queryContent"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="prerelease"></param>
        /// <param name="nugetVersion"></param>
        /// <returns></returns>
        private static async Task<NugetPackets> QueryPacket(this HttpClient httpClient, ServiceList serviceList, string queryContent = null, int? skip = null, int? take = 10, bool? prerelease = false, string nugetVersion = "2.0.0")
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (serviceList is null)
            {
                throw new ArgumentNullException(nameof(serviceList));
            }

            if (string.IsNullOrEmpty(queryContent))
            {
                throw new ArgumentException("message", nameof(queryContent));
            }

            if (skip is null)
            {
                throw new ArgumentNullException(nameof(skip));
            }

            if (take is null)
            {
                throw new ArgumentNullException(nameof(take));
            }

            if (prerelease is null)
            {
                throw new ArgumentNullException(nameof(prerelease));
            }

            if (string.IsNullOrEmpty(nugetVersion))
            {
                throw new ArgumentException("message", nameof(nugetVersion));
            }

            var packages = serviceList["SearchQueryService"];
            if (packages!=null)
            {
                var list = await httpClient.QueryPacket(packages.ID, queryContent, skip, take, prerelease, nugetVersion);
                return list;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 包内容
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestFullUrl"></param>
        /// <returns></returns>
        private static async Task<ContentVersions> PakckageVersion(this HttpClient httpClient,string id, string requestFullUrl= "https://api.nuget.org/v3-flatcontainer/")
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(requestFullUrl))
            {
                throw new ArgumentException("message", nameof(requestFullUrl));
            }

            var path=new Uri($"{requestFullUrl}/{id.ToLowerInvariant()}/index.json");
            var res=   await httpClient.GetJson<ContentVersions>(path);
            return res;
        }
        /// <summary>
        /// 包版本
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="id"></param>
        /// <param name="serviceList"></param>
        /// <returns></returns>
        private static async Task<ContentVersions> PakckageVersion(this HttpClient httpClient, string id, ServiceList serviceList)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (serviceList is null)
            {
                throw new ArgumentNullException(nameof(serviceList));
            }

            var packages = serviceList["PackageBaseAddress"];
            var res = await httpClient.PakckageVersion(id, packages.ID);
            return res;
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<Stream>> WithDownLoadPackage(this Task<HttpClientData<ServiceList>> httpClientData, string id, string version)
        {
            if (httpClientData is null)
            {
                throw new ArgumentNullException(nameof(httpClientData));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var client = await httpClientData;
            var stream=await client.HttpClient.DownloadPackage(id,version,client.Content);
            return new HttpClientData<Stream>(client,stream);
        }
        /// <summary>
        /// 下载清单文件
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<string>> WithDownLoadManifest(this Task<HttpClientData<ServiceList>> httpClientData, string id, string version)
        {
            if (httpClientData is null)
            {
                throw new ArgumentNullException(nameof(httpClientData));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var client = await httpClientData;
            var stream = await client.HttpClient.DownloadManifest(client,id, version);
            return new HttpClientData<string>(client, stream);
        }
        /// <summary>
        /// 获取包详细URL
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<string>> WithPackageDetail(this Task<HttpClientData<ServiceList>> httpClientData, string id, string version)
        {
            if (httpClientData is null)
            {
                throw new ArgumentNullException(nameof(httpClientData));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var client = await httpClientData;
            var res=await client.HttpClient.GetPackageDetail(client,id,version);
            return new HttpClientData<string>(client, res);
        }
        /// <summary>
        /// 下载包
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <param name="serviceList"></param>
        /// <returns></returns>
        private static async Task<Stream> DownloadPackage(this HttpClient httpClient,string id, string version, ServiceList serviceList)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            if (serviceList is null)
            {
                throw new ArgumentNullException(nameof(serviceList));
            }

            var packages = serviceList["PackageBaseAddress"];
            var res = await httpClient.DownloadPackage(packages.ID,id, version);
            return res;

        }
        /// <summary>
        /// 下载包
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestFullUrl"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static async Task<Stream> DownloadPackage(this HttpClient httpClient, string requestFullUrl,string id,string version)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(requestFullUrl))
            {
                throw new ArgumentException("message", nameof(requestFullUrl));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var path=$"{requestFullUrl}/{id.ToLowerInvariant()}/{version.ToLowerInvariant()}/{id.ToLowerInvariant()}.{version.ToLowerInvariant()}.nupkg" ;
             var content=await  httpClient.GetStreamAsync(path);
            return content;
        }
        /// <summary>
        /// 下载清单文件
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="serviceList"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static async Task<string> DownloadManifest(this HttpClient httpClient, ServiceList serviceList, string id, string version)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (serviceList is null)
            {
                throw new ArgumentNullException(nameof(serviceList));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var packages = serviceList["PackageBaseAddress"];
            var res = await httpClient.DownloadManifest(packages.ID,id, version);
            return res;
        }
        /// <summary>
        /// 获取包详细信息URL
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="serviceList"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static async Task<string> GetPackageDetail(this HttpClient httpClient, ServiceList serviceList, string id, string version)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (serviceList is null)
            {
                throw new ArgumentNullException(nameof(serviceList));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var packages = serviceList["PackageDetailsUriTemplate"];
            var res = await httpClient.GetPackageDetail(packages.ID, id, version);
            return res;
        }
        /// <summary>
        /// 获取包详细网页浏览路径
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestFullUrl"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static async Task<string> GetPackageDetail(this HttpClient httpClient, string requestFullUrl, string id, string version)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(requestFullUrl))
            {
                throw new ArgumentException("message", nameof(requestFullUrl));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var uri = requestFullUrl.Replace("{id}", id.ToLowerInvariant()).Replace("{version}", version.ToLowerInvariant());
            var res = await httpClient.GetStringAsync(uri);
            return res;
        }
        /// <summary>
        /// 下载包清单文件
        /// </summary>
        /// <returns></returns>
        private static async Task<string> DownloadManifest(this HttpClient httpClient, string requestFullUrl, string id, string version)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrEmpty(requestFullUrl))
            {
                throw new ArgumentException("message", nameof(requestFullUrl));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("message", nameof(version));
            }

            var path = $"{requestFullUrl}/{id.ToLowerInvariant()}/{version.ToLowerInvariant()}/{id.ToLowerInvariant()}.nuspec";
            var uri = new Uri(path).ToString();
            var content=await  httpClient.GetStringAsync(uri);
            return content;
        }
        /// <summary>
        /// 添加APIKey
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<ServiceList>> WithApiKey(this Task<HttpClientData<ServiceList>> httpClient,string apiKey)
        {
            var client = await httpClient;
            client.HttpClient.DefaultRequestHeaders.Add("X-NuGet-ApiKey", apiKey);
            return new HttpClientData<ServiceList>(client.HttpClient, client.Content);
        }
        /// <summary>
        /// 重新列出包
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestUrl"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithListPacket(this HttpClient httpClient,string requestUrl, string id, string version)
        {
            var url = $"{requestUrl}/{id.ToLowerInvariant()}/{version.ToLowerInvariant()}";
            var respond=await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, url));
            return new HttpClientData<HttpStatusCode>(httpClient,respond.StatusCode);
        }
        /// <summary>
        /// 删除指定包
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestUrl"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithDeletePacket(this HttpClient httpClient, string requestUrl, string id, string version)
        {
            var url = $"{requestUrl}/{id.ToLowerInvariant()}/{version.ToLowerInvariant()}";
            var respond = await httpClient.DeleteAsync(url);
            var msg = respond.ToString();
            return new HttpClientData<HttpStatusCode>(httpClient, respond.StatusCode);
        }
        /// <summary>
        /// 删除指定包
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithDeletePacket(this Task<HttpClientData<ServiceList>> httpClientData, string id, string version)
        {
            var client = await httpClientData;
            var service = client.Content["PackagePublish"];
            return await client.HttpClient.WithDeletePacket(service.ID, id, version);
        }
        /// <summary>
        /// 重写列出包
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithReListPacket(this Task<HttpClientData<ServiceList>> httpClientData, string id, string version)
        {
            var client =await httpClientData;
            var service=  client.Content["PackagePublish"];
            return await  client.HttpClient.WithListPacket(service.ID,id,version);
        }
        /// <summary>
        /// 当包被成功重写列出时触发
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="onSuccess"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithOK(this Task<HttpClientData<HttpStatusCode>> httpClientData,Action<HttpClientData<HttpStatusCode>> onSuccess)
        {
            var client =await httpClientData;
            if (client.Content==HttpStatusCode.OK)
            {
                onSuccess?.Invoke(client);
            }
            return client;
        }
        /// <summary>
        /// 当服务器拒绝时触发
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="onSuccess"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithForbidden(this Task<HttpClientData<HttpStatusCode>> httpClientData, Action<HttpClientData<HttpStatusCode>> onSuccess)
        {
            var client = await httpClientData;
            if (client.Content == HttpStatusCode.Forbidden)
            {
                onSuccess?.Invoke(client);
            }
            return client;
        }
        /// <summary>
        /// 包已被删除
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="onSuccess"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithNoContent(this Task<HttpClientData<HttpStatusCode>> httpClientData, Action<HttpClientData<HttpStatusCode>> onSuccess)
        {
            var client = await httpClientData;
            if (client.Content == HttpStatusCode.NoContent)
            {
                onSuccess?.Invoke(client);
            }
            return client;
        }
        /// <summary>
        /// 包未找到或者不存在
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="onSuccess"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithNotFound(this Task<HttpClientData<HttpStatusCode>> httpClientData, Action<HttpClientData<HttpStatusCode>> onSuccess)
        {
            var client = await httpClientData;
            if (client.Content == HttpStatusCode.NotFound)
            {
                onSuccess?.Invoke(client);
            }
            return client;
        }
        /// <summary>
        /// 未认证时触发
        /// </summary>
        /// <param name="httpClientData"></param>
        /// <param name="onSuccess"></param>
        /// <returns></returns>
        public static async Task<HttpClientData<HttpStatusCode>> WithUnauthorized(this Task<HttpClientData<HttpStatusCode>> httpClientData, Action<HttpClientData<HttpStatusCode>> onSuccess)
        {
            var client = await httpClientData;
            if (client.Content == HttpStatusCode.Unauthorized)
            {
                onSuccess?.Invoke(client);
            }
            return client;
        }
    }
}
