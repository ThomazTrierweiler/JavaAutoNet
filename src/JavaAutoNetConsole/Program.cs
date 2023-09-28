// See https://aka.ms/new-console-template for more information
using JavaAutoNet.Core;
using JavaAutoNet.Core.AccessBridgeAPI;
using JavaAutoNet.Core.Actions.NativeActions;
using JavaAutoNet.Core.Elements;
using JavaAutomationV1;
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

string xpath = "root pane[0]/layered pane[0]/panel[0]/scroll pane[0]/viewport[0]/table[0]/label";
using (IJavaAutomation javaAutomation = new JavaAutomation())
{
    Application.DoEvents(); //Unecessary if you solution is a Windows Forms Application
    /*using (IJavaElement? javaWindow = javaAutomation.FindJavaWindow("Planilha"))
    using (IJavaElement? javaElement = javaAutomation.FindJavaElement(javaWindow, xpath))
    {
        Console.WriteLine(javaElement.StatesInEnglish);
    }*/
    using (IJavaElement? javaWindow = javaAutomation.FindJavaWindow("Planilha"))
    {
        if (javaWindow == null) return;
        IEnumerable<IJavaElement> javaElements = javaAutomation.FindJavaElements(javaWindow, xpath);
        foreach (IJavaElement javaElement in javaElements)
        {
            Console.WriteLine(javaElement.Name);
            javaElement.Dispose();
        }
    }
        
}



