using GreenWhale.ProjectManager;
using System;
using System.Diagnostics;

namespace GreenWhale.BootLoader.Implements.ProjectManager.Views
{
    /// <summary>
    /// 选择列表事件
    /// </summary>
    public class ProjectListEditorViewService
    {
        private readonly ProjectListEditorView projectListEditorView;

        public ProjectListEditorViewService(ProjectListEditorView projectListEditorView)
        {
            this.projectListEditorView = projectListEditorView;
            projectListEditorView.dxg.MouseDoubleClick += Dxg_MouseDoubleClick;
        }

        private void Dxg_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = projectListEditorView.dxg.SelectedItem as IProjectInfo;
            if (item!=null)
            {
                SelectionChanged?.Invoke(new SelectChangedEvent(item));

            }

        }

        /// <summary>
        /// 选择列表被修改
        /// </summary>
        public event Action<SelectChangedEvent> SelectionChanged;
        /// <summary>
        /// 当前选中行
        /// </summary>
        public IProjectInfo CurrentSelect => projectListEditorView.dxg.SelectedItem as IProjectInfo;
    }
}
