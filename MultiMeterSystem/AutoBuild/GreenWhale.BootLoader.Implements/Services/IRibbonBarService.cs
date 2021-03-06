﻿using DevExpress.Xpf.Ribbon;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GreenWhale.BootLoader.Implements
{
    public interface IRibbonBarService
    {
        ObservableCollection<RibbonPage> RibbonPages { get; set; }

        void LoadRibbonPages(IEnumerable<RibbonMenuWithPageView> pageViews);
    }
}