using System.Collections.ObjectModel;
using DevExpress.Xpf.Ribbon;
namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 标题视图
    /// </summary>
    public class CurrentRibbonView<T> where T : RibbonPage
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 获取Ribbon页列表
        /// </summary>
        /// <returns></returns>
        public virtual ObservableCollection<T> GetRibbonPages()
        {
            return new ObservableCollection<T>();
        }
    }
}
