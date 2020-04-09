using GreenWhale.ProjectManager;

namespace GreenWhale.BootLoader.Implements.ProjectManager.Views
{
    /// <summary>
    /// 项目管理视图服务
    /// </summary>
    public class ProjectItemEditorViewService
    {
        private readonly ProjectItemEditorView projectItemEditorView;
        private readonly ProjectListEditorViewService listEditorViewService;

        public ProjectItemEditorViewService(ProjectItemEditorView projectItemEditorView, ProjectListEditorViewService listEditorViewService)
        {
            this.projectItemEditorView = projectItemEditorView;
            this.listEditorViewService = listEditorViewService;
            listEditorViewService.SelectionChanged += ListEditorViewService_SelectionChanged;
        }
        private void ListEditorViewService_SelectionChanged(SelectChangedEvent obj)
        {
            projectItemEditorView.LoadDataContext(obj.CurrentSelect);
        }

    }
}
