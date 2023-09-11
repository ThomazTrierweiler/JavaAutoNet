// See https://aka.ms/new-console-template for more information
using JavaAutoNet.Core;
using JavaAutoNet.Core.AccessBridgeAPI;
using JavaAutomationV1;
using JavaAutoNet.Core.Actions.NativeActions;
using JavaAutoNet.Core.Elements;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Linq;

[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

/*
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
*/

Console.WriteLine("Hello, World!");
AccessBridge.WindowsRun();
Application.DoEvents();

//IntPtr windowHandle = FindWindowByCaption(IntPtr.Zero, "Penjumlahan");
string xpath = "root pane[0]/layered pane[0]/panel[0]/text[0]";
using (IJavaAutomation javaAutomation = new JavaAutomationV1.JavaAutomationV1())
{
    IJavaElement? javaWindow = javaAutomation.FindJavaWindow("Penjumlahan");
    Console.WriteLine(javaWindow.Name + " - " + javaWindow.Role + " - " + javaWindow.Text + " - " + javaWindow.IndexInParent);
    /*
    List<IJavaElement> children = javaWindow.GetChildren().ToList();

    foreach (IJavaElement child in children[0].GetChildren())
        Console.WriteLine(child.Name + " - " + child.Role + " - " + child.Text + " - " + child.IndexInParent);
    */
    IJavaElement? childElement = javaAutomation.FindJavaElement(javaWindow, xpath);
    Console.WriteLine(childElement.Name + " - " + childElement.Role + " - " + childElement.Text + " - " + childElement.IndexInParent);
}



