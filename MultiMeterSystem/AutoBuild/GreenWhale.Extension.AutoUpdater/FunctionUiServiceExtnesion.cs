using GreenWhale.BootLoader.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using AutoUpdateCore.Net;
using AutoUpdaterDotNET;
namespace GreenWhale.Extensions.Updater
{
    public static class FunctionUiServiceExtnesion
    {
        public static IFunctionUIService UseAutoUpdate(this IFunctionUIService functionUIService,string packageUrl,string appTitle)
        {
            if (functionUIService is null)
            {
                throw new ArgumentNullException(nameof(functionUIService));
            }
            AutoUpdater.AppCastURL = packageUrl;
            AutoUpdater.AppTitle = appTitle;
            functionUIService.AddClick(new RibbonMenuWithPageView("关于/帮助/更新"),(s)=>
            {
                AutoUpdater.Start(packageUrl);
            });
            return functionUIService;
        }
    }
}
