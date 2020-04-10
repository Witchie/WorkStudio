using System;
using System.Linq;
using System.Windows.Media;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 单击触发 
    /// </summary>
    public class RibbonMenuWithPageView
    {
        internal CommandService Service { get;private set; }
        public RibbonMenuWithPageView LoadService(CommandService commandService)
        {
            this.Service = commandService;
            return this;
        }
        /// <summary>
        /// 按钮菜单
        /// </summary>
        /// <param name="menuInfo">格式:rabbionGroup/group/buttonName:description/</param>
        /// <param name="imageSource">icon图片</param>
        /// <param name="tagData">附加数据</param>
        public RibbonMenuWithPageView(string menuInfo,ImageSource imageSource = null, object tagData = null)
        {
            if (string.IsNullOrWhiteSpace(menuInfo))
            {
                throw new System.ArgumentException("message", nameof(menuInfo));
            }

           var menus=  menuInfo.Split("/");
            if (menus!=null&&menus.Length>=3)
            {
                RibbonPageCaption = menus[0];
                RibbonPageGroupCaption = menus[1];
                var menu= menus[2];
                var tips= menu.Split(":");
                BarButtonItemName = tips.First();
                if (tips.Length>1)
                {
                    BarButtonItemNameDescription = tips[1];
                }
                ImageSource = imageSource;
                TagData = tagData;
            }
            else
            {
                throw new ArgumentException("菜单信息不得为空");
            }
        }
        /// <summary>
        /// 单击触发事件
        /// </summary>
        /// <param name="ribbonPageCaption">分页</param>
        /// <param name="ribbonPageGroupCaption">分组</param>
        /// <param name="barButtonItemName">按钮名称</param>
        public RibbonMenuWithPageView(string ribbonPageCaption, string ribbonPageGroupCaption, string barButtonItemName, ImageSource imageSource = null, object tagData = null, string toolTips = null)
        {
            RibbonPageCaption = ribbonPageCaption;
            RibbonPageGroupCaption = ribbonPageGroupCaption;
            BarButtonItemName = barButtonItemName;
            ImageSource = imageSource;
            TagData = tagData;
            BarButtonItemNameDescription = toolTips;
        }
        /// <summary>
        /// 页面分组名称
        /// </summary>
        public string RibbonPageCaption { get;private set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string RibbonPageGroupCaption { get; private set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string BarButtonItemName { get; private set; }
        /// <summary>
        /// 按钮描述信息
        /// </summary>
        public string BarButtonItemNameDescription { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource ImageSource { get; private set; }

        /// <summary>
        ///寄存数据
        /// </summary>
        public object TagData { get; private set; }
    }
}
