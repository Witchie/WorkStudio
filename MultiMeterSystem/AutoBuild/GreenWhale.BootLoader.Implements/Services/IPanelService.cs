using DevExpress.Xpf.Docking;
using System.Windows;

namespace GreenWhale.BootLoader.Implements
{
    public interface IPanelService
    {
        DocumentInfo<BaseLayoutItem> CreateDocumentPanel<T>(DocumentInfo<T> documentPanel) where T : FrameworkElement;
        LayoutPanel CreateOutputBox();
        PanelInfo<T> CreateToolBoxPanel<T>(PanelInfo<T> panelInfo, PanelLocation panelLocation = PanelLocation.Left) where T : FrameworkElement;
    }
}