using System;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 可以关闭的RibbonPage
    /// </summary>
    public interface IRibbonPageClose
    {
        /// <summary>
        /// 关闭
        /// </summary>
        event Action Close;
    }
}
