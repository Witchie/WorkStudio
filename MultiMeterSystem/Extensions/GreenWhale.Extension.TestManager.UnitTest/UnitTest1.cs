using GreenWhale.Docx;
using GreenWhale.Extensions.TestManager;
using System;
using System.IO;
using GreenWhale.Extensions.TestManager.DocxDocuments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace GreenWhale.Extension.TestManager.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataTable()
        {
            Purpose vs = new Purpose();
            vs.Add("验证系统功能是否完善");
            vs.Add("逻辑是否完整");
            vs.Add("功能是否符合预期");
           var dataTable=  vs.ToDataTable(true);
            Console.WriteLine(dataTable);
            string text = vs;
            Console.WriteLine(text);
            string text1 = vs.ToString();
            Console.WriteLine(text1);
        }
        [TestMethod]
        public void DataTable1()
        {
            Extensions.TestManager.DocumentModifyInfo modifyInfos = new Extensions.TestManager.DocumentModifyInfo();
            modifyInfos.Add(new ModifyInfo(new Version(1, 0), new DateTime(2019, 12, 12), "创建文档"));
            modifyInfos.Add(new ModifyInfo(new Version(1, 1), new DateTime(2020, 1, 3), "测试文档功能"));
           var dataTable=  modifyInfos.ToDataTable(true);
            Assert.IsNotNull(dataTable);
        }
        TestPlanDocument doc = new TestPlanDocument()
        {
            CompanyInfo = new CompanyInfo
            {
                Address = "杭州市登云路425号利尔达大厦",
                CompanyLogoImage = @"C:\Users\WXF\Desktop\header.png",
                EnglishName = "LIERDA SCIENCE & TECHNOLOGY GROUP CO., LTD",
                Name = "利尔达科技集团股份有限公司",
                Fax = "0571-89908080",
                Telephone = "0571-88800000"
            },
            ProjectTitleInfo = new ProjectTitleInfo
            {
                DateTime = DateTime.Now,
                Model = "LSD3SW-00201010",
                Name = "ZR水表生产测试上位机",
                Version = new Version(0, 1)
            },
            DocumentNameInfo = new DocumentNameInfo
            {
                EnglishName = "R&D Test Scheme",
                Name = "研发测试方案"
            },
            DescriptionInfo = new DescriptionInfo
            {
                Purpose = new Purpose("验证系统功能否满足产品的性能需求", "发现系统中存在的性能瓶颈", "起到优化系统的目的"),
            },
            DocumentModifyInfo = new DocumentModifyInfo()
            {
                new ModifyInfo(new Version(0, 1), DateTime.Now, "首次新建"),
                new ModifyInfo(new Version(1, 1), DateTime.Now.AddDays(1), "修改了一些问题"),
            },
            TestAnalysis = new TestAnalysis
            {
                Purpose = new Purpose("验证调用API是否可用", "验证编解码插件可行性，稳定性"),
                TestContent = new TestContent("设备刷新", "新增与删除", "平台查询", "平台应答", "设备参数配置", "数据透传", "运行编解码插件测试工具", "设备应答", "表端主动上报"),
                EnviromentInfo = new EnviromentInfo
                {
                    Software = new Software(),
                    Hardware = new Hardware(),
                },
                TestObject = new TestObject("Nb调试客户端", "编解码插件"),
                TestRange = new TestRange("Nb调试客户端功能测试", "编解码插件功能测试"),

            },
            TestPlans = new TestPlansCore<TestPlan>()
                {
                    new TestPlan()
                    {
                         Purpose=new Purpose("验证设备新增删除正常"),
                         TestSteps=new TestSteps()
                         {
                             new TestStep()
                             {
                                 Action="单击刷新",
                                 Attach=new Attach(),
                                 Expected="刷新设备列表"
                             },
                             new TestStep()
                             {
                                 Action="重复添加35个设备，然后刷新是否全部显示",
                                 Expected="可以显示，并分页",
                                 Attach=new Attach(),
                             }
                         },
                         Category="NB调试客户端",
                         Name="设备新增与删除"
                    },
                    new TestPlan()
                    {
                        Category="NB调试客户端",
                        Name="刷新设备列表",
                        Purpose=new Purpose("验证是否可以刷新设备列表并分页"),
                        TestSteps=new TestSteps()
                        {
                            new TestStep()
                            {
                                Action="单击刷新",
                                Attach=new Attach(),
                                Expected="刷新设备列表"
                            },
                            new TestStep()
                            {
                                Action="重复添加35个设备，然后刷新是否全部显示",
                                Attach=new Attach(),
                                Expected="可以显示，并分页"
                            }
                        }
                    },new TestPlan()
                    {
                        Name="平台查询",
                        Category="插件自动测试",
                        Purpose=new Purpose("验证编解码插件是否可以解码"),
                        TestSteps=new TestSteps()
                        {
                            new TestStep("运行编解码测试工具",""),
                            new TestStep("复制编解码插件到编解码插件目录下","复制成功"),
                            new TestStep("复制devicetype-capability.json 到编解码插件目录下","复制成功"),
                            new TestStep("启动编解码测试工具","启动成功"),
                        }
                    },new TestPlan()
                    {
                        Name="查询功能验证",
                        Category="插件自动测试",
                        Purpose="验证IC卡读卡是否正常、验证请求深燃支付API是否正常",
                        TestSteps=new TestSteps
                        {
                           new TestStep("插入卡片","可自动判卡"),
                           new TestStep("自动连接深燃服务器 网络正常","查询用户信息成功"),
                           new TestStep("自动连接深燃服务器 网络异常","查询失败，告知原因"),
                           new TestStep("自动检查卡内购买次数和卡片购买次数 是否相等","相同继续，相异报错"),
                           new TestStep("自动检查当前卡片是否存在失败的账单","如果当前用户存在写卡失败的账单，提示前往写卡处理，否则继续"),
                           new TestStep("自动检查卡片内是否有购买量","如果卡片内存在购买量，则不可充值"),
                           new TestStep("选择购买方式","可正确选择 金额购买或用量购买。不同的购买方式 后续跳出的单位也不同"),
                           new TestStep("键入购买信息，单击确定"," 自动生成微信支付账单"),
                           new TestStep("查看购买信息","阶梯价用户可以查看到阶梯价信息，非三阶梯用户可以查看到非三阶梯购买信息"),
                           new TestStep("打开微信支付，扫描 ","二维码可扫描，且正确显示应支金额"),
                           new TestStep("切换到测试模式，扫描二维码并付款0.01元","可正确选择 金额购买或用量购买。不同的购买方式 后续跳出的单位也不同"),
                           new TestStep("查询用户服务器购买信息","推送SOAP账单信息推送成功，且查询用户信息时显示正确"),
                           new TestStep("打印小票","如果打印成功，则修改账单信息为成功，否则修改为失败 "),
                        }
                    }
                }
        };
        [TestMethod]
        public void DocumentTest()
        {
            using (var docx = DocX.Create(@"C:\Users\WXF\Desktop\test.docx"))
            {
                docx.WithPageSize().WithPageMargin();
                docx.WithFirstPage(doc).WithSecondPage(doc).WithMenuPage(doc).WithMainContent(doc).WithPageNumber() ;
                docx.Save();
            }
        }
    }
}
