using System.Windows;
using GreenWhale.RunTime.Scripts;
using DevExpress.Xpf.Core;
using System;
namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public class MessageBox:IMessageBox
    {
        private readonly Window mainWindow;

        public MessageBox()
        {
            this.mainWindow = System.Windows.Application.Current.MainWindow;
        }

        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox)
        {
            return  mainWindow.Dispatcher.Invoke(() => {
                return DXMessageBox.Show(mainWindow, content, title, messageBox);
            });
        }

        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox, MessageBoxImage image)
        {
            return mainWindow.Dispatcher.Invoke(() => {
                return DXMessageBox.Show(mainWindow, content, title, messageBox, image);
            });

        }

        public MessageBoxResult Show(string content, string title, MessageBoxButton messageBox, MessageBoxImage image, MessageBoxResult defaultResult)
        {
            return mainWindow.Dispatcher.Invoke(() => {
                return DXMessageBox.Show(mainWindow, content, title, messageBox, image, defaultResult);
            });

        }

        public MessageBoxResult Show(string content, string title)
        {
            return mainWindow.Dispatcher.Invoke(() => {
                return DXMessageBox.Show(mainWindow, content, title);
            });

        }

        public MessageBoxResult Show(string content)
        {
            return mainWindow.Dispatcher.Invoke(() => {
                return DXMessageBox.Show(mainWindow, content);
            });
        }
    }
}
