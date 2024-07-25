# UWPWebView2WithoutWinUI2

### 一个不依赖 WinUI 2 的 UWP WebView 2 纯 c# 版本实现

### A pure c# version of UWP WebView 2 that does not rely on WinUI 2

------

##### 目前在 UWP 项目中，如果要引入 WebView 2 控件，需要安装 Microsoft.UI.Xaml (WinUI 2 ) Nuget 包。然而 WinUI 2却不能在打包项目中包含 WinUI 3 桌面应用程序或项目不是打包项目时中引用。

##### Currently in the UWP project, if you want to introduce WebView 2 controls, you need to install the Microsoft.UI.Xaml (WinUI 2) Nuget package. WinUI 2, however, cannot be referenced when a WinUI 3 desktop application is included in a package project or when the project is not a package project.

------

##### Microsoft 在 WinUI 2 中已经提供了 WebView 2 c++ WinRT 的源代码供 c++ 或 C# 等其他编程语言使用，这是我根据 WebView 2 c++ 源代码，转换为 c# 版本的 WebView 2 控件。

##### Microsoft in WinUI 2 has provided WebView 2 c++ WinRT source code for c++ or C# and other programming languages to use, this is my WebView 2 c++ source code, converted to c# version of WebView 2 control.

------

##### UWP WebView 2 项目说明：https://learn.microsoft.com/microsoft-edge/webview2/get-started/winui2

##### UWP WebView 2 project description: https://learn.microsoft.com/microsoft-edge/webview2/get-started/winui2

------

##### 示例(Demo)

![image](https://github.com/user-attachments/assets/d8c79d1d-6611-4b62-bec4-248624f36013)