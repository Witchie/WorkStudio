using DevExpress.Xpf.Docking;
using System;
using System.Windows;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Media;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 工具箱服务
    /// </summary>
    public class PanelService : IPanelService
    {
        private readonly GlobalLayout globalLayout;
        private readonly ExportBox exportBox;
        /// <summary>
        /// 工具箱服务
        /// </summary>
        /// <param name="globalLayout"></param>
        /// <param name="commonService"></param>
        public PanelService(GlobalLayout globalLayout, ExportBox exportBox)
        {
            this.globalLayout = globalLayout;
            this.exportBox = exportBox;
        }
        public const double CommonWith = 250D;
        /// <summary>
        /// 创建面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelInfo">工具栏信息</param>
        /// <param name="panelLocation">工具栏位置</param>
        /// <returns></returns>
        public PanelInfo<T> CreateToolBoxPanel<T>(PanelInfo<T> panelInfo, PanelLocation panelLocation = PanelLocation.Left) where T : FrameworkElement
        {
            if (panelInfo is null)
            {
                throw new ArgumentNullException(nameof(panelInfo));
            }
        Again:
            SelfCheck(globalLayout);
            var group = globalLayout.root.Items.Where(p => p.Name == "toolBox_Containter").FirstOrDefault() as TabbedGroup;
            if (panelLocation == PanelLocation.Right)
            {
                group = globalLayout.root.Items.Where(p => p.Name == "toolBox_Containter_right").FirstOrDefault() as TabbedGroup;
            }
            if (group == null)
            {
                goto Again;
            }
            var items = group.GetItems();
            var dd = items.Where(p => p.Caption?.ToString() == panelInfo.Caption).FirstOrDefault();
            if (dd == null)
            {
                var panel = new LayoutPanel();
                panel.Caption = panelInfo.Caption;
                panel.ItemWidth = new GridLength(CommonWith);
                panel.Content = panelInfo.Content;
                panel.Style = globalLayout.FindResource("ToolBox_Panel") as Style;
                group.Add(panel);
                panel.IsActive = true;
                return new PanelInfo<T>() { Caption = panelInfo.Caption, Content = panelInfo.Content };
            }
            else
            {
                dd.IsActive = true;
                return new PanelInfo<T>() { Caption = panelInfo.Caption, Content = dd as T };
            }
        }
        private void Remove(string name)
        {
            var count = LogicalTreeHelper.GetChildren(globalLayout.root).OfType<LayoutGroup>().Where(p => p.Name == name).Count();
            int num = 0;
            Count(globalLayout.root, name, num);
            if (count > 1)
            {
                var b = globalLayout.root.Items.Where(p => p.Name == name).FirstOrDefault();
                if (b != null)
                {
                    globalLayout.root.Items.Remove(b);
                }
                else
                {
                    Remove(globalLayout.root, name);
                }
            }
        }
        private void Count(LayoutGroup layoutGroup, string name, int count)
        {
            if (layoutGroup.Items.Where(p => p.Name == name).Count() > 1)
            {
                var b = layoutGroup.Items.Where(p => p.Name == name).FirstOrDefault();
                if (b != null)
                {
                    count++;
                    return;
                }
                else
                {
                    foreach (var item in layoutGroup.Items)
                    {
                        if (item is LayoutGroup group)
                        {
                            Count(group, name, count);
                        }
                    }
                }
            }
        }
        private void Remove(LayoutGroup layoutGroup, string name)
        {
            if (layoutGroup.Items.Where(p => p.Name == name).Count() > 1)
            {
                var b = layoutGroup.Items.Where(p => p.Name == name).FirstOrDefault();
                if (b != null)
                {
                    layoutGroup.Items.Remove(b);
                    return;
                }
                else
                {
                    foreach (var item in layoutGroup.Items)
                    {
                        if (item is LayoutGroup group)
                        {
                            Remove(group, name);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 自我检查
        /// </summary>
        /// <param name="globalLayout"></param>
        private void SelfCheck(GlobalLayout globalLayout)
        {
            if (globalLayout.root.Items.Count >= 3)
            {
                Remove("toolBox_Containter");
                Remove("_mainContainer");
                Remove("toolBox_Containter_right");
            }
            var toolBox_Containter = LogicalTreeHelper.GetChildren(globalLayout.root).OfType<TabbedGroup>().Where(p => p.Name == "toolBox_Containter").FirstOrDefault();
            // var toolBox_Containter= globalLayout.root.Items.Where(p => p.Name == "toolBox_Containter").FirstOrDefault();
            if (toolBox_Containter == null)
            {
                globalLayout.root.Items.Insert(0, new TabbedGroup { Style = globalLayout.FindResource("ToolBox_Container") as Style, Name = "toolBox_Containter" });
            }
            var _mainContainer = LogicalTreeHelper.GetChildren(globalLayout.root).OfType<LayoutGroup>().Where(p => p.Name == "_mainContainer").FirstOrDefault();

            // var _mainContainer = globalLayout.root.Items.Where(p => p.Name == "_mainContainer").FirstOrDefault();
            if (_mainContainer == null)
            {
                var p1 = new LayoutGroup { Name = "_mainContainer", Orientation = System.Windows.Controls.Orientation.Vertical };
                p1.AllowClose = false;
                p1.AllowHide = false;
                p1.Items.Add(new DocumentGroup { Name = "documents", Style = globalLayout.FindResource("Document_Container") as Style });
                globalLayout.root.Items.Insert(1, p1);
            }
            else
            {
                var exist = _mainContainer.Children<DocumentGroup>().Where(p => p.Name == "documents").FirstOrDefault();
                if (exist == null)
                {
                    _mainContainer.Items.Insert(0, new DocumentGroup { Name = "documents", Style = globalLayout.FindResource("Document_Container") as Style });
                }
            }
            //var toolBox_Containter_right = globalLayout.root.Items.Where(p => p.Name == "toolBox_Containter_right").FirstOrDefault();
            var toolBox_Containter_right = LogicalTreeHelper.GetChildren(globalLayout.root).OfType<BaseLayoutItem>().Where(p => p.Name == "toolBox_Containter_right").FirstOrDefault();
            if (toolBox_Containter == null)
            {
                globalLayout.root.Items.Insert(2, new TabbedGroup { Style = globalLayout.FindResource("ToolBox_Container") as Style, Name = "toolBox_Containter_right" });
            }
        }
        /// <summary>
        /// 创建文档信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentPanel"></param>
        /// <returns></returns>
        public DocumentInfo<BaseLayoutItem> CreateDocumentPanel<T>(DocumentInfo<T> documentPanel) where T : FrameworkElement
        {
            if (documentPanel is null)
            {
                throw new ArgumentNullException(nameof(documentPanel));
            }
        Again:
            SelfCheck(globalLayout);
            LogicalTreeHelper.GetChildren(globalLayout.root).OfType<LayoutGroup>().Where(p => p.Name == "_mainContainer").FirstOrDefault();
            var maincontainer = LogicalTreeHelper.GetChildren(globalLayout.root).OfType<LayoutGroup>().Where(p => p.Name == "_mainContainer").FirstOrDefault();
            if (maincontainer == null)
            {
                goto Again;
            }
            var items = maincontainer.Children<DocumentGroup>().FirstOrDefault();
            if (items == null)
            {
                goto Again;
            }
            var dd = LogicalTreeHelper.GetChildren(items).OfType<BaseLayoutItem>().Where(p => p.Caption?.ToString() == documentPanel.Caption).FirstOrDefault();// items.Items.Where(p => p.Caption?.ToString() == documentPanel.Caption).FirstOrDefault();
            if (dd == null)
            {
                var panel = new DocumentPanel();
                panel.Caption = documentPanel.Caption;
                panel.ItemWidth = new GridLength(CommonWith);
                panel.Content = documentPanel.Content;
                panel.Style = globalLayout.FindResource("Document_Item") as Style;
                items.Add(panel);
                items.AllowClose = false;
                items.AllowDrag = false;
                panel.IsActive = true;
                return new DocumentInfo<BaseLayoutItem>() { Caption = documentPanel.Caption, Content = panel };
            }
            else
            {
                dd.IsActive = true;
                return new DocumentInfo<BaseLayoutItem>() { Caption = documentPanel.Caption, Content = dd };
            }
        }
        /// <summary>
        /// 添加输出框
        /// </summary>
        /// <returns></returns>
        public LayoutPanel CreateOutputBox()
        {
        Ag:
            SelfCheck(globalLayout);
            if (globalLayout.root.Items.Count < 2)
            {
                goto Ag;
            }
            //_mainContainer
            //var maincontainer = (globalLayout.root.Items[1] as LayoutGroup);
            var root = globalLayout.root.Find<LayoutGroup>("_mainContainer");
            if (root == null)
            {
                goto Ag;
            }
        Panel:
            var panel = root.Children<LayoutGroup>().Where(p => p.Caption?.ToString() == "消息框").FirstOrDefault();
            if (panel == null)
            {
                root.CreateLayoutGroup("消息框");
                goto Panel;
            }
            panel.IsActive = true;
        LayoutPanel:
            var layoutPanel = panel.Children<LayoutPanel>().Where(p => p.Caption?.ToString() == "输出框").FirstOrDefault(); //panel.IsPanelExist<LayoutPanel>("输出框");
            if (layoutPanel == null)
            {
                var style = globalLayout.root.FindResource("Export_Container") as Style;
                var panel1 = panel.CreateLayoutPanel("输出框", style);
                panel1.Content = exportBox;
                panel1.ShowMaximizeButton = false;
                panel1.ShowPinButton = false;
                panel1.IsActive = true;
                panel1.AllowDrag = false;
                goto LayoutPanel;
            }
            layoutPanel.AutoHidden = false;
            layoutPanel.IsActive = true;
            layoutPanel.AllowDock = false;
            layoutPanel.AllowDrop = false;
            layoutPanel.AllowFloat = false;
            return layoutPanel;
        }
    }
}
