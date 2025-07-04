using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace UWPWebView2WithoutWinUI2.ComTypes
{
    /// <summary>
    /// 使应用能够 (与此接口关联的 CoreWindow) 获取窗口的窗口句柄。
    /// </summary>
    [GeneratedComInterface, Guid("45D64A29-A63E-4CB6-B498-5781D298CB4F")]
    public partial interface ICoreWindowInterop
    {
        /// <summary>
        /// 获取应用的 CoreWindow (HWND) 句柄。
        /// </summary>
        [PreserveSig]
        int Get_WindowHandle(out IntPtr handle);

        /// <summary>
        /// 设置是否已处理到 CoreWindow 的消息。此属性是只写的。
        /// </summary>
        [PreserveSig]
        int Set_MessageHandled([MarshalAs(UnmanagedType.Bool)] bool messageHandled);
    }
}