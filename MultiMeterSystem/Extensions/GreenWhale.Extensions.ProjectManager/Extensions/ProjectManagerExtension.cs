using GreenWhale.BootLoader.Implements.ProjectManager.Views;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
namespace GreenWhale.BootLoader.Implements.ProjectManager
{
    /// <summary>
    /// 项目管理扩展
    /// </summary>
    public static class ProjectManagerExtension
    {
        /// <summary>
        /// 添加项目管理
        /// </summary>
        /// <typeparam name="TRootWindow"></typeparam>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="coreApplication"></param>
        /// <param name="applicationInfo"></param>
        /// <returns></returns>
        public static NetCoreApplication<TApplication> AddProjectManager<TApplication>(this NetCoreApplication<TApplication> coreApplication) where TApplication : Application
        {
            if (coreApplication is null)
            {
                throw new System.ArgumentNullException(nameof(coreApplication));
            }

            var service= coreApplication.ServiceBus;
            service.AddTransient<ProjectManagerDataSource>();
            service.AddSingleton<ProjectItemEditorView>();
            service.AddSingleton<ProjectListEditorView>();
            service.AddSingleton<ProjectListEditorViewService>();
            service.AddSingleton<ProjectItemEditorViewService>();
            return coreApplication;
        }
    }
}
