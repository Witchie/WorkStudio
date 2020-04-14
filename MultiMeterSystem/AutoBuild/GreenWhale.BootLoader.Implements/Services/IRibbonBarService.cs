using DevExpress.Xpf.Ribbon;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GreenWhale.BootLoader.Implements
{
    public interface IRibbonBarService
    {
        bool CurrentRibbonIsVisible { get; set; }

        void LoadCurrentRibbon<TRibbonView>(TRibbonView currentRibbonView) where TRibbonView : CurrentRibbonView<RibbonPage>;
        void LoadRibbonPages(IEnumerable<RibbonMenuWithPageView> pageViews);
        ObservableCollection<RibbonPage> RibbonPages { get;  set; }
    }
}