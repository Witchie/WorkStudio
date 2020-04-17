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
            vs.Add("��֤ϵͳ�����Ƿ�����");
            vs.Add("�߼��Ƿ�����");
            vs.Add("�����Ƿ����Ԥ��");
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
            modifyInfos.Add(new ModifyInfo(new Version(1, 0), new DateTime(2019, 12, 12), "�����ĵ�"));
            modifyInfos.Add(new ModifyInfo(new Version(1, 1), new DateTime(2020, 1, 3), "�����ĵ�����"));
           var dataTable=  modifyInfos.ToDataTable(true);
            Assert.IsNotNull(dataTable);
        }
        TestPlanDocument doc = new TestPlanDocument()
        {
            CompanyInfo = new CompanyInfo
            {
                Address = "�����е���·425�����������",
                CompanyLogoImage = @"C:\Users\WXF\Desktop\header.png",
                EnglishName = "LIERDA SCIENCE & TECHNOLOGY GROUP CO., LTD",
                Name = "������Ƽ����Źɷ����޹�˾",
                Fax = "0571-89908080",
                Telephone = "0571-88800000"
            },
            ProjectTitleInfo = new ProjectTitleInfo
            {
                DateTime = DateTime.Now,
                Model = "LSD3SW-00201010",
                Name = "ZRˮ������������λ��",
                Version = new Version(0, 1)
            },
            DocumentNameInfo = new DocumentNameInfo
            {
                EnglishName = "R&D Test Scheme",
                Name = "�з����Է���"
            },
            DescriptionInfo = new DescriptionInfo
            {
                Purpose = new Purpose("��֤ϵͳ���ܷ������Ʒ����������", "����ϵͳ�д��ڵ�����ƿ��", "���Ż�ϵͳ��Ŀ��"),
            },
            DocumentModifyInfo = new DocumentModifyInfo()
            {
                new ModifyInfo(new Version(0, 1), DateTime.Now, "�״��½�"),
                new ModifyInfo(new Version(1, 1), DateTime.Now.AddDays(1), "�޸���һЩ����"),
            },
            TestAnalysis = new TestAnalysis
            {
                Purpose = new Purpose("��֤����API�Ƿ����", "��֤������������ԣ��ȶ���"),
                TestContent = new TestContent("�豸ˢ��", "������ɾ��", "ƽ̨��ѯ", "ƽ̨Ӧ��", "�豸��������", "����͸��", "���б���������Թ���", "�豸Ӧ��", "��������ϱ�"),
                EnviromentInfo = new EnviromentInfo
                {
                    Software = new Software(),
                    Hardware = new Hardware(),
                },
                TestObject = new TestObject("Nb���Կͻ���", "�������"),
                TestRange = new TestRange("Nb���Կͻ��˹��ܲ���", "����������ܲ���"),

            },
            TestPlans = new TestPlansCore<TestPlan>()
                {
                    new TestPlan()
                    {
                         Purpose=new Purpose("��֤�豸����ɾ������"),
                         TestSteps=new TestSteps()
                         {
                             new TestStep()
                             {
                                 Action="����ˢ��",
                                 Attach=new Attach(),
                                 Expected="ˢ���豸�б�"
                             },
                             new TestStep()
                             {
                                 Action="�ظ����35���豸��Ȼ��ˢ���Ƿ�ȫ����ʾ",
                                 Expected="������ʾ������ҳ",
                                 Attach=new Attach(),
                             }
                         },
                         Category="NB���Կͻ���",
                         Name="�豸������ɾ��"
                    },
                    new TestPlan()
                    {
                        Category="NB���Կͻ���",
                        Name="ˢ���豸�б�",
                        Purpose=new Purpose("��֤�Ƿ����ˢ���豸�б���ҳ"),
                        TestSteps=new TestSteps()
                        {
                            new TestStep()
                            {
                                Action="����ˢ��",
                                Attach=new Attach(),
                                Expected="ˢ���豸�б�"
                            },
                            new TestStep()
                            {
                                Action="�ظ����35���豸��Ȼ��ˢ���Ƿ�ȫ����ʾ",
                                Attach=new Attach(),
                                Expected="������ʾ������ҳ"
                            }
                        }
                    },new TestPlan()
                    {
                        Name="ƽ̨��ѯ",
                        Category="����Զ�����",
                        Purpose=new Purpose("��֤��������Ƿ���Խ���"),
                        TestSteps=new TestSteps()
                        {
                            new TestStep("���б������Թ���",""),
                            new TestStep("���Ʊ���������������Ŀ¼��","���Ƴɹ�"),
                            new TestStep("����devicetype-capability.json ���������Ŀ¼��","���Ƴɹ�"),
                            new TestStep("�����������Թ���","�����ɹ�"),
                        }
                    },new TestPlan()
                    {
                        Name="��ѯ������֤",
                        Category="����Զ�����",
                        Purpose="��֤IC�������Ƿ���������֤������ȼ֧��API�Ƿ�����",
                        TestSteps=new TestSteps
                        {
                           new TestStep("���뿨Ƭ","���Զ��п�"),
                           new TestStep("�Զ�������ȼ������ ��������","��ѯ�û���Ϣ�ɹ�"),
                           new TestStep("�Զ�������ȼ������ �����쳣","��ѯʧ�ܣ���֪ԭ��"),
                           new TestStep("�Զ���鿨�ڹ�������Ϳ�Ƭ������� �Ƿ����","��ͬ���������챨��"),
                           new TestStep("�Զ���鵱ǰ��Ƭ�Ƿ����ʧ�ܵ��˵�","�����ǰ�û�����д��ʧ�ܵ��˵�����ʾǰ��д�������������"),
                           new TestStep("�Զ���鿨Ƭ���Ƿ��й�����","�����Ƭ�ڴ��ڹ��������򲻿ɳ�ֵ"),
                           new TestStep("ѡ����ʽ","����ȷѡ�� ������������򡣲�ͬ�Ĺ���ʽ ���������ĵ�λҲ��ͬ"),
                           new TestStep("���빺����Ϣ������ȷ��"," �Զ�����΢��֧���˵�"),
                           new TestStep("�鿴������Ϣ","���ݼ��û����Բ鿴�����ݼ���Ϣ�����������û����Բ鿴���������ݹ�����Ϣ"),
                           new TestStep("��΢��֧����ɨ�� ","��ά���ɨ�裬����ȷ��ʾӦ֧���"),
                           new TestStep("�л�������ģʽ��ɨ���ά�벢����0.01Ԫ","����ȷѡ�� ������������򡣲�ͬ�Ĺ���ʽ ���������ĵ�λҲ��ͬ"),
                           new TestStep("��ѯ�û�������������Ϣ","����SOAP�˵���Ϣ���ͳɹ����Ҳ�ѯ�û���Ϣʱ��ʾ��ȷ"),
                           new TestStep("��ӡСƱ","�����ӡ�ɹ������޸��˵���ϢΪ�ɹ��������޸�Ϊʧ�� "),
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
