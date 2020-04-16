using DevExpress.Xpf.Ribbon;
using GreenWhale.BootLoader.Implements;
using System.Windows.Media;

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public static class RibbonControlExtension
    {
        /// <summary>
        /// 获取Ribbon控件
        /// </summary>
        /// <param name="ribbonBar"></param>
        /// <returns></returns>
        public static RibbonControl GetRibbonControl(this RibbonBar ribbonBar)
        {
          var ribbon= (RibbonControl)VisualTreeHelper.GetChild(ribbonBar,0);
            return ribbon;
        }
        /// <summary>
        ///修改图标
        /// </summary>
        /// <param name="ribbonControl"></param>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        public static RibbonControl SetIcon(this RibbonControl ribbonControl,ImageSource imageSource)
        {
            ribbonControl.ApplicationButtonSmallIcon = imageSource;
            return ribbonControl;
        }
        public static RibbonControl SetIconStyle(this RibbonControl ribbonControl, RibbonMenuIconStyle iconStyle)
        {
            ribbonControl.MenuIconStyle = iconStyle;
            return ribbonControl;
        }
    }
}
