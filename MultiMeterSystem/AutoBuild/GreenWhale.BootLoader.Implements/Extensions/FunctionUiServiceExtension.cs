using System;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Controls;
using DevExpress.Xpf.Grid;

namespace GreenWhale.BootLoader.Implements
{
    /// <summary>
    /// 功能UI扩展
    /// </summary>
    public static class FunctionUiServiceExtension
    {
        /// <summary>
        /// 添加导航栏工具箱
        /// </summary>
        /// <param name="functionUIService"></param>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="panelLocation"></param>
        public static void AddNavBarToolBox(this FunctionUIService functionUIService,string name,Action<NavBarControl> action, PanelLocation panelLocation = PanelLocation.Left)
        {
            if (functionUIService is null)
            {
                throw new ArgumentNullException(nameof(functionUIService));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            NavBarControl navBarControl = new NavBarControl();
            action?.Invoke(navBarControl);
            functionUIService.AddToolBox(navBarControl, name, panelLocation);
        }
        /// <summary>
        /// 添加级联菜单
        /// </summary>
        /// <param name="functionUIService"></param>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="panelLocation"></param>
        public static void AddTreeListToolBox(this FunctionUIService functionUIService,string name,Action<TreeListControl> action, PanelLocation panelLocation = PanelLocation.Left)
        {
            if (functionUIService is null)
            {
                throw new ArgumentNullException(nameof(functionUIService));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            TreeListControl treeListControl = new TreeListControl();
            action?.Invoke(treeListControl);
            functionUIService.AddToolBox(treeListControl, name, panelLocation);
        }
    }
}
