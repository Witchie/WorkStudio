using GreenWhale.Extensions.TestTools2.Views;
using System;

namespace GreenWhale.Extensions.TestTools2.Models
{
    public class MessageTips : IMessageTip
    {
        private readonly ScanPanelView scanPanelView;

        public MessageTips(ScanPanelView scanPanelView)
        {
            this.scanPanelView = scanPanelView;
        }

        public void ShowTip(string content, TimeSpan? timeSpan = null)
        {
            scanPanelView.ShowTip(content, timeSpan);
        }

        public void CloseTip()
        {
            scanPanelView.CloseTip();
        }
    }
}
