using GreenWhale.BootLoader.Implements.ProjectManager.Views;
using System.Collections.Generic;
using System.Text;

namespace GreenWhale.BootLoader.Implements.ProjectManager
{
    /// <summary>
    /// 功能UI扩展
    /// </summary>
    public static class FunctionUiExtension
    {
        /// <summary>
        /// 添加项目管理
        /// </summary>
        /// <param name="functionUIService"></param>
        public static void AddProjectManager(this FunctionUIService functionUIService)
        {
            functionUIService.AddProjectEditor().AddProjectList();
        }
        /// <summary>
        /// 添加项目列表
        /// </summary>
        /// <param name="functionUIService"></param>
        /// <returns></returns>
        public static FunctionUIService AddProjectList(this FunctionUIService functionUIService)
        {
            functionUIService.AddClickRibbonMenuWithToolBox<ProjectListEditorView>(new RibbonMenuWithPageView("运营/项目管理/项目列表:项目清单列表"));
            return functionUIService;
        }
        /// <summary>
        /// 但添加项目跟踪信息
        /// </summary>
        /// <param name="functionUIService"></param>
        /// <returns></returns>
        public static FunctionUIService AddProjectEditor(this FunctionUIService functionUIService)
        {
            functionUIService.AddClickRibbonMenuWithPages<ProjectItemEditorView, ProjectListEditorView>(new RibbonMenuWithPageView("运营/项目管理/项目跟踪:项目跟踪信息"), new RibbonMenuWithPageView("运营/项目管理/项目列表:项目清单列表"));
            return functionUIService;
        }
    }
}
