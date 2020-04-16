using System;
using DevExpress.Xpf.Ribbon;

namespace GreenWhale.BootLoader.Implements
{
    public static class RibbonPageExtension
    {
        /// <summary>
        /// 是否默认页面
        /// </summary>
        /// <param name="ribbonPage"></param>
        /// <returns></returns>
        public static bool IsDefault(this RibbonPage ribbonPage)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            if (ribbonPage.Tag is bool res)
            {
                return res;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置为默认页面
        /// </summary>
        /// <param name="ribbonPage"></param>
        public static void SetDefault(this RibbonPage ribbonPage)
        {
            if (ribbonPage is null)
            {
                throw new ArgumentNullException(nameof(ribbonPage));
            }

            ribbonPage.Tag = true;
        }
    }
}
