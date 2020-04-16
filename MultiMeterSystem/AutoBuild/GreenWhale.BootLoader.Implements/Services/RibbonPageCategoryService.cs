using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using GreenWhale.BootLoader.Implements.Properties;
using System.Linq;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// RibbonPage分组服务
    /// </summary>
    public class RibbonPageCategoryService
    {
        private readonly IRibbonBarService ribbonBarService;
        /// <summary>
        /// RibbonPage分组服务
        /// </summary>
        /// <param name="ribbonBarService"></param>
        public RibbonPageCategoryService(IRibbonBarService ribbonBarService)
        {
            this.ribbonBarService = ribbonBarService;
        }
        /// <summary>
        /// 当前页面分组名称
        /// </summary>
        public string PageCategoryName { get=> ribbonBarService.CurrentPageCategory.Caption; set=> ribbonBarService.CurrentPageCategory.Caption=value; }
        /// <summary>
        /// 添加RibbonPage
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="page"></param>
        public void AddPage<TPage>(TPage page) where TPage: RibbonPage
        {
            if (ribbonBarService?.CurrentPageCategory?.Pages?.Count==0)
            {
                page.SetDefault();
            }
            ribbonBarService.CurrentPageCategory.Pages.Add(page);
        }
        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="captionName"></param>
        public void AddPage(string captionName)
        {

            ribbonBarService.CurrentPageCategory.Add(captionName);
            Update();
        }
        private void Update()
        {
            if (ribbonBarService.CurrentPageCategory.Pages.Count > 0)
            {
                ribbonBarService.CurrentRibbonIsVisible = true;
            }
            else
            {
                ribbonBarService.CurrentRibbonIsVisible = false;
            }
        }
        /// <summary>
        /// 获取页面
        /// </summary>
        /// <param name="captionName"></param>
        /// <returns></returns>
        public RibbonPage GetPage(string captionName)
        {
            if (captionName is null)
            {
                return null;
            }

            return  ribbonBarService?.CurrentPageCategory.Pages.Where(p => p.Caption?.ToString() == captionName).FirstOrDefault();
        }
        /// <summary>
        /// 删除指定页面
        /// </summary>
        /// <param name="captionName"></param>
        /// <returns></returns>
        public bool RemovePage(string captionName)
        {
            var page=   GetPage(captionName);
            if (page!=null)
            {
                return ribbonBarService.CurrentPageCategory.Pages.Remove(page);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="pageCaption"></param>
        /// <param name="groupCaption"></param>
        /// <param name="groupCommand"></param>
        /// <returns></returns>
        public RibbonPageGroup AddGroup(string pageCaption,string groupCaption,CommandService groupCommand)
        {
            var page= GetPage(pageCaption);
            if (page!=null)
            {
                return  page.Add(groupCaption, groupCommand);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取页面分组
        /// </summary>
        /// <param name="pageCaption"></param>
        /// <param name="groupCaption"></param>
        /// <returns></returns>
        public RibbonPageGroup GetGroup(string pageCaption, string groupCaption)
        {
            var page = GetPage(pageCaption);
            if (page != null)
            {
                return page.GetGroup(groupCaption);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 添加默认页面
        /// </summary>
        /// <param name="name"></param>
        public void AddPageDefault(string name)
        {
            var page=  ribbonBarService.CurrentPageCategory.Add(name);
            page.SetDefault();
        }
        /// <summary>
        /// 添加默认分组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RibbonPageGroup AddGroupDefault()
        {
            if (DefaultPage!=null)
            {
                var group=   DefaultPage.Add(Resource.default_group_name);
                return group;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 默认页面
        /// </summary>
        public RibbonPage DefaultPage => ribbonBarService.CurrentPageCategory.Pages.Where(p=>p.IsDefault()).FirstOrDefault();
        /// <summary>
        /// 添加关闭按钮
        /// </summary>
        public void AddCloseButton()
        {
            if (DefaultPage!=null)
            {
               var group=  DefaultPage.GetGroup(Resource.default_group_name);
                if (group!=null)
                {
                    var button= new BarButtonItem() { Content = Resource.bar_button_close,RibbonStyle=RibbonItemStyles.Large};
                    button.ItemClick += Button_ItemClick;
                    group.Items.Add(button);
                }
            }
        }

        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClearReset();
            if (sender is BarButtonItem buttonItem)
            {
                buttonItem.ItemClick -= Button_ItemClick;
            }
        }
        /// <summary>
        /// 清空并注销事件
        /// </summary>
        private void ClearReset()
        {
            ribbonBarService.CurrentPageCategory.Pages.Clear();
            ribbonBarService.CurrentRibbonIsVisible = false;
        }
    }
}
