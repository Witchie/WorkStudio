using GreenWhale.ProjectManager;

namespace GreenWhale.BootLoader.Implements.ProjectManager.Views
{
    /// <summary>
    /// 选中对象被修改事件
    /// </summary>
    public class SelectChangedEvent
    {
        public SelectChangedEvent(IProjectInfo currentSelect)
        {
            CurrentSelect = currentSelect;
        }

        /// <summary>
        /// 当前选中项目
        /// </summary>
        public IProjectInfo CurrentSelect { get; set; }
    }
}
