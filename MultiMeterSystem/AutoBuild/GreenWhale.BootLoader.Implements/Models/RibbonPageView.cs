using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 默认页面分组
    /// </summary>
    public class RibbonPageView: IButtonItem
    {
        /// <summary>
        /// 按钮菜单
        /// </summary>
        /// <param name="menuInfo">格式:rabbionGroup/group/buttonName:description/</param>
        /// <param name="imageSource">icon图片</param>
        /// <param name="tagData">附加数据</param>
        public RibbonPageView(string menuInfo, ImageSource imageSource = null, object tagData = null)
        {
            if (string.IsNullOrWhiteSpace(menuInfo))
            {
                throw new System.ArgumentException("message", nameof(menuInfo));
            }

            var menus = menuInfo.Split("/");
            if (menus != null && menus.Length >= 3)
            {
                RibbonPageCaption = menus[0];
                RibbonPageGroupCaption = menus[1];
                var menu = menus[2];
                var tips = menu.Split(":");
                BarButtonItemName = tips.First();
                BarButtonItemNameDescription = tips[1];
                ImageSource = imageSource;
                TagData = tagData;
            }
            else
            {
                throw new ArgumentException("菜单信息不得为空");
            }
        }
        public static implicit operator RibbonPageView(string content)
        {
            return new RibbonPageView(content);
        }
        /// <summary>
        /// 页面分组名称
        /// </summary>
        public string RibbonPageCaption { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string RibbonPageGroupCaption { get; set; }
        /// <summary>
        /// 视图简称
        /// </summary>
        public string BarButtonItemName { get; set; }
        /// <summary>
        /// 按钮描述信息
        /// </summary>
        public string BarButtonItemNameDescription { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource ImageSource { get; set; }

        /// <summary>
        ///寄存数据
        /// </summary>
        public object TagData { get; set; }
        /// <summary>
        /// 命令绑定
        /// </summary>
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public IInputElement CommandTarget { get; set; }

        public RibbonPageView ToRibbonPageView()
        {
            return this;
        }
    }
}
