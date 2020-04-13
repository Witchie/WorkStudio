﻿using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    public interface IApplicationInfo
    {
        string GetApplicationName();
        double GetMainWindowHeight();
        double GetMainWindowWidth();
        void LoadUI(Window window);
        void SetApplicationName(string value);
        void SetMainWindowHeight(double value);
        void SetMainWindowWidth(double value);
    }
}