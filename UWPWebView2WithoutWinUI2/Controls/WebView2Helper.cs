﻿using System;
using UWPWebView2WithoutWinUI2.PInvoke.User32;
using Windows.Foundation;
using Windows.System;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;

namespace UWPWebView2WithoutWinUI2.Controls
{
    /// <summary>
    /// 这些实用程序将 WinRT 事件参数转换为 Win32 事件参数，并发送给 WebView
    /// 参考 winuser.h 输入的代码：https://docs.microsoft.com/en-us/windows/desktop/api/winuser/
    /// </summary>
    public static class WebView2Utility
    {
        public static short GetWheelDataWParam(nuint wParam)
        {
            return (short)HiWord(wParam);
        }

        public static short GetXButtonWParam(nuint wParam)
        {
            return (short)HiWord(wParam);
        }

        public static short GetKeystateWParam(nuint wParam)
        {
            return (short)LoWord(wParam);
        }

        public static long LoWord(nint Number)
        {
            return Number & 0xffff;
        }

        public static long LoWord(nuint Number)
        {
            return Convert.ToInt64(Number & 0xffff);
        }

        public static long HiWord(nint Number)
        {
            return Number >> 16 & 0xffff;
        }

        public static long HiWord(nuint Number)
        {
            return Convert.ToInt64(Number >> 16 & 0xffff);
        }

        public static uint MakeWParam(ushort low, ushort high)
        {
            return (uint)high << 16 | low;
        }

        public static int MakeLParam(int LoWord, int HiWord)
        {
            return HiWord << 16 | LoWord & 0xffff;
        }

        public static nint PackIntoWin32StylePointerArgs_lparam(Point point)
        {
            // 这些对于基于 WM_POINTER 和 WM_MOUSE 的事件是相同的
            // Pointer: https://msdn.microsoft.com/en-us/ie/hh454929(v=vs.80)
            // Mouse: https://docs.microsoft.com/en-us/windows/desktop/inputdev/wm-mousemove
            nint lParam = new(MakeLParam((int)point.X, (int)point.Y));
            return lParam;
        }

        public static nuint PackIntoWin32StyleMouseArgs_wparam(WindowMessage message, PointerRoutedEventArgs args, PointerPoint pointerPoint)
        {
            ushort lowWord = 0x0;
            ushort highWord = 0x0;

            VirtualKeyModifiers modifiers = args.KeyModifiers;

            // 可以支持像 Ctrl|Alt + Scroll 这样的情况，Alt 将被忽略，它将被视为 Ctrl + Scroll
            if (((int)modifiers & (int)VirtualKeyModifiers.Control) is not 0)
            {
                lowWord |= (ushort)MODIFIERKEYS_FLAGS.MK_CONTROL;
            }
            if (((int)modifiers & (int)VirtualKeyModifiers.Shift) != 0)
            {
                lowWord |= (ushort)MODIFIERKEYS_FLAGS.MK_SHIFT;
            }

            PointerPointProperties properties = pointerPoint.Properties;

            if (properties.IsLeftButtonPressed)
            {
                lowWord |= (ushort)MODIFIERKEYS_FLAGS.MK_LBUTTON;
            }
            if (properties.IsRightButtonPressed)
            {
                lowWord |= (ushort)MODIFIERKEYS_FLAGS.MK_RBUTTON;
            }
            if (properties.IsMiddleButtonPressed)
            {
                lowWord |= (ushort)MODIFIERKEYS_FLAGS.MK_MBUTTON;
            }
            if (properties.IsXButton1Pressed)
            {
                lowWord |= (ushort)MODIFIERKEYS_FLAGS.MK_XBUTTON1;
            }
            if (properties.IsXButton2Pressed)
            {
                lowWord |= (ushort)MODIFIERKEYS_FLAGS.MK_XBUTTON2;
            }

            // 鼠标滚动消息 : https://docs.microsoft.com/en-us/windows/desktop/inputdev/wm-mousewheel
            if (message is WindowMessage.WM_MOUSEWHEEL or WindowMessage.WM_MOUSEHWHEEL)
            {
                highWord = (ushort)properties.MouseWheelDelta;
            }
            else if (message is WindowMessage.WM_XBUTTONDOWN or WindowMessage.WM_XBUTTONUP)
            {
                PointerUpdateKind pointerUpdateKind = properties.PointerUpdateKind;
                if (pointerUpdateKind == PointerUpdateKind.XButton1Pressed ||
                    pointerUpdateKind == PointerUpdateKind.XButton1Released)
                {
                    highWord |= (ushort)MOUSEHOOKSTRUCTEX_MOUSE_DATA.XBUTTON1;
                }
                else if (pointerUpdateKind == PointerUpdateKind.XButton2Pressed ||
                         pointerUpdateKind == PointerUpdateKind.XButton2Released)
                {
                    highWord |= (ushort)MOUSEHOOKSTRUCTEX_MOUSE_DATA.XBUTTON2;
                }
            }

            nuint wParam = new(MakeWParam(lowWord, highWord));
            return wParam;
        }

        public static void ScheduleActionAfterWait(CoreDispatcher Dispatcher, Action action, uint millisecondWait)
        {
            // 给 CreateTimer 的回调在 UI 线程中被调用。
            // 为了使这个有用，我们可以与 XAML 对象进行交互，
            // 在执行 UI 线程之前，我们将使用调度程序先将我们的工作发布到UI线程。
            ThreadPoolTimer timer = ThreadPoolTimer.CreateTimer(async _ => await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()), TimeSpan.FromMilliseconds(millisecondWait));
        }
    }
}
