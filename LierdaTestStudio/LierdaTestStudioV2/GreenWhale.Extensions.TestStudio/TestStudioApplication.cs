﻿using DevExpress.Xpf.Core;
using GreenWhale.BootLoader;
using GreenWhale.BootLoader.Implements;
using GreenWhale.Extensions.TestTools2.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using App = System.Windows.Application;
namespace GreenWhale.Extensions.TestStudio
{
    /// <summary>
    /// 测试Studio应用
    /// </summary>
    public class TestStudioApplication
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <param name="application"></param>
        /// <param name="mainWindow"></param>
        /// <param name="themeName"></param>
        /// <returns></returns>
        public Task<NetCoreApplication<TApplication>> StartAsync<TApplication>(TApplication application,Window mainWindow,string themeName= Theme.Office2010BlueName) where TApplication: App
        {
            return Task.Run(() => {
                var info = new NetCoreApplication<TApplication>(application, new AppSetting { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory, IsMutexApplication = true });
                info.AddThemeName(themeName).AddVsMode().AddTestStudio().AddMainWindow(mainWindow).BuildService();
                return info;
            });
        }
    }
}
