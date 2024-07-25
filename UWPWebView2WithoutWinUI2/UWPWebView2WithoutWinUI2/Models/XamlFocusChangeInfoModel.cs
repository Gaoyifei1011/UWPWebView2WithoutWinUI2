using Microsoft.Web.WebView2.Core;

namespace UWPWebView2WithoutWinUI2.Models
{
    public struct XamlFocusChangeInfoModel
    {
        public CoreWebView2MoveFocusReason storedMoveFocusReason;

        public bool isPending;
    }
}