// See https://aka.ms/new-console-template for more information
using JavaAutoNet.Core;
using JavaAutoNet.Core.AccessBridgeAPI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);


//Console.WriteLine("Hello, World!");
AccessBridge.WindowsRun();
Application.DoEvents();
int vmID = 0;
IntPtr windowHandle = FindWindowByCaption(IntPtr.Zero, "Penjumlahan");
AccessBridge.GetAccessibleContextFromHWND(windowHandle, out Int32 VmID, out IntPtr javaWindowHandle);
string xpath = "root pane[0]/layered pane[0]/panel[0]/text[0]";
IntPtr ppPTR = AccessBridge.GetAccessibleChildFromContext(VmID, javaWindowHandle, 0);

IntPtr acInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleContextInfo()));
Marshal.StructureToPtr(new AccessibleContextInfo(), acInfoPtr, true);
AccessBridge.GetAccessibleContextInfo(VmID, ppPTR, acInfoPtr);
AccessibleContextInfo currentContext = (AccessibleContextInfo)Marshal.PtrToStructure(acInfoPtr, typeof(AccessibleContextInfo));

Console.WriteLine($"{currentContext.Role} - {currentContext.ChildrenCount}");
