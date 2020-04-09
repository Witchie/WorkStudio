using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreenWhale.BootLoader.Extensions.Nuget.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetTools()
        {
            HttpClient httpClient = new HttpClient();
            var tools= await httpClient.WithGetTools();
            if (tools==null)
            {
                Assert.Fail();
            }
            foreach (var item in tools.Content)
            {
                System.Console.WriteLine(item);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ServiceList()
        {
            var client = new HttpClient();
            var list=await client.WithIndexList();
            foreach (var item in list.Content.Resource)
            {
                System.Console.WriteLine($"{item.ServiceInfo().ServiceType}\t{item.ID}\t{item.Commment}");
            }
        }

        /// <summary>
        /// ��ѯ��
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task QueryPakckage()
        {
            var client = new HttpClient();
            var packages =await client.WithIndexList().WithQueryPacket("GreenWhale");
            foreach (var item in packages.Content.Data)
            {
                System.Console.WriteLine(item.ID);
            }
        }
        /// <summary>
        /// ��ѯ�汾
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task QueryPakckageVersion()
        {
            var client = new HttpClient();
            var packages = await client.WithIndexList().WithPakckageVersion("GreenWhale.WebCore");
            foreach (var item in packages.Content.Versions)
            {
                System.Console.WriteLine(item);
            }
        }
        /// <summary>
        /// ���ذ��ļ�
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task DownLoadPackage()
        {
            var client = new HttpClient();
            var packages = await client.WithIndexList().WithDownLoadPackage("GreenWhale.WebCore", "1.0.1").OnSavePackage("GreenWhale.WebCore.1.0.1.nupkg",Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }
        /// <summary>
        /// �����嵥�ļ�
        /// </summary>
        /// <returns></returns>
        [TestMethod]

        public async Task DownLoadManifest()
        {
            var client = new HttpClient();
            var packages = await client.WithIndexList().WithDownLoadManifest("GreenWhale.WebCore", "1.0.1").OnSaveManifest("GreenWhale.WebCore.1.0.1.nuspec", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }
        /// <summary>
        /// ��ȡ������ϸ�嵥��Ϣ
        /// </summary>
        /// <returns></returns>
        [TestMethod]

        public async Task GetPackageDetail()
        {
            var client = new HttpClient();
            var packages = await client.WithIndexList().WithPackageDetail("GreenWhale.WebCore", "1.0.1");
            Console.WriteLine(packages.Content);
        }
        [TestMethod]
        public async Task ReListPackage()
        {
            var client = new HttpClient();
            var packages = await client.WithIndexList().WithApiKey("oy2as4g4qvchjdlxgk5axvo7m2te7ite3dcgetobkhdrnu").WithReListPacket("GreenWhale.WebCore", "1.0.1").WithUnauthorized(s =>
            {
                Console.WriteLine("δ��֤");
                Assert.Fail();
            }).WithNotFound(s =>
            {
                Console.WriteLine("���ҵ���Ӧ�İ�");
                Assert.Fail();
            }).WithOK(s =>
            {
                Console.WriteLine("���ͳɹ�");
            }).WithForbidden(s=>
            {
                Console.WriteLine("���󱻷������ܾ�");
            });
        }
        [TestMethod]
        public async Task DeletePackage()
        {
            var client = new HttpClient();
            var packages = await client.WithIndexList().WithApiKey("oy2ekvbjilm4sxirjayakie3gyavvjnenlbkdicctnucuu").WithDeletePacket("Lierda.Security", "1.0.1").WithUnauthorized(s =>
            {
                Console.WriteLine("δ��֤");
                Assert.Fail();
            }).WithNotFound(s =>
            {
                Console.WriteLine("���ҵ���Ӧ�İ�");
                Assert.Fail();
            }).WithOK(s =>
            {
                Console.WriteLine("���ͳɹ�");
            }).WithForbidden(s =>
            {
                Console.WriteLine("���󱻷������ܾ�");
                Assert.Fail();
            });
        }
    }
}
