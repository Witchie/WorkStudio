using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GreenWhale.Application.SerialPorts;
using GreenWhale.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using GreenWhale.BootLoader;
using GreenWhale.BootLoader.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DevExpress.Xpf.Core;
using GreenWhale.Extensions.TestTools2.Extensions;
using GreenWhale.Extensions.TestTools2;
using GreenWhale.Extensions.Views;
using System.Threading;
using GreenWhale.Extensions.TestStudio;

namespace LSD3SWM_0710000000
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           // Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-cn");

        }
    }
}
