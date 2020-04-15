using GreenWhale.BootLoader.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using AutoUpdateCore.Net;
using AutoUpdaterDotNET;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class FunctionUiServiceExtnesion
    {
        /// <summary>
        /// 使用自动更新
        /// </summary>
        /// <param name="functionUIService"></param>
        /// <param name="packageUrl">更新包描述文件地址</param>
        /// <param name="appTitle">标题</param>
        /// <returns></returns>
        public static IFunctionUIService UseAutoUpdate(this IFunctionUIService functionUIService,string packageUrl,string appTitle)
        {
            if (functionUIService is null)
            {
                throw new ArgumentNullException(nameof(functionUIService));
            }
            AutoUpdater.AppCastURL = packageUrl;
            AutoUpdater.AppTitle = appTitle;
            functionUIService.AddClick(new RibbonMenuWithPageView("帮助/关于/更新"),(s)=>
            {
                AutoUpdater.Start(packageUrl);
            });
            return functionUIService;
        }
    }
}
