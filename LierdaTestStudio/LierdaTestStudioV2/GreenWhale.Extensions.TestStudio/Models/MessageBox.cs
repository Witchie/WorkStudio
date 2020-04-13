using System.Windows;
using GreenWhale.RunTime.Scripts;
using DevExpress.Xpf.Core;
using System;
using System.Threading.Tasks;

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public class MessageBox:IMessageBox
    {
        private readonly Window mainWindow;

        public MessageBox()
        {
            this.mainWindow = System.Windows.Application.Current.MainWindow;
        }

        public async Task<MessageBoxResult> ShowAsync(string content, string title, MessageBoxButton messageBox)
        {
            return await mainWindow.Dispatcher.InvokeAsync(() => {
                return DXMessageBox.Show(mainWindow, content, title, messageBox);
            });
        }

        public async Task<MessageBoxResult> ShowAsync(string content, string title, MessageBoxButton messageBox, MessageBoxImage image)
        {
            return await mainWindow.Dispatcher.InvokeAsync(() => {
                return DXMessageBox.Show(mainWindow, content, title, messageBox, image);
            });

        }

        public async Task<MessageBoxResult> ShowAsync(string content, string title, MessageBoxButton messageBox, MessageBoxImage image, MessageBoxResult defaultResult)
        {
            return await mainWindow.Dispatcher.InvokeAsync(() => {
                return DXMessageBox.Show(mainWindow, content, title, messageBox, image, defaultResult);
            });

        }

        public async Task<MessageBoxResult> ShowAsync(string content, string title)
        {
            return await mainWindow.Dispatcher.InvokeAsync(() => {
                return DXMessageBox.Show(mainWindow, content, title);
            });

        }

        public async Task<MessageBoxResult> ShowAsync(string content)
        {
            return await mainWindow.Dispatcher.InvokeAsync(() => {
                return DXMessageBox.Show(mainWindow, content);
            });
        }

        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox)
        {
            return DXMessageBox.Show(mainWindow, content, title, messageBox);

        }

        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox, MessageBoxImage image)
        {
            return DXMessageBox.Show(mainWindow, content, title, messageBox, image);
        }

        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox, MessageBoxImage image, MessageBoxResult defaultResult)
        {
            return DXMessageBox.Show(mainWindow, content, title, messageBox, image, defaultResult);
        }

        public MessageBoxResult Show(string content, string title)
        {
            return DXMessageBox.Show(mainWindow, content, title);
        }

        public MessageBoxResult Show(string content)
        {
            return DXMessageBox.Show(mainWindow, content);
        }
    }
}
