using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 面板信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PanelInfo<T> : BoxCore<T> where T : FrameworkElement
    {

    }
    /// <summary>
    /// 核心框
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoxCore<T> where T : FrameworkElement
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 内容信息
        /// </summary>
        public T Content { get; set; }
    }
    /// <summary>
    /// 文档信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DocumentInfo<T>: BoxCore<T> where T : FrameworkElement
    {


    }
}
